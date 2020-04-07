using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicAPI.Models
{
    public class Artist : IArtist
    {
        public string MbId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();

        public async Task<Album> CreateCompleteAlbum(string id, string title, Task<string> GetFrontCoverUrl)
        {            
            Album completeAlbum = new Album { Id = id, Title = title, Image = await GetFrontCoverUrl };
            return completeAlbum;
        }
    }
}
