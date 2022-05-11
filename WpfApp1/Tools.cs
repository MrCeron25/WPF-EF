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

        public static TData BinarySearch<TData>(List<TData> data, string key, int left, int right, Func<TData, string> func)
        {
            int mid = left + ((right - left) / 2);
            if (left >= right)
            {
                return data[-(1 + left)];
            }

            if (func.Invoke(data[mid]) == key)
            {
                return data[mid];
            }
            else
            {
                return string.Compare(func.Invoke(data[mid]), key) > 0
                    ? BinarySearch(data, key, left, mid, func)
                    : BinarySearch(data, key, mid + 1, right, func);
            }
        }
    }
}
