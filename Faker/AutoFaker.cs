using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    /// <summary>
    /// Faker that fills all members of TClass of basic types with no RuleFor set for them by calling a default random function for particular type 
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public partial class AutoFaker<TClass> : BaseFaker<TClass>, IFaker where TClass : class
    {
        static AutoFaker()
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of AutoFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        public AutoFaker() : base()
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of AutoFaker customized to a given Culture, that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="info"></param>
        public AutoFaker(CultureInfo info) : base(info)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of AutoFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="seed"></param>
        public AutoFaker(ulong seed) : base(seed)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of AutoFaker customized to a given culture, that creates a new instance of RandomGenerator with a given seed <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="info"></param>
        public AutoFaker(ulong seed, CultureInfo info) : base(seed, info)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of AutoFaker that uses existing instance of RandomGenerator <br/>
        /// one instance of random generator can be shared by multiple fakers to save memory <br/>
        /// recommended for innerFakers <br/>
        /// this Faker fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="randomGenerator"></param>
        public AutoFaker(RandomGenerator randomGenerator) : base(randomGenerator)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }

        public new FirstConditionalMemberAutoFluent<TFirstMember, TFirstMember> For<TFirstMember>(Expression<Func<TClass, TFirstMember>> selector)
        {
            base.For<TFirstMember>(selector);
            return new FirstConditionalMemberAutoFluent<TFirstMember, TFirstMember>(this);
        }

        protected new ConditionalMemberAutoFluent<TFirstMember, TCurMember> _for<TFirstMember, TCurMember>(MemberInfo memberInfo)
        {
            base._for<TFirstMember, TCurMember>(memberInfo);
            return new ConditionalMemberAutoFluent<TFirstMember, TCurMember>(this);
        }
        protected new FirstConditionalRuleAutoFluent<TFirstMember> _firtsSetRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            base._firtsSetRule<TFirstMember, TCurMember>(setter);
            return new FirstConditionalRuleAutoFluent<TFirstMember>(this);
        }

        protected virtual new ConditionalRuleAutoFluent<TFirstMember> _setRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            base._setRule<TFirstMember, TCurMember>(setter);
            return new ConditionalRuleAutoFluent<TFirstMember>(this);
        }
        internal ConditionalRuleAutoFluent<TFirstMember> _autoIgnore<TFirstMember>()
        {
            //add pending member to TemporarilyIgnored
            // mark cur ConditionPack ignored
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.SetLastRulePackIgnored();
            //MemberInfo curMember = CurResolver.GetLastMember();
            //this.TemporarilyIgnored.Add(curMember);
            return new ConditionalRuleAutoFluent<TFirstMember>(this);
        }
        
        protected new ConditionAutoFluent<TFirstMember> _when<TFirstMember>(Func<TFirstMember, bool> condition)
        {
            base._when<TFirstMember>(condition);
            return new ConditionAutoFluent<TFirstMember>(this);
        }

        protected new ConditionAutoFluent<TFirstMember> _otherwise<TFirstMember>()
        {
            base._otherwise<TFirstMember>();
            return new ConditionAutoFluent<TFirstMember>(this);
        }

        /// <summary>
        /// Adds Rule for how to generate a random content of particular member <br/>
        /// selector and setter must have the same return type
        /// </summary>
        /// <typeparam name="TMember">Type of member to be filled in </typeparam>
        /// <param name="selector">lambda returning member to be filled </param>
        /// <param name="setter">random function to fill in the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to set a RuleFor a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /*public new void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalRuleFor(memberInfo, setter);
        }

        protected internal sealed override void _internalRuleFor<TMember>(MemberInfo memberInfo, Func<RandomGenerator, TMember> setter)
        {
            base._internalRuleFor(memberInfo, setter);
            RulelessMembersInstance.Remove(memberInfo);
        }*/
        /// <summary>
        /// Sets member as Ignored - this member won't be filled by default random function by AutoFaker instances <br/>
        /// </summary>
        /// <typeparam name="TMember">Type of member to be ignored</typeparam>
        /// <param name="selector">lambda returning member to be ignored</param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to Ignore a member that already has a Rule or InnerFaker set for it</exception>
        public void Ignore<TMember>(Expression<Func<TClass, TMember>> selector)
        {
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            RulelessMembersInstance.Remove(memberInfo);
            base._internalIgnore<TMember>(memberInfo);
        }
        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        /*public new void SetFaker<TInnerClass>(
            Expression<Func<TClass, TInnerClass>> selector,
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalSetFaker(memberInfo, faker);
        }

        protected internal sealed override void _internalSetFaker<TInnerClass>(MemberInfo memberInfo, BaseFaker<TInnerClass> faker)
        {
            base._internalSetFaker(memberInfo, faker);
            RulelessMembersInstance.Remove(memberInfo);
        }*/

        /// <summary>
        /// Use rules to fill the instance with a random content
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>The same instance filled with a random content based on rules</returns>
        public new TClass Populate(TClass instance)
        {
            return this._internal_populate(instance);
        }

        private protected sealed override TClass _internal_populate(TClass instance)
        {
            TClass PopulatedInstance = base._internal_populate(instance);
            RandomlyFillRemainingMembers(PopulatedInstance);
            //this.TemporarilyRuleless.Clear();
            return PopulatedInstance;
        }
        /// <summary>
        /// Fills members with no RuleFor or InnerFaker set for them with random values provided by default random functions corresponding to member types
        /// </summary>
        /// <param name="instance"></param>
        internal void RandomlyFillRemainingMembers(TClass instance)
        {
            HashSet<MemberInfo> membersToFill = this.RulelessMembersInstance;
            foreach (var member in membersToFill)
            {
                if (member is PropertyInfo propertyInfo)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    var sampleInstance = propertyType.GetSampleInstance(); // get sample instance of type to be used in GetDefaultRandomFuncForType()
                    var fillingFunc = this.Random.GetDefaultRandomFuncForType(sampleInstance);
                    if (fillingFunc is null)
                    {
                        //no Default Random Func for this type of property (property is not of supported basic type)
                        continue;
                    }
                    var o = fillingFunc();
                    var value = Convert.ChangeType(o, propertyType);
                    propertyInfo.SetValue(instance, value);
                }
                if (member is FieldInfo fieldInfo)
                {
                    Type fieldType = fieldInfo.FieldType;
                    var sampleInstance = fieldType.GetSampleInstance();
                    var fillingFunc = this.Random.GetDefaultRandomFuncForType(sampleInstance);
                    if (fillingFunc is null)
                    {
                        //no Default Random Func for this type of field (field is not of supported basic type)
                        continue;
                    }
                    var o = fillingFunc();
                    var value = Convert.ChangeType(o, fieldType);
                    fieldInfo.SetValue(instance, value);
                }
            }
        }
        /// <summary>
        /// creates AutoFaker for given type that uses default random functions to fill members of basic types <br/>
        /// and recursively creates and set similar AutoFakers for members of user defined class types
        /// created Faker respects FakerIgnore attributes
        /// All user defined types appearing as member in hierarchy of TClass must have public parameterless ctor
        /// heavy use of reflection!
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TClass> CreateAutoFaker()
        {
            return AutoFakerCreator.CreateAutoFaker<TClass>();
        }
        static internal Action<TClass, object> CreateSetterActionNotStroglyTyped(MemberInfo memberInfo)
        {
            //expression representing an instance which member is to  be set
            ParameterExpression targetObjExpr = Expression.Parameter(typeof(TClass), "targetObjParam");
            //expression representing a value to be set to the member
            ParameterExpression valueToSetExpr = Expression.Parameter(typeof(object), "valueToSet");
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

            Action<TClass, object> setter = Expression.Lambda<Action<TClass, object>>
                (assignExpr, targetObjExpr, valueToSetExpr).Compile();
            return setter;
        }

    }

    internal class AutoFakerCreator
    {
        /// <summary>
        /// creates AutoFaker for given type that uses default random functions to fill members of basic types <br/>
        /// and recursively creates and set similar AutoFakers for members of user defined class types
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TClass> CreateAutoFaker<TClass>() where TClass : class
        {
            AutoFaker<TClass> faker = new AutoFaker<TClass>();
            Type type = typeof(TClass);
            List<MemberInfo> memberInfos = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && (IsUserDefinedClassType(memberInfo)) && (memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Count() == 0))).ToList();
            foreach (var memberInfo in memberInfos)
            {
                if (faker.Ignored.Contains(memberInfo))
                {
                    continue;
                }
                Type memberType = GetTypeFromMemberInfo(memberInfo);
                if (!BaseFaker<TClass>.Setters.ContainsKey(memberInfo))
                {
                    var setter = AutoFaker<TClass>.CreateSetterActionNotStroglyTyped(memberInfo);
                    BaseFaker<TClass>.Setters.Add(memberInfo, setter);
                }
                var memberAutoFaker = typeof(AutoFakerCreator).GetMethod("CreateAutoFaker").MakeGenericMethod(memberType).Invoke(null, null);
                faker.InnerFakers.Add(memberInfo, (IFaker)memberAutoFaker);
            }
            return faker;
        }
        

        /// <summary>
        /// if member is property or field, returns a type of corresponding property or field
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static Type GetTypeFromMemberInfo(MemberInfo memberInfo)
        {
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }
            else
            {
                throw new NotImplementedException("This method is expected to be used only for properties and fields");
            }
        }
        /// <summary>
        /// determines whether the member has type that for which an inner AutoFaker should be generated while creating AutoFaker via CreateAutoFaker method 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static bool IsUserDefinedClassType(MemberInfo memberInfo)
        {
            Type memberType = GetTypeFromMemberInfo(memberInfo);
            if (typeof(Delegate).IsAssignableFrom(memberType) || typeof(IEnumerable).IsAssignableFrom(memberType))
            {
                return false;
            }
            if (memberType == typeof(string))
            {
                return false;
            }

            return memberType.IsClass;
        }
    }
}
