using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generic.Common;

namespace Generic
{
    public static class StringUtil
    {
        public static string StartUpperCase(this string s)
        {
            return new string(char.ToUpper(s[0]).Singleton().Concat(s.Skip(1)).ToArray());
        }
    }
}
