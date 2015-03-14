using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Generic
{
    public class DebugUtil
    {
        public static bool False { get { return false; } }
        public static bool True { get { return true; } }

        [Conditional("DEBUG")]
        public static void AssertNoException([InstantHandle]Action action)
        {
            action();
        }
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string failMessage = "")
        {
            Assert(() => condition, failMessage);
        }

        [Conditional("DEBUG")]
        public static void Assert(Func<bool> condition, string failMessage = "")
        {
            if (!condition())
                throw new Exception(failMessage);
        }
    }
}
