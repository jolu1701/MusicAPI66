namespace MusicAPI.Models
{
    public interface IAlbum
    {
        string Id { get; set; }
        string Image { get; set; }
        string Title { get; set; }
    }
}