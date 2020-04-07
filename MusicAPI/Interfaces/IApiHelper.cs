using System.Net.Http;

namespace MusicAPI
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}