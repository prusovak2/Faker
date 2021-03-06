﻿using System;
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
    public partial class AutoFaker<TClass> : BaseFaker<TClass>, IInnerFaker where TClass : class
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
        /// <summary>
        /// selects a member of TClass to have an unconditional rule set for it
        /// </summary>
        /// <typeparam name="TFirstMember">Type of member</typeparam>
        /// <param name="selector">lambda returning a member</param>
        /// <returns>Fluent syntax helper</returns>
        public new FirstMemberAutoFluent<TFirstMember> For<TFirstMember>(Expression<Func<TClass, TFirstMember>> selector)
        {
            base.For<TFirstMember>(selector);
            return new FirstMemberAutoFluent<TFirstMember>(this);
        }
        /// <summary>
        /// selects a member of a TClass to have a conditional rule set for it
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of member to have an unconditional rule set for it</typeparam>
        /// <param name="memberInfo">member to have a rule set</param>
        /// <returns>fluent syntax helper</returns>
        protected new MemberAutoFluent<TFirstMember, TCurMember> _for<TFirstMember, TCurMember>(MemberInfo memberInfo)
        {
            base._for<TFirstMember, TCurMember>(memberInfo);
            return new MemberAutoFluent<TFirstMember, TCurMember>(this);
        }
        /// <summary>
        /// Sets unconditional rule for a member <br/>
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        /// <param name="setter"></param>
        /// <returns>fluent syntax helper</returns>
        protected new FirstRuleAutoFluent<TFirstMember> _firtsSetRule<TFirstMember>(Func<RandomGenerator, TFirstMember> setter)
        {
            base._firtsSetRule<TFirstMember>(setter);
            return new FirstRuleAutoFluent<TFirstMember>(this);
        }
        /// <summary>
        /// Sets a conditional rule for a member
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of member to have an unconditional rule set for it</typeparam>
        /// <param name="setter">function to be used to fill a member</param>
        /// <returns>fluent syntax helper</returns>
        protected virtual new RuleAutoFluent<TFirstMember> _setRule<TFirstMember, TCurMember>(Func<RandomGenerator, TCurMember> setter)
        {
            base._setRule<TFirstMember, TCurMember>(setter);
            return new RuleAutoFluent<TFirstMember>(this);
        }
        /// <summary>
        /// Instead of setting an conditional rule for this member, sets member ignored 
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <returns>fluent syntax helper</returns>
        internal RuleAutoFluent<TFirstMember> _chainedIgnore<TFirstMember>()
        {
            //add pending member to TemporarilyIgnored
            // mark cur ConditionPack ignored
            ChainedRuleResolver<TFirstMember> CurResolver = GetResolverForMemberInfo<TFirstMember>(this.pendingMember);
            CurResolver.SetLastRulePackIgnored();
            return new RuleAutoFluent<TFirstMember>(this);
        }
        /// <summary>
        /// Sets an condition using value generated by an unconditional rule at the beginning of this rule chain to be evaluated  
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <param name="condition"></param>
        /// <returns>fluent syntax helper</returns>
        protected new ConditionAutoFluent<TFirstMember> _when<TFirstMember>(Func<TFirstMember, bool> condition)
        {
            base._when<TFirstMember>(condition);
            return new ConditionAutoFluent<TFirstMember>(this);
        }
        /// <summary>
        /// sets an Otherwise condition, it is satisfied if and only if any of .When conditions preceding this .Otherwise had not been satisfied
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in this rule chain</typeparam>
        /// <returns>fluent syntax helper</returns>
        protected new ConditionAutoFluent<TFirstMember> _otherwise<TFirstMember>()
        {
            base._otherwise<TFirstMember>();
            return new ConditionAutoFluent<TFirstMember>(this);
        }
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
        /// and recursively creates and set similar AutoFakers for members of user defined class types <br/>
        /// created Faker respects FakerIgnore attributes <br/>
        /// All user defined types appearing as member in hierarchy of TClass must have public parameterless ctor <br/>
        /// heavy use of reflection!
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TClass> CreateAutoFaker()
        {
            return AutoFakerCreator.CreateAutoFaker<TClass>();
        }
        /// <summary>
        /// Used by CreateAutoFaker to create setters without a necessity to know the type of the members
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
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

    internal static class AutoFakerCreator
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
                faker.InnerFakers.Add(memberInfo, (IInnerFaker)memberAutoFaker);
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
