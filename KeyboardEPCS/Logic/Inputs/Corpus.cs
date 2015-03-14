using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Generic.Containers.Collections.Dictionaries;
using Generic.InputOutput.Printing;
using Generic.InputOutput.Printing.Sizable;

namespace KeyboardEPCS.Logic.Inputs
{
    [Serializable]
    public class Corpus
    {
        readonly double[,] occurrenceFractions;
        readonly Keyboard keyboard;
        int length;

        void SetOccurrenceFractions(IDictionary<Tuple<char, char>, int> occurrences)
        {
            length = 0;
            foreach (var keyCount in occurrences.Values)
                length += keyCount;

            foreach (var pair in occurrences)
                this[pair.Key.Item1, pair.Key.Item2] = pair.Value / (double)length;
        }

        public Corpus(double[,] occurrenceFractions, int length, Keyboard keyboard)
        {
            this.occurrenceFractions = occurrenceFractions;
            this.length = length;
            this.keyboard = keyboard;
        }

        Corpus(Keyboard keyboard)
        {
            this.keyboard = keyboard;
            occurrenceFractions = new Double[keyboard.AllChars.Count,keyboard.AllChars.Count];
        }

        public Double this[char first, char second]
        {
            get { return occurrenceFractions[keyboard.GetCharIndex(first), keyboard.GetCharIndex(second)]; }
            set { occurrenceFractions[keyboard.GetCharIndex(first), keyboard.GetCharIndex(second)] = value; }
        }

        public static Corpus GetEmpty(Keyboard keyboard)
        {
            return new Corpus(keyboard);
        }

        public Document Print()
        {
            var writer = new DocumentWriter();
            writer.WriteLine("Corpus size is: " + DocumentUtil.Print(length) + " characters.");
            foreach (var key in keyboard.AllChars)
            {
                writer.WriteLine(DocumentUtil.Empty + "From" - key - "to:");
                writer.StartBlock(1);
                foreach (var toKey in keyboard.AllChars)
                {
                    writer.WriteLine(DocumentUtil.Empty + toKey - "=" - (this[key, toKey] * 100).ToString("F10") + "%");
                }
                writer.EndBlock();
            }
            return writer.ToDocument();
        }

        public Corpus AddFileText(String filename)
        {
            var fileContent = new StreamReader(filename).ReadToEnd();
            return AddText(fileContent);
        }

        public Corpus AddText(string fileContent)
        {
            var occurrencesCount = GetFileOccurrences(fileContent);
            AddPresentOccurrences(occurrencesCount);
            SetOccurrenceFractions(occurrencesCount);
            return this;
        }

        void AddPresentOccurrences(Dictionary<Tuple<char, char>, int> occurrencesCount)
        {
            foreach (var fromChar in keyboard.AllChars)
            {
                foreach (var toChar in keyboard.AllChars)
                {
                    var key = Tuple.Create(fromChar, toChar);
                    occurrencesCount.InsertOrUpdate(key, i => i + (int)(this[fromChar, toChar] * length));
                }
            }
        }

        Dictionary<Tuple<char, char>, int> GetFileOccurrences(string fileContent)
        {
            var occurrencesCount = new Dictionary<Tuple<char, char>, int>();
            foreach (var chrIndex in Enumerable.Range(0, fileContent.Length - 1))
            {
                var firstChar = fileContent[chrIndex];
                var secondChar = fileContent[chrIndex + 1];
                if (!keyboard.AllChars.Contains(firstChar) || !keyboard.AllChars.Contains(secondChar))
                    continue;

                var key = Tuple.Create(firstChar, secondChar);
                var previous = occurrencesCount.ContainsKey(key) ? occurrencesCount[key] : 0;
                occurrencesCount[key] = previous + 1;
            }
            return occurrencesCount;
        }
    }
}
