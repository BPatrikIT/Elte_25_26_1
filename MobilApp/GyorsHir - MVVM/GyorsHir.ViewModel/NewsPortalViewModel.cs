using System;
using System.Collections.Generic;
using System.Text;

namespace GyorsHir.ViewModel
{
    public class NewsPortalViewModel : BindingSource
    {

        #region Properties

        public string ID { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get => string.IsNullOrWhiteSpace(Description) || Description.Length < 100 ? Description : (Description.Substring(0, 100) + "..."); }

        public DelegateCommand SelectCommand { get; private set; }

        #endregion

        #region Constructors

        public NewsPortalViewModel(Model.DTO.FeedlySearchResult feedlySearchResult, DelegateCommand selectCommand)
        {
            ID = feedlySearchResult.FeedId;

            Title = feedlySearchResult.Title;
            Description = feedlySearchResult.Description;

            SelectCommand = selectCommand;
        }

        #endregion

    }
}
