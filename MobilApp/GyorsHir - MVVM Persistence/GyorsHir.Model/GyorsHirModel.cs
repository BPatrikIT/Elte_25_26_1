using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GyorsHir.Model
{
    public class GyorsHirModel
    {

        #region Fields

        private Persistence.IGyorsHirPersistence _persistence;

        private List<Persistence.FavouriteNewsPortal> _favourites = new List<Persistence.FavouriteNewsPortal>();

        #endregion

        #region Properties

        public IEnumerable<DTO.FeedlySearchResult>? SearchResults { get; private set; }
        public RSS.RSSChannel? NewsChannel { get; private set; }
        public IEnumerable<Persistence.FavouriteNewsPortal> Favourites => _favourites;

        #endregion

        #region Events

        public event EventHandler? SearchResultsLoaded;
        public event EventHandler? NewsChannelLoaded;
        public event EventHandler? FavouritesChanged;

        #endregion

        #region Constructors

        public GyorsHirModel(Persistence.IGyorsHirPersistence persistence)
        {
            _persistence = persistence;
        }

        #endregion

        #region Private Methods

        private async Task<RSS.RSSChannel?> GetNewsAsync(string feed)
        {
            string uriString = feed.StartsWith("feed/") ? feed.Substring(5) : feed;
            Uri uri = new Uri(uriString);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        RSS.RSSChannel channel = RSS.RSSParser.ParseString(await response.Content.ReadAsStringAsync());

                        return channel != null && channel.Items != null ? channel : null;
                    }
                }
            }
            catch { }

            return null;
        }

        #endregion

        #region Public Methods

        public async Task InitializeAsync()
        {
            if (_persistence != null)
            {
                _favourites = (await _persistence.GetFavouritesAsync()).ToList();
                FavouritesChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task SearchNewsAsync(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Uri uri = new Uri($"https://cloud.feedly.com/v3/search/feeds?query={name}");

                using (HttpClient client = new HttpClient())
                {
                    using HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        DTO.FeedlySearchResponse? searchResponse =
                            JsonConvert.DeserializeObject<DTO.FeedlySearchResponse>(await response.Content.ReadAsStringAsync());

                        if (searchResponse != null && searchResponse.Results != null)
                        {
                            SearchResults = searchResponse.Results;
                            SearchResultsLoaded?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        public async Task LoadNewsAsync(string feed)
        {
            RSS.RSSChannel? channel = await GetNewsAsync(feed);

            if (channel != null)
            {
                NewsChannel = channel;
                NewsChannelLoaded?.Invoke(this, EventArgs.Empty);
            }
        }


        public async Task AddFavouriteAsync(string feedID, string title)
        {
            if (_favourites.All(f => f.FeedId != feedID))
            {
                Persistence.FavouriteNewsPortal favourite = new Persistence.FavouriteNewsPortal() { FeedId = feedID, Title = title };
                _favourites.Add(favourite);
                FavouritesChanged?.Invoke(this, EventArgs.Empty);

                if (_persistence != null)
                    await _persistence.AddFavouriteAsync(favourite);
            }
        }
        public async Task RemoveFavouriteAsync(string feedID)
        {
            Persistence.FavouriteNewsPortal? favourite = _favourites.FirstOrDefault(f => f.FeedId == feedID);
            if (favourite != null)
            {
                _favourites.Remove(favourite);
                FavouritesChanged?.Invoke(this, EventArgs.Empty);

                if (_persistence != null)
                    await _persistence.RemoveFavouriteAsync(feedID);
            }
        }

        #endregion

    }
}
