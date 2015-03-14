using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.List;
using Generic.Containers.Collections.Set;

namespace KeyboardEPCS.Logic.Inputs
{
    [Serializable]
    public class Keyboard
    {
        readonly static ISet<char> allStandardChars = SetUtil.NewHash(new[]
        {
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' //,  '[', ']' //,'{','}'
            , 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';'//, '\'' //,':','"'
            , 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' //,'<','>','?' 
        });
        public static readonly Keyboard StandardKeyboard = new Keyboard(new[]{10, 10, 10},allStandardChars);
        readonly int[] rowLengths;
        readonly ISet<char> allChars;
        public readonly int PositionCount;
        readonly IDictionary<char, int> charIndexMap = new Dictionary<char, int>();
        readonly IDictionary<KeyboardPosition, int> positionToIndex;
        readonly IDictionary<int, KeyboardPosition> indexToPosition;
 
        public Keyboard(int[] rowLengths, ISet<char> allChars)
        {
            this.rowLengths = rowLengths;
            this.allChars = allChars;
            PositionCount = RowLengths.Sum();

            foreach (var chr in allChars)
                charIndexMap.Add(chr, charIndexMap.Count);

            positionToIndex = new Dictionary<KeyboardPosition, int>();
            indexToPosition = new Dictionary<int, KeyboardPosition>();

            var index = 0;
            foreach(var position in Positions)
            {
                positionToIndex[position] = index;
                indexToPosition[index] = position;
                index++;
            }
        }

        public int[] RowLengths
        {
            get { return rowLengths; }
        }

        public int PositionToPositionIndex(KeyboardPosition pos)
        {
            return positionToIndex[pos];
        }

        public KeyboardPosition PositionIndexToPosition(int index)
        {
            return indexToPosition[index];
        }

        public IEnumerable<KeyboardPosition> Positions
        {
            get { return RowLengths.SelectMany((length,row) => ListUtil.FromTo(0, length - 1).Select(col => new KeyboardPosition(row,col))); }
        }

        public ICollection<char> AllChars { get { return allChars; } }

        public IEnumerable<Tuple<char, char>> CharPairs { get { return AllChars.SelectMany(from => AllChars.Select(to => Tuple.Create(from, to))); } } 

        public int GetCharIndex(char chr)
        {
            return charIndexMap[chr];
        }
    }
}
