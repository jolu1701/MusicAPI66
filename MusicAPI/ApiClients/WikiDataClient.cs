using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public class WikiDataClient : IWikiDataClient
    {
        private readonly IApiHelper _ApiHelper;
        public WikiDataClient(IApiHelper apiHelper)
        {
            _ApiHelper = apiHelper;
        }
        public async Task<string> GetWikiPediaTitle(string Qnumber)
        {
            string url = $"https://www.wikidata.org/w/api.php?action=wbgetentities&ids={Qnumber}&format=json&props=sitelinks";

            using (HttpResponseMessage response = await _ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var datastring = await response.Content.ReadAsStringAsync();
                    JObject json2 = JObject.Parse(datastring);
                    var wikiTitle = (JValue)json2["entities"][Qnumber]["sitelinks"]["enwiki"]["title"];
                    return Uri.EscapeDataString(wikiTitle.ToString());
                }
                else
                {
                    throw new Exception(((int)response.StatusCode).ToString());
                }
            }
        }
    }
}
