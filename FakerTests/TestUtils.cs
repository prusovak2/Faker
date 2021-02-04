using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakerTests
{
    public static class TestUtils
    {
        public static string InstanceToString<T>(T instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException();
            }
            Type type = typeof(T);
            string res = type.Name + ":\n\t";
            List<MemberInfo> memberInfos = type.GetMembers().Where(memberInfo => (memberInfo is PropertyInfo || memberInfo is FieldInfo)).ToList();
            foreach (var memInfo in memberInfos)
            {
                res += (memInfo.Name + ": ");
                if (memInfo is PropertyInfo propInfo)
                {
                    var val = propInfo.GetValue(instance);
                    if(val is object)
                    {
                        res += val.ToString();
                    }
                    else
                    {
                        res += "null";
                    }
                }
                else if (memInfo is FieldInfo fieldInfo)
                {
                    var val = fieldInfo.GetValue(instance);
                    if (val is object)
                    {
                        res += val.ToString();
                    }
                    else
                    {
                        res += "null";
                    }
                }
                else
                {
                    throw new NotImplementedException("Unexpected");
                }
                res += "\n\t";
            }
            return res;
        }
        public static void IncInDic<T>(Dictionary<T, int> dic, T member)
        {
            if (dic.ContainsKey(member))
            {
                dic[member]++;
            }
            else
            {
                dic.Add(member, 1);
            }
        }

        public static void CheckDic<T>(Dictionary<T, int> dic, int unwantedValue)
        {
            foreach (var item in dic)
            {
                if (dic[item.Key] == unwantedValue)
                {
                    Assert.Fail();
                }
            }
        }
    }
}
