using System;
using System.Linq;
using System.Collections.ObjectModel;


namespace GyorsHir.ViewModel
{
    public class GyorsHirViewModel : BindingSource
    {

        #region Fields

        private Model.GyorsHirModel _model;

        private NewsPortalViewModel _selectedNewsPortal;
        private NewsItemViewModel _selectedNewsItem;

        private IShare? iShare;

        #endregion

        #region Properties

        public string SearchFilter { get; set; }
        public ObservableCollection<NewsPortalViewModel> NewsPortals { get; private set; } = new ObservableCollection<NewsPortalViewModel>();

        public NewsPortalViewModel SelectedNewsPortal
        {
            get => _selectedNewsPortal;
            private set
            {
                if (value != _selectedNewsPortal)
                {
                    _selectedNewsPortal = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<NewsItemViewModel> NewsList { get; private set; } = new ObservableCollection<NewsItemViewModel>();

        public NewsItemViewModel SelectedNewsItem
        {
            get => _selectedNewsItem;
            private set
            {
                if (value != _selectedNewsItem)
                {
                    _selectedNewsItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<NewsPortalViewModel> Favourites { get; private set; } = new ObservableCollection<NewsPortalViewModel>();

        public DelegateCommand SearchCommand { get; private set; }

        #endregion

        #region Events

        public event EventHandler? NewsLoaded;
        public event EventHandler? NewsItemSelected;

        #endregion

        #region Constructors

        public GyorsHirViewModel(Model.GyorsHirModel model, IShare? iShare = null)
        {
            _model = model ?? throw new ArgumentNullException("model");
            
            this.iShare = iShare;

            _model.SearchResultsLoaded += _model_SearchResultsLoaded;
            _model.NewsChannelLoaded += _model_NewsChannelLoaded;
            _model.FavouritesChanged += _model_FavouritesChanged;

            SearchCommand = new DelegateCommand(Command_Search);
        }

        #endregion

        #region Model Event Handlers

        private void _model_SearchResultsLoaded(object? sender, EventArgs e)
        {
            if (_model.SearchResults != null)
            {
                foreach (Model.DTO.FeedlySearchResult searchResult in _model.SearchResults)
                    NewsPortals.Add(new NewsPortalViewModel(
                        searchResult,
                        _model.Favourites.Any(f => f.FeedId == searchResult.FeedId),
                        new DelegateCommand(Command_SelectNewsPortal),
                        new DelegateCommand(Command_FavouriteNewsPortal)));
            }
        }
        private void _model_NewsChannelLoaded(object? sender, EventArgs e)
        {
            if (_model.NewsChannel != null)
            {
                foreach (Model.RSS.RSSItem rssItem in _model.NewsChannel.Items)
                    NewsList.Add(new NewsItemViewModel(rssItem, new DelegateCommand(Command_SelectNewsItem), new DelegateCommand(Command_ShareNewsItem, Command_ShareNewsItem_CanExecute)));
                OnNewsLoaded();
            }
        }
        private void _model_FavouritesChanged(object sender, EventArgs e)
        {
            Favourites.Clear();
            foreach (Persistence.FavouriteNewsPortal favourite in _model.Favourites)
                Favourites.Add(new NewsPortalViewModel(favourite, new DelegateCommand(Command_SelectNewsPortal), new DelegateCommand(Command_FavouriteNewsPortal)));

            foreach (NewsPortalViewModel newsPortal in NewsPortals)
                newsPortal.IsFavourite = _model.Favourites.Any(f => f.FeedId == newsPortal.ID);
            if (_selectedNewsPortal != null)
                _selectedNewsPortal.IsFavourite = _model.Favourites.Any(f => f.FeedId == _selectedNewsPortal.ID);
        }

        #endregion

        #region Command Methods

        private async void Command_Search(object? param)
        {
            NewsPortals.Clear();
            await _model.SearchNewsAsync(SearchFilter);
        }

        private async void Command_SelectNewsPortal(object? param)
        {
            if (param != null && param is NewsPortalViewModel newsPortal)
            {
                NewsList.Clear();
                SelectedNewsPortal = newsPortal;
                await _model.LoadNewsAsync(newsPortal.ID);
            }
        }
        private async void Command_FavouriteNewsPortal(object param)
        {
            if (param != null && param is NewsPortalViewModel newsPortal)
            {
                if (newsPortal.IsFavourite)
                    await _model.RemoveFavouriteAsync(newsPortal.ID);
                else await _model.AddFavouriteAsync(newsPortal.ID, newsPortal.Title);
            }
        }

        private void Command_SelectNewsItem(object? param)
        {
            if (param != null && param is NewsItemViewModel newsItem)
            {
                SelectedNewsItem = newsItem;
                OnNewsItemSelected();
            }
        }

        private void Command_ShareNewsItem(object? param)
        {
            if (param is NewsItemViewModel newsItem && newsItem.IsShareable && iShare != null)
            {
                iShare.ShareUri(newsItem.Link.ToString(), newsItem.Title);
            }
        }

        private bool Command_ShareNewsItem_CanExecute(object? param)
            => param is NewsItemViewModel newsItem && newsItem.IsShareable;

        #endregion

        #region Private Methods

        private void OnNewsLoaded() => NewsLoaded?.Invoke(this, EventArgs.Empty);
        private void OnNewsItemSelected() => NewsItemSelected?.Invoke(this, EventArgs.Empty);

        #endregion

    }
}
