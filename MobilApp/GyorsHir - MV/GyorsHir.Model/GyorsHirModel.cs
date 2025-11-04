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

        #region Properties

        public IEnumerable<DTO.FeedlySearchResult>? SearchResults { get; private set; }
        public RSS.RSSChannel? NewsChannel { get; private set; }

        #endregion

        #region Events

        public event EventHandler? SearchResultsLoaded;
        public event EventHandler? NewsChannelLoaded;

        #endregion

        #region Public Methods

        public async Task SearchNewsAsync(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Uri uri = new Uri("https://cloud.feedly.com/v3/search/feeds?query=" + name);

                using (HttpClient client = new HttpClient())
                {
                    try
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
                    catch { }
                }
            }
        }

        public async Task GetNewsAsync(string feed)
        {
            string uriString = feed.StartsWith("feed/") ? feed.Substring(5) : feed;
            Uri uri = new Uri(uriString);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        RSS.RSSChannel channel = RSS.RSSParser.ParseString(await response.Content.ReadAsStringAsync());

                        if (channel != null && channel.Items != null)
                        {
                            NewsChannel = channel;
                            NewsChannelLoaded?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                catch { }
            }
        }

        #endregion

    }
}
