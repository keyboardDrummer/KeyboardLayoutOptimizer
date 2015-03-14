using System;
using System.Collections.Generic;

namespace KeyboardEPCS.Logic
{
    public class KeyboardConfiguration
    {
        readonly Dictionary<Char, KeyboardPosition> data;
        public KeyboardConfiguration() { data = new Dictionary<Char, KeyboardPosition>(); }

        public KeyboardPosition this[Char c]
        {
            get { return Data[c]; }
        }

        public Dictionary<Char, KeyboardPosition> Data { get { return data; } }

        public String Print2() { return ""; }

        public String Print()
        {
            Char[] result = new Char[63];
            int index = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    result[index] = ' ';
                    index += 2;
                }
                result[index] = '\r';
                result[index + 1] = '\n';
                index += 3;
            }
            foreach (Char c in "abcdefghijklmnopqrstuvwxyz;,./")
            {
                KeyboardPosition pos = data[c];
                index = (pos.Row - 1) * 21 + pos.Column * 2;
                result[index] = c;
            }
            return new String(result);
        }
    }
}