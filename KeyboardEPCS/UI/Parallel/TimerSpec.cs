using System;
using System.Collections.Generic;

namespace KeyboardEPCS.UI
{
    class TimerSpec : IColumnSpec
    {
        DateTime startingTime;
        public void Start()
        {
            startingTime = DateTime.Now;
        }

        public string Caption
        {
            get { return "Time"; }
        }

        public string GetColumnString(IDictionary<KeyboardEPAlgorithm, BetterFoundInfo> infos, BetterFoundInfo info)
        {
            var timeDifference = (info.CurrentTime - startingTime).TotalMilliseconds;
            return timeDifference.ToString();
        }
    }
}