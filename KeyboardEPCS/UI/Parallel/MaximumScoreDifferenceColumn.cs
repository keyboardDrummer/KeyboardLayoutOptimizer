using System.Collections.Generic;
using Generic.Containers.Collections.Dictionaries;

namespace KeyboardEPCS.UI
{
    class MaximumScoreDifferenceColumn : IColumnSpec
    {
        readonly SortedSet<BetterFoundInfo> sortedScores = new SortedSet<BetterFoundInfo>(new InfoScoreComparer());
            
        class InfoScoreComparer : IComparer<BetterFoundInfo>
        {
            public int Compare(BetterFoundInfo x, BetterFoundInfo y)
            {
                return x.CurrentScore.CompareTo(y.CurrentScore);
            }
        }

        public void Start()
        {
        }

        public string Caption
        {
            get { return "Maximum score difference"; }
        }

        public string GetColumnString(IDictionary<KeyboardEPAlgorithm, BetterFoundInfo> infos, BetterFoundInfo info)
        {
            infos.Get(info.Algorithm).Exec(oldInfo => sortedScores.Remove(oldInfo));
            infos[info.Algorithm] = info;
            sortedScores.Add(info);
            var maxScoreDifference = sortedScores.Max.CurrentScore - sortedScores.Min.CurrentScore;
            return maxScoreDifference.ToString();
        }
    }
}