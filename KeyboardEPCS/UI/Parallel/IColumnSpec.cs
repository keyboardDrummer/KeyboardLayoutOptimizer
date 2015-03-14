using System.Collections.Generic;

namespace KeyboardEPCS.UI
{
    interface IColumnSpec
    {
        void Start();
        string Caption { get; }
        string GetColumnString(IDictionary<KeyboardEPAlgorithm, BetterFoundInfo> infos, BetterFoundInfo info);
    }
}