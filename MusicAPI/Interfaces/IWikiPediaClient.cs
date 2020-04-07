using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public interface IWikiPediaClient
    {
        Task<string> GetArtistInfo(string searchTitle);
    }
}