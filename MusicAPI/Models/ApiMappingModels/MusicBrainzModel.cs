using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Models
{
    public class MusicBrainzModel : IMusicBrainzModel
    {
        public string Name { get; set; }
        [JsonProperty("release-groups")]
        public IEnumerable<MusicBrainzAlbum> Albums { get; set; }
        public IEnumerable<MusicBrainzWikiDataInfo> Relations { get; set; }

        public class MusicBrainzWikiDataInfo
        {
            public string Type { get; set; }
            [JsonProperty("type-id")]
            public string TypeId { get; set; }
            public Dictionary<string, string> Url { get; set; }
            public string Title { get; set; }
            
        }

        public class MusicBrainzAlbum
        {
            public string Id { get; set; }            
            public string Title { get; set; }
        }

        public string GetWikiDataQNumber()
        {
            /*Här letas strängen fram bland modellens relationer och 
             trimmas, eftersom MusicBrainz ger hela URLen och inte 
             bara Q-numret vi är ute efter.*/
            var wikidataRelation = Relations.Where(i => i.Type == "wikidata").FirstOrDefault();
            string wikidataURL = wikidataRelation.Url["resource"];
            int indexOfQ = wikidataURL.IndexOf('Q');
            return wikidataURL.Substring(indexOfQ, wikidataURL.Length - indexOfQ);
        }

        public string GetWikiPediaTitle()
        {
            /*Metoden letar efter en wikipediatitle i modellens relationer vilket
             enligt specialfallet i uppgiftsbeskrivningen förekommer i enstaka fall*/
            var wikiPedia = Relations.Where(i => i.Type == "wikipedia").FirstOrDefault();
            if (wikiPedia != null)
                return wikiPedia.Title;
            else
                return null;
        }
    }
}
