using System.Collections.Generic;

namespace Kata04.TabularFile
{
    public class TabularFileData<T>
    {
        private readonly List<T> _fileData;

        public TabularFileData()
        {
            _fileData = new List<T>();
        }
        public IReadOnlyList<T> FileData => _fileData;
        public string HeaderText { get; set; }
        public string SummaryText { get; set; }

        public void AddData(T fileData)
        {
            _fileData.Add(fileData);
        }
    }
}