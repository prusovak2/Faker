using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

/*
    Faker
    Generator of pseudo random contend of C# objects
    Katerina Prusova, sophomore MFF UK
    summer term 2019/2020 
    Jazyk C# a platforma .NET (NPRG035), Pokročilé programování pro .NET I (NPRG038)
*/

namespace Faker
{

    internal interface IInnerFaker
    {
        public InnerFakerConstructorUsage CtorUsageFlag { get; }
        internal object Generate(object instance);
        internal bool AllRulesSetDeep();
        HashSet<MemberInfo> GetAllMembersRequiringRuleDeep();
    }

    public partial class BaseFaker<TClass> : IInnerFaker where TClass : class
    {
        /// <summary>
        /// Setters for members of TClass compiled in runtime by Expression.Lambda.Compile
        /// </summary>
        static internal Dictionary<MemberInfo, Action<TClass, object>> Setters { get; set; } = new();
        /// <summary>
        /// Not ignored (by ignore attribute) members of TClass type <br/>
        /// used by Strict and Auto Faker instances as the base of RulelessMembersInstance set
        /// </summary>
        static internal HashSet<MemberInfo> AllNotIgnoredMembers { get; set; } = null;
        /// <summary>
        /// source of pseudo-random entities
        /// </summary>
        public RandomGenerator Random { get; }
        /// <summary>
        /// ctor to be used to create an instance of TClass when faker is used as inner faker <br/>
        /// (and Generate method is called automatically) <br/>
        /// default is InnerFakerConstructorUsage.Parameterless
        /// </summary>
        public InnerFakerConstructorUsage CtorUsageFlag 
        {
            get 
            {
                return this._ctorUsageFlag;
            }
            protected set
            {
                if (this.IsFrozen)
                {
                    throw new InvalidOperationException("State of the frozen Faker cannot be changed");
                }
                this._ctorUsageFlag = value;
            }
        } 
        private InnerFakerConstructorUsage _ctorUsageFlag = InnerFakerConstructorUsage.Parameterless;
        /// <summary>
        /// Parameters for TClass constructor used when CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters 
        /// </summary>
        public object[] CtorParameters 
        {
            get
            {
                return this._ctorParams;
            }
            protected set
            {
                if (this.IsFrozen)
                {
                    throw new InvalidOperationException("State of the frozen Faker cannot be changed");
                }
                this._ctorParams = value;
            }
        } 
        private object[] _ctorParams = new object[] { };
        /// <summary>
        /// affects a behavior of some of the methods on RandomGenerator to produce more culturally adequate values (names, city names...)  
        /// </summary>
        public CultureInfo Culture => this.Random.Culture;
        /// <summary>
        /// Members requiring rule by StrictFaker instances and being filled by default random functions by AutoFaker instances
        /// </summary>
        internal HashSet<MemberInfo> RulelessMembersInstance { get; set; } = new HashSet<MemberInfo>();
        /// <summary>
        /// Generate call on a faker calls Generate on all its innerFakers 
        /// </summary>
        internal IDictionary<MemberInfo, IInnerFaker> InnerFakers { get; } = new Dictionary<MemberInfo, IInnerFaker>();
        /// <summary>
        /// rules used to generate pseudo-random content
        /// </summary>
        internal IDictionary<MemberInfo, ChainedRule> Rules { get; } = new Dictionary<MemberInfo, ChainedRule>();
        /// <summary>
        /// set of members whose content should not be filled by a default random function by AutoFaker <br/>
        /// rules are nor required for ignored members by the StrictFaker <br/>
        /// members get inserted here by calling Ignore(member) in Fakers ctor <br/>
        /// once member is Ignored, it cannot have a Rule or InnnerFaker set for it in the same instance of the AutoFaker
        /// </summary>
        internal HashSet<MemberInfo> Ignored { get; } = new HashSet<MemberInfo>();
        /// <summary>
        /// last member for which an unconditional rule or InnerFaker was set <br/>
        /// used for constructing chains of related rules
        /// </summary>
        internal MemberInfo pendingMember { get; private set; } = null;
        /// <summary>
        /// If frozen, no more rules can be added to this Faker <br/>
        /// The first .Generate call freezes the instance
        /// </summary>
        public bool IsFrozen { get; private set; } = false;

