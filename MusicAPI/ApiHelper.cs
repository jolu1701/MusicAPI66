using System.Net.Http;
using System.Net.Http.Headers;

namespace MusicAPI
{
    public class ApiHelper : IApiHelper
    {
        public HttpClient ApiClient { get; private set; }

        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
