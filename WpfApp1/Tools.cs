using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    public class Tools
    {
        public static bool CheckStrings(Func<string, bool> func, params string[] arr)
        {
            HashSet<bool> bools = new HashSet<bool>();
            foreach (string item in arr)
            {
                bools.Add(func.Invoke(item));
            }
            return bools.All(it => it);
        }
    }
}
