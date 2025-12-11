using System;
using System.Collections.Generic;
using System.Text;

namespace GyorsHir.ViewModel
{
    public class NewsPortalViewModel : BindingSource
    {

        #region Fields

        private bool _isFavourite;

        #endregion

        #region Properties

        public string ID { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get => string.IsNullOrWhiteSpace(Description) || Description.Length < 100 ? Description : (Description.Substring(0, 100) + "..."); }

        public bool IsFavourite
        {
            get => _isFavourite;
            set
            {
                if (value != _isFavourite)
                {
                    _isFavourite = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand SelectCommand { get; private set; }
        public DelegateCommand FavouriteCommand { get; private set; }

        #endregion

        #region Constructors

        public NewsPortalViewModel(Model.DTO.FeedlySearchResult feedlySearchResult, bool isFavourite, DelegateCommand selectCommand, DelegateCommand favouriteCommand)
        {
            ID = feedlySearchResult.FeedId;

            Title = feedlySearchResult.Title;
            Description = feedlySearchResult.Description;

            IsFavourite = isFavourite;

            SelectCommand = selectCommand;
            FavouriteCommand = favouriteCommand;
        }
        public NewsPortalViewModel(Persistence.FavouriteNewsPortal favourite, DelegateCommand selectCommand, DelegateCommand favouriteCommand)
        {
            ID = favourite.FeedId;

            Title = favourite.Title;
            Description = null;

            IsFavourite = true;

            SelectCommand = selectCommand;
            FavouriteCommand = favouriteCommand;
        }

        #endregion

    }
}
