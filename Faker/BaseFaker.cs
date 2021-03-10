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
    internal interface IFaker
    {
        internal object Generate(object instance);
        internal bool AllRulesSetDeep();
        HashSet<MemberInfo> GetAllMembersRequiringRuleDeep();
    }
    public partial class BaseFaker<TClass> : IFaker where TClass : class
    {
        /// <summary>
        /// Setters for members of TClass compiled in runtime by Expression.Lambda.Compile
        /// </summary>
        internal static Dictionary<MemberInfo, Action<TClass, object>> Setters { get; set; } = new();
        /// <summary>
        /// Not ignored (by ignore attribute) members of TClass type, used by Strict and Auto Faker instances
        /// </summary>
        static internal HashSet<MemberInfo> AllNotIgnoredMembers { get; set; } = null;

        internal HashSet<MemberInfo> RulelessMembersInstance { get; set; } = new HashSet<MemberInfo>();
        /// <summary>
        /// source of pseudo-random entities
        /// </summary>
        public RandomGenerator Random { get; }
        /// <summary>
        /// ctor to be used to create an instance of TClass when faker is used as inner faker <br/>
        /// (and Generate method is called automatically) <br/>
        /// default is InnerFakerConstructorUsage.Parameterless
        /// </summary>
        public InnerFakerConstructorUsage CtorUsageFlag { get; protected set; } = InnerFakerConstructorUsage.Parameterless;
        /// <summary>
        /// Parameters for TClass constructor used when CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters 
        /// </summary>
        public object[] CtorParameters { get; set; } = new object[] { };
        /// <summary>
        /// affects a behavior of some of the methods on RandomGenerator to produce more culturally adequate values (names, city names...)  
        /// </summary>
        public CultureInfo Culture => this.Random.Culture;
        /// <summary>
        /// Generate call on a faker calls Generate on all its innerFakers 
        /// </summary>
        internal IDictionary<MemberInfo, IFaker> InnerFakers { get; } = new Dictionary<MemberInfo, IFaker>();
        /// <summary>
        /// rules used to generate pseudo-random content
        /// </summary>
        internal IDictionary<MemberInfo, RuleType> Rules { get; } = new Dictionary<MemberInfo, RuleType>();
        /// <summary>
        /// set of members whose content should not be filled by a default random function by AutoFaker <br/>
        /// members get inserted here by calling Ignore(member) in Fakers ctor <br/>
        /// once member is Ignored, it cannot have a RuleFor or InnnerFaker set for it in the same instance of the AutoFaker
        /// </summary>
        internal HashSet<MemberInfo> Ignored { get; } = new HashSet<MemberInfo>();

        //internal HashSet<MemberInfo> TemporarilyRuleless { get; } = new HashSet<MemberInfo>();

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
        ///  new instance of BaseFaker customized to given culture that creates a new instance of RandomGenerator with a given seed
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="info"></param>
        public BaseFaker(ulong seed, CultureInfo info)
        {
            this.Random = new RandomGenerator(seed, info);
        }
        /// <summary>
        /// new instance of BaseFaker customized to given culture that uses existing instance of RandomGenerator <br/>
        /// one instance of random generator can be shared by multiple fakers to save memory <br/>
        /// recommended for innerFakers 
        /// </summary>
        /// <param name="randomGenerator"></param>
        public BaseFaker(RandomGenerator randomGenerator)
        {
            this.Random = randomGenerator;
        }

        internal MemberInfo pendingMember = null;

        /*public UnconditionalMemberFluent<TMember> For<TMember>(Expression<Func<TClass, TMember>> selector)
        {
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that already has a InnerFaker set for it.");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that was already marked as strictly Ignored by calling Ignore method.");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot state multiple unconditional rules for the same member.");
            }
            this.pendingMember = memberInfo;
            AddSetterIfNew<TMember>(memberInfo);

            return new UnconditionalMemberFluent<TMember>(this);
        }
        private void _uncoditionalRule<TMember>(Func<RandomGenerator, TMember> setter)
        {
            if (this.pendingMember is null)
            {
                //TODO: delete when tested
                throw new NotImplementedException("this should never happen");
            }
            MemberInfo memberInfo = this.pendingMember;
            this.pendingMember = null;
            //whether member the member isn't ignored or does not have a InnerFaker set for it already is checked in SetFakerFor method
            
            SimpleRule newRule = new SimpleRule(() => setter(this.Random));
            this.Rules.Add(memberInfo, newRule);
            this.RulelessMembersInstance.Remove(memberInfo);
        }*/

        //TODO: resolve management of MembersToBeFilledInstance in derived Fakers - maybe done
        public RefMemberFluent<TInnerClass> SetFakerFor<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector) where TInnerClass: class
        {
            //TODO: add messages
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }

            this.pendingMember = memberInfo;
            AddSetterIfNew<TInnerClass>(memberInfo);

            return new RefMemberFluent<TInnerClass>(this);
        }

        private void _faker<TInnerClass>(BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            if(this.pendingMember is null)
            {
                //TODO: delete when tested
                throw new NotImplementedException("this should never happen");
            }
            MemberInfo memberInfo = this.pendingMember;
            this.pendingMember = null;
            //whether member the member isn't ignored or does not have a InnerFaker set for it already is checked in SetFakerFor method
            this.InnerFakers.Add(memberInfo, faker);
            this.RulelessMembersInstance.Remove(memberInfo);
        }
        public FirstConditionalMemberFluent<TFirstMember, TFirstMember> For<TFirstMember>(Expression<Func<TClass, TFirstMember>> selector)
        {
            //only the first one of the series on conditional rules in unconditional
            //therefore the For can be set only for a member with no unconditional rule or inner faker set for it
            //multiple conditional rules can be set for a member
            //TODO: add messages
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }
            AddSetterIfNew<TFirstMember>(memberInfo);

            this.pendingMember = memberInfo;

            //resolver initialization for this TFirstMember member
            //creates a new resolver<IFirstMember> that contains the first ConditionPack with this memberInfo an always true condition
            ChainedRuleResolver<TFirstMember> resolver = new ChainedRuleResolver<TFirstMember>(memberInfo);
            this.Rules.Add(memberInfo, resolver);
            RulelessMembersInstance.Remove(memberInfo);

            return new FirstConditionalMemberFluent<TFirstMember, TFirstMember>(this);
        }

        private protected FirstConditionalRuleFluent<TFirstMember> _firtsSetRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            //add Function info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            //CurResolver.AddFunctionToLastRulePack(() => setter(this.Random));
            CurResolver.AddFirstFunc(() => setter(this.Random));

            return new FirstConditionalRuleFluent<TFirstMember>(this);
        }

        private protected ConditionalMemberFluent<TFirstMember, TCurMember> _for<TFirstMember, TCurMember>(MemberInfo memberInfo)
        {
            //TODO: add message
            /*if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("SOME MESSAGE");
            }*/
            AddSetterIfNew<TCurMember>(memberInfo);

            //add MemberInfo info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddMemberToLastRulePack(memberInfo);
            RulelessMembersInstance.Remove(memberInfo);  //makes a member ignored by AutoFaker.RandomlyFillRemainingMembers

            return new ConditionalMemberFluent<TFirstMember, TCurMember>(this);
        }
        private protected ConditionalRuleFluent<TFirstMember> _setRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            //add Function info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleResolver<TFirstMember> CurResolver  = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddFunctionToLastRulePack(() => setter(this.Random));

            return new ConditionalRuleFluent<TFirstMember>(this);
        }

        private protected ConditionFluent<TFirstMember> _when<TFirstMember>(Func<TFirstMember, bool> condition)
        {
            //add Condition info to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddNewCondPackWithCondition(condition);

            return new ConditionFluent<TFirstMember>(this);
        }

        private protected ConditionFluent<TFirstMember> _otherwise<TFirstMember>()
        {
            //add Otherwise condition to the resolver corresponding to pendingMember MemberInfo 
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.AddNewCondPackWithOtherwiseCondition();

            return new ConditionFluent<TFirstMember>(this);
        }

        private protected ChainedRuleResolver<TFirstMember> GetResolverForMemberInfo<TFirstMember>(MemberInfo memberInfo)
        {
            if (!this.Rules.ContainsKey(memberInfo))
            {
                //TODO: delete when tested
                throw new InvalidOperationException("This MemberInfo should be present in Rules dict, flawed implemenation");
            }
            if(this.Rules[memberInfo] is ChainedRuleResolver<TFirstMember> chainedRuleResover)
            {
                return chainedRuleResover;
            }
            throw new InvalidOperationException("Unexpected, this method should only be called with memberInfos corresponding to chainedRules");
        }

        /// <summary>
        /// Adds Rule for how to generate a random content of particular member <br/>
        /// selector and setter must have the same return type
        /// </summary>
        /// <typeparam name="TMember">Type of member to be filled in </typeparam>
        /// <param name="selector">lambda returning member to be filled </param>
        /// <param name="setter">random function to fill in the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to set a RuleFor a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /*public void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            _internalRuleFor(memberInfo, setter);
        }

        internal protected virtual void _internalRuleFor<TMember>(MemberInfo memberInfo, Func<RandomGenerator, TMember> setter)
        {
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that already has a InnerFaker set for it.");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that was already marked as strictly Ignored by calling Ignore method.");
            }
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot state multiple unconditional rules for the same member.");
            }
            SimpleRule newRule = new SimpleRule(() => setter(this.Random));
            this.Rules.Add(memberInfo, newRule);
           
            AddSetterIfNew<TMember>(memberInfo);
        }*/

        /// <summary>
        /// Sets member as Ignored - this member won't be filled by default random function by AutoFaker instances <br/>
        /// </summary>
        /// <typeparam name="TMember">Type of member to be ignored</typeparam>
        /// <param name="selector">lambda returning member to be ignored</param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to Ignore a member that already has a Rule or InnerFaker set for it</exception>
        internal protected void _internalIgnore<TMember>(MemberInfo memberInfo)
        {            
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an RuleFor set for it");
            }
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an InnerFaker set for it");
            }
            //does not throw exception when member info is already present in the HashSet, returns false
            //Ignored method called multiple times with the same member as the parameter won't change the semantics of the program
            //no need to throw the FakerException here
            this.Ignored.Add(memberInfo);
        }
        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /*public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector,
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo= this.GetMemberFromExpression(selector);
            _internalSetFaker(memberInfo, faker);
        }

        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        internal protected virtual void _internalSetFaker<TInnerClass>(MemberInfo memberInfo, 
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot set an InnerFaker for a member that already has a Rule for it.");
            }
            if (this.Ignored.Contains(memberInfo))
            {
                throw new FakerException("You cannot set an InnerFaker for a member that was already marked as strictly Ignored by calling Ignore method.");
            }
            try
            {
                // Store to list of known fakers
                InnerFakers.Add(memberInfo, faker);
            }
            catch (ArgumentException)
            {
                throw new FakerException("You cannot set multiple InnerFakers for the same member.");
            }
            AddSetterIfNew<TInnerClass>(memberInfo);
        }*/
        /// <summary>
        /// Called when faker is used as an innerFaker
        /// </summary>
        /// <returns> generated instance </returns>
        object IFaker.Generate(object instance)
        {
            switch (this.CtorUsageFlag)
            {
                case InnerFakerConstructorUsage.Parameterless:
                    return this.Generate();
                case InnerFakerConstructorUsage.GivenParameters:
                    return this.Generate(this.CtorParameters);
                case InnerFakerConstructorUsage.PopulateExistingInstance:
                    if(instance is null)
                    {
                        throw new FakerException("InnerFaker set to PopulateExisting instance can only be used to fill member, that is already initialized by instance of particular type.");
                    }
                    return this.Populate((TClass)instance);
                default:
                    throw new NotImplementedException();
            }
        }
        bool IFaker.AllRulesSetDeep()
        {
            return true;
        }

        HashSet<MemberInfo> IFaker.GetAllMembersRequiringRuleDeep()
        {
            return new HashSet<MemberInfo>();
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
            if(instance is null)
            {
                throw new FakerException($"Argument of Populate must be existing instance of {typeof(TClass)} type");
            }
            // Use rules
            foreach (var rule in this.Rules)
            {
                if(rule.Value is SimpleRule simpleRule)
                {
                    this.UseRule(instance, rule.Key, simpleRule.Rule);
                }
                if(rule.Value is ChainedRule chainedRule)
                {
                    chainedRule.ResolveChainedRule(instance, this);
                }
            }
            // Use InnerFakers
            foreach (var innerFaker in this.InnerFakers)
            {
                this.UseInnerFaker(instance, innerFaker.Key, innerFaker.Value);
            }
            //if required, fill remaining members with default random functions corresponding to their types
            /*if (this.FillEmptyMembers == UnfilledMembers.DefaultRandomFunc)
            {
                this.RandomlyFillRemainingMembers(instance);
            }*/
            return instance;
        }
        /// <summary>
        /// Fill one member with a random contend
        /// </summary>
        /// <param name="instance">instance, whose member is to be filled</param>
        /// <param name="MemberInfo">info about member to be filled</param>
        /// <param name="RandomFunc">Function, that generates a content to be used as the member value</param>
        internal void UseRule(TClass instance, MemberInfo MemberInfo, Func<object> RandomFunc)
        {
            var memberSetter = Setters[MemberInfo];
            //member is a property
            if(MemberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var o = RandomFunc();
                //var value = Convert.ChangeType(o, propertyType);
                //propertyInfo.SetValue(instance, value);
                memberSetter(instance, o);
            }
            //member is a field
            else if(MemberInfo is FieldInfo fieldInfo)
            {
                Type fieldType = fieldInfo.FieldType;
                var o = RandomFunc();
                //var value = Convert.ChangeType(o, fieldType);
                //fieldInfo.SetValue(instance, value);
                memberSetter(instance, o);
            }
            else
            {
                throw new ArgumentException("Member is not a property nor a field.");
            }
        }
        internal TFirstMember UseRule<TFirstMember>(TClass instance, MemberInfo MemberInfo, Func<object> RandomFunc)
        {
            var memberSetter = Setters[MemberInfo];
            //member is a property
            if (MemberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var randomValue = RandomFunc();
                memberSetter(instance, randomValue);
                return (TFirstMember)randomValue;
            }
            //member is a field
            else if (MemberInfo is FieldInfo fieldInfo)
            {
                Type fieldType = fieldInfo.FieldType;
                var randomValue = RandomFunc();
                memberSetter(instance, randomValue);
                return (TFirstMember)randomValue;
            }
            else
            {
                throw new ArgumentException("Member is not a property nor a field.");
            }
        }
        /// <summary>
        /// Fill one member using InnerFaker
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberInfo"></param>
        /// <param name="innerFaker"></param>
        internal void UseInnerFaker(TClass instance, MemberInfo memberInfo, IFaker innerFaker)
        {
            var memberSetter = Setters[memberInfo];
            if (memberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var instanceInProperty =  propertyInfo.GetValue(instance);
                // null is valid value of instanceInProperty when innerFaker does not have CtorUsageFlag set to PopulateExistingInstance
                // whether instanceInProperty is not null is checked in IFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInProperty);
                //var innerClass = Convert.ChangeType(o, propertyType);
                //propertyInfo.SetValue(instance, innerClass);
                memberSetter(instance, o);
            }
            if (memberInfo is FieldInfo fieldInfo)
            {
                Type propertyType = fieldInfo.FieldType;
                var instanceInField = fieldInfo.GetValue(instance);
                // null is valid value of instanceInProperty when innerFaker does not have CtorUsageFlag set to PopulateExistingInstance
                // whether instanceInProperty is not null is checked if IFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInField);
                //var innerClass = Convert.ChangeType(o, propertyType);
                //fieldInfo.SetValue(instance, innerClass);
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
        /// returns HashSet of all fields and properties of TClass that does not have RuleFor or InnerFaker set in this Faker
        /// </summary>
        /// <returns></returns>
        /*internal HashSet<MemberInfo> GetSetOfMembersToBeFilledByDefaultRandFunc()
        {
            if(AllNotIgnoredMembers is null)
            {
                Type type = typeof(TClass);
                AllNotIgnoredMembers = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Count() == 0)).ToHashSet();
            }
            HashSet<MemberInfo> memberInfos = AllNotIgnoredMembers;
            HashSet<MemberInfo> HasRulefor = this.Rules.Keys.ToHashSet();
            HashSet<MemberInfo> HasSetFaker = this.InnerFakers.Keys.ToHashSet();
            memberInfos.ExceptWith(HasRulefor);
            memberInfos.ExceptWith(HasSetFaker);
            memberInfos.ExceptWith(this.Ignored);
            
            return memberInfos;
        }*/

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
}
