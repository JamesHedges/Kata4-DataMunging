using System.Collections.Generic;

namespace Kata04.Football
{
    public class FootballFileData
    {
        private readonly List<FootballData> _footballData;

        public FootballFileData()
        {
            _footballData = new List<FootballData>();
        }
        public IReadOnlyList<FootballData> FootballData => _footballData;
        public string HeaderText { get; set; }
        public string SummaryText { get; set; }

        public void AddFootballData(FootballData footballData)
        {
            _footballData.Add(footballData);
        }
    }
}