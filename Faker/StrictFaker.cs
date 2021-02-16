using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker
{
    public class StrictFaker<TClass> : BaseFaker<TClass>, IFaker where TClass : class
    {
        /// <summary>
        /// new instance of StrictFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        public StrictFaker() : base()
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of StrictFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        /// <param name="seed"></param>
        public StrictFaker(ulong seed) : base(seed)
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of StrictFaker that uses existing instance of RandomGenerator <br/>
        /// one instance of random generator can be shared by multiple fakers to save memory <br/>
        /// recommended for innerFakers <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        /// <param name="randomGenerator"></param>
        public StrictFaker(RandomGenerator randomGenerator) : base(randomGenerator)
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// indicates whether all members of this instance of TClass do have a Rule or an InnerFaker set for them <br/>
        /// does not recursively check all inner StrictFakers <br/>
        /// is false, attempt to call .Generate() or .Popuplate() on this instance of the StrictFaker will result in FakerException
        /// </summary>
        /// <returns></returns>
        public bool AllRulesSetShallow()
        {
            return !this.MembersToBeFilledDefaultly.Any();
        }

        /// <summary>
        /// Internal method to be called on InnerFakers  <br/>
        /// indicates whether all members of this instance of TClass as well as all members of members of TClass with a StrictFaker set for them <br/>
        /// do have a Rule or an InnerFaker set for them <br/>
        /// is false, attempt to call .Generate() or .Popuplate() on this instance of the StrictFaker will result in FakerException
        /// </summary>
        /// <returns></returns>
        bool IFaker.AllRulesSetDeep()
        {
            bool allFiled = this.AllRulesSetShallow();
            if (!allFiled)
            {
                return allFiled;
            }
            foreach (var innerFaker in this.InnerFakers)
            {
                allFiled = allFiled & innerFaker.Value.AllRulesSetDeep();
            }
            return allFiled;
        }

        /// <summary>
        /// indicates whether all members of this instance of TClass as well as all members of members of TClass with a StrictFaker set for them <br/>
        /// do have a Rule or an InnerFaker set for them <br/>
        /// is false, attempt to call .Generate() or .Popuplate() on this instance of the StrictFaker will result in FakerException
        /// </summary>
        /// <returns></returns>
        public bool AllRulesSetDeep()
        {
            return ((IFaker) this).AllRulesSetDeep();
        }

        /// <summary>
        /// returns HashSet of the members of this instance of TClass that require a Rule or a InnerFaker to be set for them <br/>
        /// does not recursively scan all inner StrictFakers
        /// </summary>
        /// <returns></returns>
        public HashSet<MemberInfo> GetAllMembersRequiringRuleShallow()
        {
            HashSet<MemberInfo> toReturn = new HashSet<MemberInfo>();
            foreach (var item in this.MembersToBeFilledDefaultly)
            {
                toReturn.Add(item);
            }
            return toReturn;
        }
        /// <summary>
        /// Internal method to be called on InnerFakers  <br/>
        /// returns HashSet of the members of this instance of TClass that require a Rule or a InnerFaker to be set for them <br/>
        /// and scans all the members with StrictFaker set for them recursively for all other members requiring a Rule or an InnerFaker <br/>
        /// in the whole tree of members beneath this instance 
        /// </summary>
        /// <returns></returns>
        HashSet<MemberInfo> IFaker.GetAllMembersRequiringRuleDeep()
        {
            HashSet<MemberInfo> members = this.GetAllMembersRequiringRuleShallow();
            foreach (var innerFaker in InnerFakers)
            {
                members.UnionWith(innerFaker.Value.GetAllMembersRequiringRuleDeep());
            }
            return members;
        }

        /// <summary>
        /// returns HashSet of the members of this instance of TClass that require a Rule or a InnerFaker to be set for them <br/>
        /// and scans all the members with StrictFaker set for them recursively for all other members requiring a Rule or an InnerFaker <br/>
        /// in the whole tree of members beneath this instance 
        /// </summary>
        /// <returns></returns>
        public HashSet<MemberInfo> GetAllMembersRequiringRuleDeep()
        {
            return ((IFaker)this).GetAllMembersRequiringRuleDeep();
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
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            _internalRuleFor(memberInfo, setter);
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
            MemberInfo memberInfo = GetMemberFromExpression(selector);
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
            MemberInfo memberInfo = GetMemberFromExpression(selector);
            _internalSetFaker(memberInfo, faker);
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
            if (!this.AllRulesSetShallow())
            {
                throw new FakerException("IgnoreFaker must have RuleFor or InnerFaker set for every member of TClass or the members must be marked ignored before Generate method is called.");
            }
            TClass PopulatedInstance = base._internal_populate(instance);
            return PopulatedInstance;
        }
    }
}
