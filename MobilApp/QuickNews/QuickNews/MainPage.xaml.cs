using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickNews.DTO;

namespace QuickNews
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void _searchEntry_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_searchEntry.Text))
            {

                _searchREsultsList.Clear();

                Uri uri = new Uri($"https://cloud.feedly.com/v3/search/feeds?query={_searchEntry.Text.Trim().ToLower()}");

                using HttpClient client = new HttpClient();

                using HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) 
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    DTO.FeedlySearchResponse? feedlyResponse =
                    JsonConvert.DeserializeObject<DTO.FeedlySearchResponse>(jsonResponse);

                    if (feedlyResponse != null)
                    {
                        foreach (DTO.FeedlySearchResult result in feedlyResponse.Results)
                        {
                            Button resultButton = new Button()
                            {
                                Text = result.Title,
                                BindingContext = result,
                            };
                            resultButton.Clicked += ResultButton_Clicked;
                            _searchREsultsList.Children.Add(resultButton);
                            
                        }
                    }
                }
            }
        }
        private async void ResultButton_Clicked(object? sender, EventArgs e)
        {
            DTO.FeedlySearchResult result = (sender as Button).BindingContext as DTO.FeedlySearchResult;
            SearchResultsPage searcchResultPage = new SearchResultsPage(result);
            await (App.Current as App).NavPage.PushAsync(searcchResultPage);
        }
    }

}
