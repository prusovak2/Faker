using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace FakerTests
{
    public static class TestUtils
    {
        public static string InstanceToString<T>(T instance)
        {
            Type type = typeof(T);
            string res = type.Name + ":\n\t";
            List<MemberInfo> memberInfos = type.GetMembers().Where(memberInfo => (memberInfo is PropertyInfo || memberInfo is FieldInfo)).ToList();
            foreach (var memInfo in memberInfos)
            {
                res += (memInfo.Name + ": ");
                if (memInfo is PropertyInfo propInfo)
                {
                    res += propInfo.GetValue(instance).ToString();
                }
                else if (memInfo is FieldInfo fieldInfo)
                {
                    res += fieldInfo.GetValue(instance).ToString();
                }
                else
                {
                    throw new NotImplementedException("Unexpected");
                }
                res += "\n\t";
            }
            return res;
        }
    }
}
