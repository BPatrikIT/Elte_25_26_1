using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader.Model
{
    public class WebImage
    {
        public Uri Url { get; private set; }
        public byte[] Data { get; private set; }

        private WebImage(Uri url, byte[] data)
        {
            Url = url;
            Data = data;
        }

        public static async Task<WebImage> DownloadAsync(Uri url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (!url.IsAbsoluteUri)
            {
                throw new ArgumentException("The URL must be an absolute URI.", nameof(url));
            }

            HttpClient client = new HttpClient();
            byte[] data = await client.GetByteArrayAsync(url);

            return new WebImage(url, data);
        }
    }
}
