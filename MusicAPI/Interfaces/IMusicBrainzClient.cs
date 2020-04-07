using System.Threading.Tasks;
using MusicAPI.Models;

namespace MusicAPI
{
    public interface IMusicBrainzClient
    {
        Task<MusicBrainzModel> GetModel(string id);
    }
}