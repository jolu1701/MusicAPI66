using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public class WikiPediaClient : IWikiPediaClient
    {
        private readonly IApiHelper _ApiHelper;
        public WikiPediaClient(IApiHelper apiHelper)
        {
            _ApiHelper = apiHelper;
        }
        public async Task<string> GetArtistInfo(string searchTitle)
        {
            string url = "https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=" + searchTitle;

            using (HttpResponseMessage response = await _ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var datastring = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(datastring);
                    var page = (JObject)json["query"]["pages"];
                    var pageId = page.Properties().First().Name;
                    var artistInfo = (JValue)page[pageId]["extract"];

                    return artistInfo.ToString();
                }
                else
                {
                    throw new Exception(((int)response.StatusCode).ToString());
                }
            }
        }
    }
}
