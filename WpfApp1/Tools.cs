using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

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

        public static string GetCrypt(string text)
        {
            try
            {
                return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(text)));
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка получения хеш значения.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool CheckPassword(string password)
        {
            char[] specialCharactersArray = "%!@#$%^&*()?/>.<,:;'\\|}]{[_~`+=-\"".ToCharArray();
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(specialCharactersArray.Contains);
        }
    }
}
