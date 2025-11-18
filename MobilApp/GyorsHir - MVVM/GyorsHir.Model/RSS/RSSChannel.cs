using System;
using System.Collections.Generic;
using System.Text;

namespace GyorsHir.Model.RSS
{
    public class RSSChannel
    {

        public string Title { get; set; }
        public Uri Link { get; set; }
        public string Description { get; set; }


        public IEnumerable<RSSItem> Items { get; set; }

    }
}