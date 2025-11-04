using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Model
{
    internal class DocumentStatistics : IDocumentStatistics
    {
        public event EventHandler? FileContentReady;
        public event EventHandler? TextStatisticsReady;
        private string _filePath;

        public DocumentStatistics(string filePath)
        {
            _filePath = filePath;
        }

        public string FileContent { get; private set; }

        public string Load()
        {
            FileContent = System.IO.File.ReadAllText(_filePath);
            OnFileContentReady();
            return FileContent;
        }

        private void OnFileContentReady()
        {
            FileContentReady?.Invoke(this, EventArgs.Empty);
        }

        private void OnTextStatisticsReady()
        {
            TextStatisticsReady?.Invoke(this, EventArgs.Empty);
        }

    }
 }
