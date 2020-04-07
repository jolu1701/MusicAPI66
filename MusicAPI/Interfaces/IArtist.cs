using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicAPI.Models
{
    public interface IArtist
    {
        List<Album> Albums { get; set; }
        string Description { get; set; }
        string MbId { get; set; }
        string Name { get; set; }

        Task<Album> CreateCompleteAlbum(string id, string title, Task<string> GetFrontCoverUrl);
    }
}