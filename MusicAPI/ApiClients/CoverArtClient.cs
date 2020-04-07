using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public class CoverArtClient : ICoverArtClient
    {
        private readonly IApiHelper _ApiHelper;
        public CoverArtClient(IApiHelper apiHelper)
        {
            _ApiHelper = apiHelper;
        }
        public async Task<string> GetUrl(string id)
        {
            string url = "http://coverartarchive.org/release-group/" + id;

            using (HttpResponseMessage response = await _ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var datastring = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(datastring);
                    string imageUrl = json["images"]               // Navigerar in i images-arrayen
                        .Where(jt => (bool)jt["front"] == true)   // Finner imagen som är framsida
                        .Select(jt => (string)jt["image"])       // Plockar image(url)-värdet från det objektet
                        .FirstOrDefault();
                    return imageUrl;
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "No cover found";
                }
                else
                {
                    throw new Exception(((int)response.StatusCode).ToString());
                }
            }
        }
    }
}