        /// <summary>
        /// new instance of BaseFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// this BaseFaker uses a default en-US culture
        /// </summary>
        public BaseFaker()
        {
            this.Random = new RandomGenerator();
            
        }
        /// <summary>
        /// new instance of BaseFaker customized to given culture that creates a new instance of the RandomGenerator and produces its seed automatically
        /// </summary>
        /// <param name="info"></param>
        public BaseFaker(CultureInfo info)
        {
            this.Random = new RandomGenerator(info);
        }
        /// <summary>
        /// new instance of BaseFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// this BaseFaker uses a default en-US culture
        /// </summary>
        /// <param name="seed"></param>
        public BaseFaker(ulong seed)
        {
            this.Random = new RandomGenerator(seed);
        }
        /// <summary>
        /// new instance of BaseFaker customized to given culture that creates a new instance of RandomGenerator with a given seed
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="info"></param>
        public BaseFaker(ulong seed, CultureInfo info)
        {
            this.Random = new RandomGenerator(seed, info);
        }
        /// <summary>
        /// New instance of BaseFaker customized to given culture that uses existing instance of RandomGenerator <br/>
        /// One instance of random generator can be shared by multiple fakers to save memory <br/>
        /// Recommended for innerFakers 
        /// </summary>
        /// <param name="randomGenerator"></param>
        public BaseFaker(RandomGenerator randomGenerator)
        {
            this.Random = randomGenerator;
        }
        /// <summary>
        /// Freezes this instance, no mo rules can be added
        /// </summary>
        public void Freeze()
        {
            this.IsFrozen = true;
        }

        /// <summary>
        /// Selects a member of TClass to have InnerFaker set for it
        /// </summary>
        /// <typeparam name="TInnerClass">Type of member</typeparam>
        /// <param name="selector"> lambda returning the member</param>
        /// <returns>fluent syntax helper</returns>
        /// <exception cref="FakerException">Throws FakerException, when SetFakerFor is called with a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /// <exception cref="InvalidOperationException">Thrown when the instance is already frozen.</exception>
        public RefMemberFluent<TInnerClass> SetFakerFor<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector) where TInnerClass: class
        {
            if (this.IsFrozen)
            {
                throw new InvalidOperationException("No more rules can be added to the frozen instance of the Faker.\n The first invocation of Generate or Populate method freezes the instance.");
            }
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("Multiple inner fakers cannot be set for the same member.");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("Inner faker cannot be set for a member that is already marked Ignored by Ignore call.");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("Inner faker cannot be set for a member that already has an unconditional rule set for it.");
            }

            //remember this member, .As call will retrieve it
            this.pendingMember = memberInfo;
            AddSetterIfNew<TInnerClass>(memberInfo);

