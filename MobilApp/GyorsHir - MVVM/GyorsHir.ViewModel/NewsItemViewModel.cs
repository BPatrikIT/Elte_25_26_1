using System;
using System.Collections.Generic;
using System.Text;

namespace GyorsHir.ViewModel
{
    public class NewsItemViewModel : BindingSource
    {

        #region Properties

        public string Title { get; private set; }
        public string ShortTitle { get => string.IsNullOrWhiteSpace(Title) || Title.Length < 50 ? Title : (Title.Substring(0, 50) + "..."); }
        public Uri Link { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get => string.IsNullOrWhiteSpace(Description) || Description.Length < 100 ? Description : (Description.Substring(0, 100) + "..."); }

        public string Author { get; private set; }
        public DateTimeOffset PublishDate { get; private set; }

        public DelegateCommand SelectCommand { get; private set; }

        #endregion

        #region Constructors

        public NewsItemViewModel(Model.RSS.RSSItem rssItem, DelegateCommand selectCommand)
        {
            Title = rssItem.Title;
            Link = rssItem.Link;
            Description = rssItem.Description;

            Author = rssItem.Author;
            PublishDate = rssItem.PublishDate;

            SelectCommand = selectCommand;
        }

        #endregion

    }
}
