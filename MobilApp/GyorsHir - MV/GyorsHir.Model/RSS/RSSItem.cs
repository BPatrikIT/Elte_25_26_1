using System;
using System.Collections.Generic;
using System.Text;

namespace GyorsHir.Model.RSS
{
    public class RSSItem
    {

        public string Title { get; set; }
        public Uri Link { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
        public string Category { get; set; }
        public string GUID { get; set; }
        public DateTimeOffset PublishDate { get; set; }

    }
}