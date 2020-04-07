using System.Threading.Tasks;

namespace MusicAPI.APIs
{
    public interface IWikiDataClient
    {
        Task<string> GetWikiPediaTitle(string Qnumber);
    }
}