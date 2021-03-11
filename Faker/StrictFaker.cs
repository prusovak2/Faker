using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker
{
    public class StrictFaker<TClass> : BaseFaker<TClass>, IInnerFaker where TClass : class
    {
        static StrictFaker()
        {
            InitializeListOfRandomlyFilledMembers();
        }
        /// <summary>
        /// new instance of StrictFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        public StrictFaker() : base()
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of StrictFaker customized to a given Culture that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        /// <param name="info"></param>
        public StrictFaker(CultureInfo info) : base(info)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of StrictFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        /// <param name="seed"></param>
        public StrictFaker(ulong seed) : base(seed)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// new instance of StrictFaker customized to a given Culture that creates a new instance of RandomGenerator with a given seed <br/>
        /// this Faker requires to have Rules or InnerFakers set for all TClass members before Generate method can be called
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="info"></param>
        public StrictFaker(ulong seed, CultureInfo info) : base(seed, info)
        {
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
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
            this.RulelessMembersInstance = new HashSet<MemberInfo>(AllNotIgnoredMembers);
        }
        /// <summary>
        /// indicates whether all members of this instance of TClass do have a Rule or an InnerFaker set for them <br/>
        /// does not recursively check all inner StrictFakers <br/>
        /// is false, attempt to call .Generate() or .Popuplate() on this instance of the StrictFaker will result in FakerException
        /// </summary>
        /// <returns></returns>
        public bool AllRulesSetShallow()
        {
            return !this.RulelessMembersInstance.Any();
        }
        /// <summary>
        /// Internal method to be called on InnerFakers  <br/>
        /// indicates whether all members of this instance of TClass as well as all members of members of TClass with a StrictFaker set for them <br/>
        /// do have a Rule or an InnerFaker set for them <br/>
        /// is false, attempt to call .Generate() or .Popuplate() on this instance of the StrictFaker will result in FakerException
        /// </summary>
        /// <returns></returns>
        bool IInnerFaker.AllRulesSetDeep()
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
            return ((IInnerFaker) this).AllRulesSetDeep();
        }
        /// <summary>
        /// returns HashSet of the members of this instance of TClass that require a Rule or a InnerFaker to be set for them <br/>
        /// does not recursively scan all inner StrictFakers
        /// </summary>
        /// <returns></returns>
        public HashSet<MemberInfo> GetAllMembersRequiringRuleShallow()
        {
            HashSet<MemberInfo> toReturn = new HashSet<MemberInfo>();
            foreach (var item in this.RulelessMembersInstance)
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
        HashSet<MemberInfo> IInnerFaker.GetAllMembersRequiringRuleDeep()
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
            return ((IInnerFaker)this).GetAllMembersRequiringRuleDeep();
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
            if (!this.AllRulesSetShallow())
            {
                throw new FakerException("IgnoreFaker must have RuleFor or InnerFaker set for every member of TClass or the members must be marked ignored before Generate method is called.");
            }
            TClass PopulatedInstance = base._internal_populate(instance);
            return PopulatedInstance;
        }
    }
}
