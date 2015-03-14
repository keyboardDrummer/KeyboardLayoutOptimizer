using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Generic.Containers.Collections.List.Final;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace KeyboardEPCS.Logic.Inputs
{
    [Serializable]
    public class KeyboardLayout : FixedSizeList<PositionData>, IPrintable
    {
        readonly Keyboard keyboard;
        readonly PositionData[][] positionChars;

        [NonSerialized]
        IDictionary<char, KeyboardPosition> charToPosition = new Dictionary<char, KeyboardPosition>();

        KeyboardLayout(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            positionChars = this.keyboard.RowLengths.Select(length => new PositionData[length]).ToArray();
        }

        IDictionary<char, KeyboardPosition> GetCharToPositionDictionary(Keyboard keyboard)
        {
            var result = new Dictionary<char, KeyboardPosition>();
            foreach (var position in keyboard.Positions)
                SetPositionInfo(result, position);
            return result;
        }

        void SetPositionInfo(IDictionary<char, KeyboardPosition> result, KeyboardPosition position)
        {
            var positionInfo = this[position];
            result[positionInfo.RegularChar] = position;
            if (positionInfo.ShiftChar.HasValue)
                result[positionInfo.ShiftChar.Value] = position;
        }

        public static KeyboardLayout Random(Keyboard keyboard)
        {
            var result = new KeyboardLayout(keyboard);
            var random = new Random();
            var chars = keyboard.AllChars.ToList();
            for (var i = 0; i < result.Count; i++)
            {
                var charIndex = random.Next(0, chars.Count);
                result[i] = new PositionData(chars[charIndex]);
                chars.RemoveAt(charIndex);
            }
            return result;
        }

        public KeyboardLayout(IEnumerable<char> chars, Keyboard keyboard)
            : this(keyboard)
        {
            var rator = chars.GetEnumerator();
            for (var row = 0; row < this.keyboard.RowLengths.Length; row++)
            {
                for (var col = 0; col < this.keyboard.RowLengths[row]; col++)
                {
                    rator.MoveNext();
                    positionChars[row][col] = new PositionData(rator.Current);
                }
            }
            charToPosition = GetCharToPositionDictionary(keyboard);
        }

        [OnSerialized]
        void FillCharToPosition(StreamingContext context)
        {
            charToPosition = GetCharToPositionDictionary(keyboard);
        }

        public KeyboardLayout Clone()
        {
            var result = new KeyboardLayout(keyboard);
            foreach (var position in keyboard.Positions)
                result[position] = this[position];
            result.charToPosition = new Dictionary<char, KeyboardPosition>(charToPosition);
            return result;
        }

        public int TotalPositions
        {
            get { return keyboard.RowLengths.Sum(); }
        }

        public Keyboard Keyboard
        {
            get { return keyboard; }
        }

        public PositionData this[KeyboardPosition position]
        {
            get { return positionChars[position.Row][position.Column]; }
            set { positionChars[position.Row][position.Column] = value;
                SetPositionInfo(charToPosition, position);
            }
        }

        public override int Count
        {
            get { return keyboard.PositionCount; }
        }

        public override PositionData this[int positionIndex]
        {
            get { return this[keyboard.PositionIndexToPosition(positionIndex)]; }
            set { this[keyboard.PositionIndexToPosition(positionIndex)] = value; }
        }

        public void CheckConsistency()
        {
            foreach (var chr in keyboard.AllChars)
                GetCharPosition(chr);
        }

        public KeyboardPosition GetCharPosition(char chr)
        {
            return charToPosition[chr];
        }

        public Document Print()
        {
            var writer = new DocumentWriter();
            foreach (var row in positionChars)
                writer.WriteLine(row.Seperated(' '));
            return writer.ToDocument();
        }

        public static KeyboardLayout QWERTY
        {
            get
            {
                Char[] input =
                    {
                        'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'
                        , 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';'
                        , 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/'
                    };
                return new KeyboardLayout(input, Keyboard.StandardKeyboard);
            }
        }

        public static KeyboardLayout Dvorak
        {
            get
            {
                Char[] input = {
                    '/', ',', '.', 'p', 'y', 'f', 'g', 'c', 'r'
                    , 'l', 'a', 'o', 'e', 'u', 'i', 'd', 'h'
                    , 't', 'n', 's', ';', 'q', 'j', 'k', 'x'
                    , 'b', 'm', 'w', 'v', 'z'
                };
                return new KeyboardLayout(input, Keyboard.StandardKeyboard);
            }
        }

        public static KeyboardLayout Abc
        {
            get
            {
                Char[] input =
                    {
                        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'
                        , 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't'
                        , 'u', 'v', 'w', 'x', 'y', 'z', ';', ',', '.', '/'
                    };
                return new KeyboardLayout(input, Keyboard.StandardKeyboard);
            }
        }
    }
}
