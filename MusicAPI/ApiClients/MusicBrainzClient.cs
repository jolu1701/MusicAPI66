using MusicAPI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicAPI
{
    public class MusicBrainzClient : IMusicBrainzClient
    {
        private readonly IApiHelper _ApiHelper;

        public MusicBrainzClient(IApiHelper apiHelper)
        {
            _ApiHelper = apiHelper;
        }
        public async Task<MusicBrainzModel> GetModel(string id)
        {
            string url = "http://musicbrainz.org/ws/2/artist/" + id + "?&fmt=json&inc=url-rels+release-groups";

            using (HttpResponseMessage response = await _ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    MusicBrainzModel musicBrainz = await response.Content.ReadAsAsync<MusicBrainzModel>();
                    return musicBrainz;
                }
                else
                {
                    throw new Exception(((int)response.StatusCode).ToString());
                }
            }
        }
    }
}
