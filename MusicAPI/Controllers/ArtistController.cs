using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.APIs;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        IArtist _Artist;
        IMusicBrainzModel _MusicBrainzModel;
        ICoverArtClient _CoverArt;
        IMusicBrainzClient _MusicBrainz;
        IWikiPediaClient _WikiPedia;
        IWikiDataClient _WikiData;

        public ArtistController(IArtist artist, IMusicBrainzModel musicBrainzModel,  
            ICoverArtClient coverArtClient, IMusicBrainzClient musicBrainzClient, IWikiDataClient wikiDataClient, IWikiPediaClient wikiPediaClient)
        {
            _Artist = artist;
            _MusicBrainzModel = musicBrainzModel;
            _CoverArt = coverArtClient;
            _MusicBrainz = musicBrainzClient;
            _WikiData = wikiDataClient;
            _WikiPedia = wikiPediaClient;
        }

        [HttpGet("{id}")]
        // GET api/artist/5
        public async Task<IActionResult> Get(string id)
        {
            if (Guid.TryParse(id, out Guid guidOutput))
            {                
                List<Task<Album>> AsyncCoverArtCalls = new List<Task<Album>>(); // En lista med asyncrona anrop som hämtar skivomslag
                Task<string> callWikiDataForWikiPediaTitle; // Deklarerar ett asyncront anrop som ger Wikipedia-title.

                try
                {
                    // Ropar på MusicBrainz och inväntar svaret.
                    _MusicBrainzModel = await _MusicBrainz.GetModel(id);
                    _Artist.MbId = id;
                    _Artist.Name = _MusicBrainzModel.Name;
                }
                catch (Exception exception)
                {
                    return StatusCode(int.Parse(exception.Message), "The following error occurred at MusicBrainz: " 
                                                                    + int.Parse(exception.Message));
                }

                foreach (var album in _MusicBrainzModel.Albums)
                {
                    //Påbörjar skapandet av album samt de asyncrona anropen efter skivomslag
                    Task<Album> getCompleteAlbum = _Artist.CreateCompleteAlbum(album.Id, album.Title, _CoverArt.GetUrl(album.Id));
                    AsyncCoverArtCalls.Add(getCompleteAlbum);
                }

                try
                {
                    //Ropar på WikiData
                    callWikiDataForWikiPediaTitle = _WikiData.GetWikiPediaTitle(_MusicBrainzModel.GetWikiDataQNumber());
                }
                catch (Exception exception)
                {
                    return StatusCode(int.Parse(exception.Message), "The following error occurred at Wikidata: " 
                                                                    + int.Parse(exception.Message));
                }

                try
                {
                    foreach (var album in AsyncCoverArtCalls)
                    {
                        //Inväntar att omslaget kommit, lägger sedan in albumobjektet i artistens lista över album.
                        _Artist.Albums.Add(await album);
                    }
                }
                catch (Exception exception)
                {
                    return StatusCode(int.Parse(exception.Message), "The following error occurred at CoverArtArchive: " 
                                                                    + int.Parse(exception.Message));
                }

                try
                {
                    /*Tar fram WikiPediaTitle antingen genom befintlig data eller
                    från WikiDatas API och med WikiPediaTitle hämtas artistens 
                    description från WikiPedias API.*/
                    string wikiPediaTitle;
                    if (_MusicBrainzModel.GetWikiPediaTitle() != null)
                        wikiPediaTitle = _MusicBrainzModel.GetWikiPediaTitle();
                    else
                        wikiPediaTitle = await callWikiDataForWikiPediaTitle;     
                    
                    _Artist.Description = await _WikiPedia.GetArtistInfo(wikiPediaTitle);
                }
                catch (Exception exception)
                {
                    return StatusCode(int.Parse(exception.Message), "The following error occurred at Wikipedia: " 
                                                                    + int.Parse(exception.Message));
                }

            return Ok(_Artist);
            }
            else
            {
                return BadRequest("You need to enter a Guid from https://musicbrainz.org/.");                
            }
        }
    }
}
