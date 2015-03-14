using System;
using Generic;
using Generic.Common;
using Generic.Containers.Collections.List;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace KeyboardEPCS.Logic
{
    [Serializable]
    public class PositionData : DefaultObject, IPrintable
    {
        char regularChar;
        char? shiftChar;

        public PositionData(char regularChar, char? shiftChar = null)
        {
            this.regularChar = regularChar;
            this.shiftChar = shiftChar;
        }

        public char RegularChar
        {
            get { return regularChar; }
            set { regularChar = value; }
        }

        public char? ShiftChar
        {
            get { return shiftChar; }
            set { shiftChar = value; }
        }

        public Document Print()
        {
            return shiftChar.HasValue 
                ? ListUtil.New(regularChar, (char)shiftChar).Parameters() 
                : regularChar.Print();
        }
    }
}