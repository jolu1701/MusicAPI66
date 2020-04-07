using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public interface ICoverArtClient
    {
        Task<string> GetUrl(string id);
    }
}