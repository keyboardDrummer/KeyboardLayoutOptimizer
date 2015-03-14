using System;
using System.Linq;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Inputs.Transitions;

namespace KeyboardEPCS.Logic.Transitions
{
    [Serializable]
    public class TransitionTimeBuilder : IPrintable
    {
        readonly Keyboard keyboard;
        readonly TransitionInfo[,] data;
        
        TransitionTimeBuilder(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            data = new TransitionInfo[this.keyboard.PositionCount,this.keyboard.PositionCount];
            for (var i = 0; i < this.keyboard.PositionCount; i++)
            {
                for (var j = 0; j < this.keyboard.PositionCount; j++)
                    data[i, j] = new TransitionInfo();
            }
        }

        static double[,] KlauslerSimpleTable
        {
            get
            {
                var result = new Double[6,6];
                result[0, 0] = 403;
                result[0, 1] = 198;
                result[0, 2] = 311;
                result[0, 3] = 351;
                result[0, 4] = 547;
                result[0, 5] = 236;
                result[1, 0] = 271;
                result[1, 1] = 193;
                result[1, 2] = 157;
                result[1, 3] = 237;
                result[1, 4] = 614;
                result[1, 5] = 320;
                result[2, 0] = 287;
                result[2, 1] = 119;
                result[2, 2] = 159;
                result[2, 3] = 134;
                result[2, 4] = 409;
                result[2, 5] = 123;
                result[3, 0] = 263;
                result[3, 1] = 415;
                result[3, 2] = 140;
                result[3, 3] = 180;
                result[3, 4] = 418;
                result[3, 5] = 152;
                result[4, 0] = 499;
                result[4, 1] = 524;
                result[4, 2] = 334;
                result[4, 3] = 495;
                result[4, 4] = 629;
                result[4, 5] = 173;
                result[5, 0] = 159;
                result[5, 1] = 245;
                result[5, 2] = 168;
                result[5, 3] = 307;
                result[5, 4] = 268;
                result[5, 5] = 318;
                return result;
            }
        }

        public static TransitionTimeBuilder KlauslerSimple
        {
            get
            {
                var table = KlauslerSimpleTable;
                return GetTransitionTimeBuilderFromBlockTable(table);
            }
        }

        public static TransitionTimeBuilder Exaggerated
        {
            get
            {
                var blockTable = new double[6,6];
                for(var i=0;i<6;i++)
                {
                    for(var j=0;j<6;j++)
                    {
                        blockTable[i, j] = 100;
                    }
                }
                blockTable[2, 3] = 0;
                blockTable[3, 2] = 0;
                return GetTransitionTimeBuilderFromBlockTable(blockTable);
            }
        }

        public static TransitionTimeBuilder QuasiRandom
        {
            get
            {
                var random = new Random(0);
                var keyboard = Keyboard.StandardKeyboard;
                var result = new TransitionTimeBuilder(keyboard);
                for (var fromChar = 0; fromChar < keyboard.PositionCount; fromChar++)
                {
                    for (var toChar = 0; toChar < keyboard.PositionCount; toChar++)
                    {
                        var iMod = fromChar % 5;
                        var jMod = toChar % 5;
                        result.data[fromChar, toChar] = new TransitionInfo();
                        var fromBlock = fromChar / 5;
                        var toBlock = toChar / 5;
                        var addDifference = iMod * 5 + jMod;
                        result.data[fromChar, toChar].Mean = random.NextDouble();
                        result.data[fromChar, toChar].Count = 1;
                    }
                }
                return result;
            }
        }

        static TransitionTimeBuilder GetTransitionTimeBuilderFromBlockTable(double[,] table)
        {
            var keyboard = Keyboard.StandardKeyboard;
            var result = new TransitionTimeBuilder(keyboard);
            for (var fromChar = 0; fromChar < keyboard.PositionCount; fromChar++)
            {
                for (var toChar = 0; toChar < keyboard.PositionCount; toChar++)
                {
                    var iMod = fromChar % 5;
                    var jMod = toChar % 5;
                    result.data[fromChar, toChar] = new TransitionInfo();
                    var fromBlock = fromChar / 5;
                    var toBlock = toChar / 5;
                    var addDifference = iMod * 5 + jMod;
                    result.data[fromChar, toChar].Mean = table[fromBlock, toBlock] + addDifference;
                    result.data[fromChar, toChar].Count = 1;
                }
            }
            return result;
        }

        public TransitionInfo this[KeyboardPosition pos1, KeyboardPosition pos2]
        {
            get { return data[keyboard.PositionToPositionIndex(pos1), keyboard.PositionToPositionIndex(pos2)]; }
        }

        public TransitionInfo this[int pos1, int pos2]
        {
            get { return data[pos1, pos2]; }
        }

        public Keyboard Keyboard
        {
            get { return keyboard; }
        }

        public TransitionTimes Times
        {
            get
            {
                var resultData = new double[keyboard.PositionCount,keyboard.PositionCount];
                var worstTime = 0.0;
                var positionIndices = Enumerable.Range(0, keyboard.PositionCount);
                var indexPairs = from x in positionIndices
                                 from y in positionIndices
                                 select Tuple.Create(x,y);
                foreach (var indexPair in indexPairs)
                    worstTime = Math.Max(worstTime,this[indexPair.Item1, indexPair.Item2].Mean);

                foreach(var indexPair in indexPairs)
                {
                    var time = this[indexPair.Item1, indexPair.Item2];
                    resultData[indexPair.Item1, indexPair.Item2] = time.Count == 0 ? worstTime * 2 : time.Mean;
                }
                return new TransitionTimes(resultData, keyboard);
            }
        }

        public static TransitionTimeBuilder GetEmpty(Keyboard keyboard)
        {
            return new TransitionTimeBuilder(keyboard);
        }

        public Document Print()
        {
            var builder = new DocumentWriter();
            foreach(var from in keyboard.Positions)
            {
                builder.Write("From".Print() - from.Print());
                builder.StartBlock(1);
                foreach(var to in keyboard.Positions)
                {
                    var info = this[from, to];
                    builder.WriteLine("To" - to.Print() + DocumentUtil.Semi - info.Print());
                }
                builder.EndBlock();
            }
            return builder.ToDocument();
        }

        public Document Print(KeyboardLayout layout)
        {
            var builder = new DocumentWriter();
            foreach (var from in keyboard.Positions)
            {
                builder.Write("From".Print() - layout[from].RegularChar.Print());
                builder.StartBlock(1);
                foreach (var to in keyboard.Positions)
                {
                    var info = this[from, to];
                    builder.WriteLine("To" - layout[to].RegularChar.Print() + DocumentUtil.Semi - info.Print());
                }
                builder.EndBlock();
            }
            return builder.ToDocument();
        }
    }
}
