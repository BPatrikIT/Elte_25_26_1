using GyorsHir.Model.DTO;
using GyorsHir.Model;
using Newtonsoft.Json;

namespace GyorsHir
{
    public partial class MainPage : ContentPage
    {

        #region Fields

        private GyorsHirModel _model;

        #endregion

        #region Constructors

        public MainPage(GyorsHirModel model)
        {
            InitializeComponent();

            _model = model;
        }

        #endregion

        #region Control Event Handlers

        private async void _searchEntry_Completed(object sender, EventArgs e)
            => await _model.SearchNewsAsync(_searchEntry.Text);

        private async void NewsResultButton_Clicked(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            FeedlySearchResult searchResult = button.BindingContext as FeedlySearchResult;
            await _model.GetNewsAsync(searchResult.FeedId);
        }

        #endregion

        #region Model Event Handlers

        private void _model_SearchResultsLoaded(object sender, EventArgs e)
        {
            _searchResultsList.Children.Clear();

            if (_model.SearchResults != null)
                foreach (FeedlySearchResult result in _model.SearchResults)
                {
                    Button newsResultButton = new Button()
                    {
                        Text = result.Title,
                        BindingContext = result,
                    };
                    newsResultButton.Clicked += NewsResultButton_Clicked;

                    _searchResultsList.Children.Add(newsResultButton);
                }
        }

        #endregion

        #region Protected Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _model.SearchResultsLoaded += _model_SearchResultsLoaded;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _model.SearchResultsLoaded -= _model_SearchResultsLoaded;
        }

        #endregion

    }
}