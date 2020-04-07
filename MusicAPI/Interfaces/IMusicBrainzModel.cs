using System.Collections.Generic;

namespace MusicAPI.Models
{
    public interface IMusicBrainzModel
    {
        IEnumerable<MusicBrainzModel.MusicBrainzAlbum> Albums { get; set; }
        string Name { get; set; }
        IEnumerable<MusicBrainzModel.MusicBrainzWikiDataInfo> Relations { get; set; }

        string GetWikiDataQNumber();
        string GetWikiPediaTitle();
    }
}