            return new RefMemberFluent<TInnerClass>(this);
        }
        /// <summary>
        /// Called from .As method, sets an inner faker for a member stores in pendingMember property
        /// </summary>
        /// <typeparam name="TInnerClass">Type of member to have inner faker set for it</typeparam>
        /// <param name="faker">faker to be used as an inner faker </param>
        private protected void _faker<TInnerClass>(BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.pendingMember;
            this.pendingMember = null;
            // whether member the member isn't ignored or does not have a InnerFaker set for it already is checked in SetFakerFor method
            this.InnerFakers.Add(memberInfo, faker);
            this.RulelessMembersInstance.Remove(memberInfo);
        }
        /// <summary>
        /// Selects a member of TClass to have an unconditional rule set for it
        /// </summary>
        /// <typeparam name="TFirstMember">Type of member</typeparam>
        /// <param name="selector">lambda returning a member</param>
        /// <returns>Fluent syntax helper</returns>
        /// <exception cref="FakerException">Throws FakerException, when For is called with a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /// <exception cref="InvalidOperationException">Thrown when the instance is already frozen.</exception>
        public FirstMemberFluent<TFirstMember> For<TFirstMember>(Expression<Func<TClass, TFirstMember>> selector)
        {
            if (this.IsFrozen)
            {
                throw new InvalidOperationException("No more rules can be added to the frozen instance of Faker.");
            }
            // the first one of series of chained rules is unconditional, the rest of rules are conditional
            // therefore the For can be set only for a member with no unconditional rule or inner faker set for it
            // multiple conditional rules can be set for a member
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("Unconditional rule cannot be set for a member that already has an inner faker set for it.");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("Unconditional cannot be set for a member that is already marked Ignored by Ignore call");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("Multiple unconditional rules cannot be set for the same member.");
            }
            AddSetterIfNew<TFirstMember>(memberInfo);

            this.pendingMember = memberInfo;

            //resolver initialization for this TFirstMember member
            //creates a new resolver<IFirstMember> that contains the first ConditionPack with this memberInfo an always true condition
            ChainedRuleTyped<TFirstMember> resolver = new ChainedRuleTyped<TFirstMember>(memberInfo);
            this.Rules.Add(memberInfo, resolver);
            RulelessMembersInstance.Remove(memberInfo);

            return new FirstMemberFluent<TFirstMember>(this);
        }
        /// <summary>
        /// Selects a member of a TClass to have a conditional rule set for it
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of member to have an unconditional rule set for it</typeparam>
        /// <param name="memberInfo">member to have a rule set</param>
        /// <returns>fluent syntax helper</returns>
        private protected MemberFluent<TFirstMember, TCurMember> _for<TFirstMember, TCurMember>(MemberInfo memberInfo)
        {
            AddSetterIfNew<TCurMember>(memberInfo);

            //add MemberInfo info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleTyped<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddMemberToLastRulePack(memberInfo);
            RulelessMembersInstance.Remove(memberInfo);  //makes a member ignored by AutoFaker.RandomlyFillRemainingMembers

            return new MemberFluent<TFirstMember, TCurMember>(this);
        }
        /// <summary>
        /// Sets unconditional rule for a member <br/>
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        /// <param name="setter"></param>
        /// <returns>fluent syntax helper</returns>
        private protected FirstRuleFluent<TFirstMember> _firtsSetRule<TFirstMember>(Func<RandomGenerator, TFirstMember> setter)
        {
            // this case needs to be treated by a separate method, because unconditional member and rule are to be stored differently than conditional ones
            // .Otherwise method cannot be called on 'First' syntax helpers

            // add Function info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleTyped<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddFirstFunc(() => setter(this.Random));

            return new FirstRuleFluent<TFirstMember>(this);
        }
        /// <summary>
        /// Sets a conditional rule for a member
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of member to have an unconditional rule set for it</typeparam>
        /// <param name="setter">function to be used to fill a member</param>
        /// <returns>fluent syntax helper</returns>
        private protected RuleFluent<TFirstMember> _setRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            //add Function info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleTyped<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddFunctionToLastRulePack(() => setter(this.Random));

            return new RuleFluent<TFirstMember>(this);
        }
        /// <summary>
        /// sets an condition using value generated by an unconditional rule at the beginning of this rule chain to be evaluated  
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <param name="condition"></param>
        /// <returns>fluent syntax helper</returns>
        private protected ConditionFluent<TFirstMember> _when<TFirstMember>(Func<TFirstMember, bool> condition)
        {
            //add Condition info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleTyped<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddNewCondPackWithCondition(condition);

            return new ConditionFluent<TFirstMember>(this);
        }
        /// <summary>
        /// sets an Otherwise condition, it is satisfied if and only if any of .When conditions preceding this .Otherwise has not been satisfied
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <returns>fluent syntax helper</returns>
        private protected ConditionFluent<TFirstMember> _otherwise<TFirstMember>()
        {
            //add Otherwise condition to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleTyped<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddNewCondPackWithOtherwiseCondition();

            return new ConditionFluent<TFirstMember>(this);
        }
        /// <summary>
        /// returns a Resolver storing a rule chain corresponding to given leading member
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        /// <param name="memberInfo">the first member in particular rule chain</param>
        /// <returns></returns>
        private protected ChainedRuleTyped<TFirstMember> GetResolverForMemberInfo<TFirstMember>(MemberInfo memberInfo)
        {
            if (!this.Rules.ContainsKey(memberInfo))
            {
                throw new InvalidOperationException("This MemberInfo should be present in Rules dict, flawed implemenation");
            }
            if(this.Rules[memberInfo] is ChainedRuleTyped<TFirstMember> chainedRuleResover)
            {
                return chainedRuleResover;
            }
            throw new InvalidOperationException("Unexpected, this method should only be called with memberInfos corresponding to chainedRules");
        }
        /// <summary>
        /// Sets member as Ignored - this member won't be filled by default random function by AutoFaker instances <br/>
        /// </summary>
        /// <typeparam name="TMember">Type of member to be ignored</typeparam>
        /// <param name="selector">lambda returning member to be ignored</param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to Ignore a member that already has a Rule or InnerFaker set for it</exception>
        /// <exception cref="InvalidOperationException">Thrown when the instance is already frozen.</exception>
        private protected void _internalIgnore<TMember>(MemberInfo memberInfo)
        {
            if (this.IsFrozen)
            {
                throw new InvalidOperationException("No more rules can be added to the frozen instance of Faker.");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an RuleFor set for it");
            }
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an InnerFaker set for it");
            }
            
            // Ignored method called multiple times with the same member as the parameter won't change the semantics of the program
            // no need to throw the FakerException here
            this.Ignored.Add(memberInfo); //does not throw exception when member info is already present in the HashSet, returns false
        }

        /// <summary>
        /// Creates new instance of TClass using parameterless constructor, generates a random content base on RulesFor and InnerFakers stated for this Faker<br/>
        /// it is recommended to created instance by ctor and than call Populate() on existing instance - avoids using reflexion to create an instance
        /// </summary>
        /// <returns>New TClass instance with random content</returns>
        /// <exception cref="ArgumentException">Throws ArgumentException, when TClass does not have a parameterless constructor</exception>
        public TClass Generate()
        {
            Type type = typeof(TClass);
            TClass instance;
            try
            {
                instance = (TClass)Activator.CreateInstance(type);
            }
            catch(Exception e)
            {
                throw new FakerException($"Problem with parameterless constructor on {typeof(TClass)} type, use other overload of generate" + e.Message);
            }

            // Use rules
            instance = this.Populate(instance);
            return instance;
        }
        /// <summary>
        /// Creates new instance of TClass using constructor with corresponding param. types, generates a random content base on RulesFor and InnerFakers stated for this Faker<br/>
        /// it is recommended to created instance by ctor and than call Populate() on existing instance - avoids using reflexion to create an instance
        /// </summary>
        /// <returns>New TClass instance with random content</returns>
        /// <exception cref="ArgumentException">Throws ArgumentException, when TClass does not have a corresponding constructor</exception>
        public TClass Generate(params object[] CtorParams)
        {
            //get array of param. types
            Type[] paramTypes = new Type[CtorParams.Length];
            for (int i = 0; i < CtorParams.Length; i++)
            {
                paramTypes[i] = CtorParams[i].GetType();
            }
            Type type = typeof(TClass);
            //check whether the class has corresponding constructor 
            ConstructorInfo ctor = type.GetConstructor(paramTypes);
            if (ctor is null)
            {
                throw new FakerException("Your class does not have a constructor with corresponding parameters");
            }
            TClass instance = (TClass)ctor.Invoke(CtorParams);
            //apply rules
            instance = Populate(instance);
            return instance;
        }
        /// <summary>
        /// Use rules to fill the instance with a random content
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>The same instance filled with a random content based on rules</returns>
        public TClass Populate(TClass instance)
        {
            return this._internal_populate(instance);
        }
        private protected virtual TClass _internal_populate(TClass instance)
        {
            this.IsFrozen = true; //Freeze this instance right before the rules are accessed
            if(instance is null)
            {
                throw new FakerException($"Argument of Populate must be existing instance of {typeof(TClass)} type");
            }
            // Use rules
            foreach (var chainedRulePair in this.Rules)
            {
                chainedRulePair.Value.ResolveChainedRule(instance, this);
            }
            // Use InnerFakers
            foreach (var innerFaker in this.InnerFakers)
            {
                this.UseInnerFaker(instance, innerFaker.Key, innerFaker.Value);
            }
            return instance;
        }
        /// <summary>
        /// Fill one member with a random contend
        /// </summary>
        /// <param name="instance">instance, whose member is to be filled</param>
        /// <param name="MemberInfo">info about member to be filled</param>
        /// <param name="RandomFunc">Function, that generates a content to be used as the member value</param>
        /// <returns>Generated random value</returns>
        internal object UseRule(TClass instance, MemberInfo MemberInfo, Func<object> RandomFunc)
        {
            var memberSetter = Setters[MemberInfo];
            //member is a property
            if(MemberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var randomValue = RandomFunc();
                memberSetter(instance, randomValue);
                return randomValue;
            }
            //member is a field
            else if(MemberInfo is FieldInfo fieldInfo)
            {
                Type fieldType = fieldInfo.FieldType;
                var randomValue = RandomFunc();
                memberSetter(instance, randomValue);
                return randomValue;
            }
            else
            {
                throw new ArgumentException("Member is not a property nor a field.");
            }
        }
        /// <summary>
        /// Fill one member with a random contend <br/>
        /// Used to fill first member (unconditional) in a rule chain and to set value against which following conditions are to be evaluated
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        /// <param name="instance">instance, whose member is to be filled</param>
        /// <param name="MemberInfo">info about member to be filled</param>
        /// <param name="RandomFunc">Function, that generates a content to be used as the member value</param>
        /// <returns>strongly typed random value generated to fill the member</returns>
        internal TFirstMember UseRule<TFirstMember>(TClass instance, MemberInfo MemberInfo, Func<object> RandomFunc)
        {
            return (TFirstMember)UseRule(instance, MemberInfo, RandomFunc);
        }
        /// <summary>
        /// Fill one member using InnerFaker
        /// </summary>
        /// <param name="instance">instance, whose member is to be filled</param>
        /// <param name="MemberInfo">info about member to be filled</param>
        /// <param name="innerFaker">faker to be used to generate a random contend of the member</param>
        internal void UseInnerFaker(TClass instance, MemberInfo memberInfo, IInnerFaker innerFaker)
        {
            var memberSetter = Setters[memberInfo];
            if (memberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                object instanceInProperty = null;
                if (innerFaker.CtorUsageFlag == InnerFakerConstructorUsage.PopulateExistingInstance)
                {
                    instanceInProperty = propertyInfo.GetValue(instance);
                }
                // whether instanceInProperty is not null is checked in IInnerFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInProperty);
                memberSetter(instance, o);
            }
            if (memberInfo is FieldInfo fieldInfo)
            {
                Type propertyType = fieldInfo.FieldType;
                object instanceInField = null;
                if (innerFaker.CtorUsageFlag == InnerFakerConstructorUsage.PopulateExistingInstance)
                {
                    instanceInField = fieldInfo.GetValue(instance);
                }
                // whether instanceInField is not null is checked in IInnerFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInField);
                memberSetter(instance, o);
            }
        }
        /// <summary>
        /// Gets MemberInfo about a member that GetMemberLambda returns
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="GetMemberLambda"></param>
        /// <returns></returns>
        private protected static MemberInfo GetMemberFromExpression<TMember>(Expression<Func<TClass, TMember>> GetMemberLambda)
        {
            MemberExpression expression;

            //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
            if (GetMemberLambda.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression)
                {
                    expression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else if (GetMemberLambda.Body is MemberExpression)
            {
                expression = (MemberExpression)GetMemberLambda.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            return expression.Member;
        }

        /// <summary>
        /// Used by AutoFaker and StrictFaker (called in their ctors), scans for all members of TClass not decorated with FakerIgnore attribute <br/>
        /// and stores them in AllNotIgnoredMembers HashSet
        /// </summary>
        static internal void InitializeListOfRandomlyFilledMembers()
        {
            if (AllNotIgnoredMembers is null)
            {
                Type type = typeof(TClass);
                AllNotIgnoredMembers = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && !memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Any())).ToHashSet();
            }
        }
        /// <summary>
        /// If the member does not have a setter compiled for it yet, compiles it and adds it to Setters dictionary 
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="memberInfo"></param>
        static internal void AddSetterIfNew<TMember>(MemberInfo memberInfo)
        {
            if (!Setters.ContainsKey(memberInfo))
            {
                Setters.Add(memberInfo, CreateSetterAction<TMember>(memberInfo));
            }
        }
        /// <summary>
        /// Compiles a Setter for given member of TClass and returns it
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        static internal Action<TClass, object> CreateSetterAction<TMember>(MemberInfo memberInfo)
        {
            //expression representing an instance which member is to  be set
            ParameterExpression targetObjExpr = Expression.Parameter(typeof(TClass), "targetObjParam");
            //expression representing a value to be set to the member
            ParameterExpression valueToSetExpr = Expression.Parameter(typeof(TMember), "valueToSet");
            //expression representing a member
            MemberExpression memberExpression;
            UnaryExpression convertedValue;
            if (memberInfo is PropertyInfo propertyInfo)
            {
                memberExpression = Expression.Property(targetObjExpr, propertyInfo);
                convertedValue = Expression.Convert(valueToSetExpr, propertyInfo.PropertyType);
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                memberExpression = Expression.Field(targetObjExpr, fieldInfo);
                convertedValue = Expression.Convert(valueToSetExpr, fieldInfo.FieldType);
            }
            else
            {
                throw new NotImplementedException("Unexpected");
            }
            BinaryExpression assignExpr = Expression.Assign(memberExpression, convertedValue);

            Action<TClass, TMember> setter = Expression.Lambda<Action<TClass, TMember>>
                (assignExpr, targetObjExpr, valueToSetExpr).Compile();
            return (inst, val) => setter(inst, (TMember)Convert.ChangeType(val, typeof(TMember)));
        }
        /// <summary>
        /// Called when faker is used as an innerFaker
        /// </summary>
        /// <returns> generated instance </returns>
        object IInnerFaker.Generate(object instance)
        {
            switch (this.CtorUsageFlag)
            {
                case InnerFakerConstructorUsage.Parameterless:
                    return this.Generate();
                case InnerFakerConstructorUsage.GivenParameters:
                    return this.Generate(this.CtorParameters);
                case InnerFakerConstructorUsage.PopulateExistingInstance:
                    if (instance is null)
                    {
                        throw new FakerException("InnerFaker set to PopulateExisting instance can only be used to fill member, that is already initialized by instance of particular type.");
                    }
                    return this.Populate((TClass)instance);
                default:
                    throw new NotImplementedException();
            }
        }
        bool IInnerFaker.AllRulesSetDeep()
        {
            return true;
        }

        HashSet<MemberInfo> IInnerFaker.GetAllMembersRequiringRuleDeep()
        {
            return new HashSet<MemberInfo>();
        }
    }
    /// <summary>
    /// which ctor should be used to create instances of TClass when faker is used as inner faker
    /// </summary>
    public enum InnerFakerConstructorUsage
    {
        Parameterless,
        GivenParameters,
        PopulateExistingInstance
    }
}
