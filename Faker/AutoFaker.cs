using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    /// <summary>
    /// Faker that fills all members of TClass of basic types with no RuleFor set for them by calling a default random function for particular type 
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public class AutoFaker<TClass> : BaseFaker<TClass>, IFaker where TClass : class
    {
        /// <summary>
        /// new instance of AutoFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        public AutoFaker() : base()
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of AutoFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="seed"></param>
        public AutoFaker(ulong seed) : base(seed)
        {
            InitializeListOfRandomlyFilledMembers();
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
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// Adds Rule for how to generate a random content of particular member <br/>
        /// selector and setter must have the same return type
        /// </summary>
        /// <typeparam name="TMember">Type of member to be filled in </typeparam>
        /// <param name="selector">lambda returning member to be filled </param>
        /// <param name="setter">random function to fill in the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to set a RuleFor a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        public new void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalRuleFor(memberInfo, setter);
        }

        protected internal sealed override void _internalRuleFor<TMember>(MemberInfo memberInfo, Func<RandomGenerator, TMember> setter)
        {
            base._internalRuleFor(memberInfo, setter);
            MembersToBeFilledDefaultly.Remove(memberInfo);
        }

        /// <summary>
        /// Sets member as Ignored - this member won't be filled by default random function by AutoFaker instances <br/>
        /// </summary>
        /// <typeparam name="TMember">Type of member to be ignored</typeparam>
        /// <param name="selector">lambda returning member to be ignored</param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to Ignore a member that already has a Rule or InnerFaker set for it</exception>
        public void Ignore<TMember>(Expression<Func<TClass, TMember>> selector)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            MembersToBeFilledDefaultly.Remove(memberInfo);
            base._internalIgnore<TMember>(memberInfo);
        }

        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        public new void SetFaker<TInnerClass>(
            Expression<Func<TClass, TInnerClass>> selector,
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalSetFaker(memberInfo, faker);
        }

        protected internal sealed override void _internalSetFaker<TInnerClass>(MemberInfo memberInfo, BaseFaker<TInnerClass> faker)
        {
            base._internalSetFaker(memberInfo, faker);
            MembersToBeFilledDefaultly.Remove(memberInfo);
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

        protected internal sealed override TClass _internal_populate(TClass instance)
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
            HashSet<MemberInfo> membersToFill = this.MembersToBeFilledDefaultly;
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
    }

    internal class AutoFakerCreator
    {
        /// <summary>
        /// creates AutoFaker for given type that uses default random functions to fill members of basic types <br/>
        /// and recursively creates and set similar AutoFakers for members of user defined class types
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TMember> CreateAutoFaker<TMember>() where TMember : class
        {
            AutoFaker<TMember> faker = new AutoFaker<TMember>();
            Type type = typeof(TMember);
            List<MemberInfo> memberInfos = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && (IsUserDefinedClassType(memberInfo)) && (memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Count() == 0))).ToList();
            foreach (var memberInfo in memberInfos)
            {
                if (faker.Ignored.Contains(memberInfo))
                {
                    continue;
                }
                Type memberType = GetTypeFromMemberInfo(memberInfo);
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
