using System.ComponentModel.DataAnnotations.Schema;
using Nhaccuatoi.Models;

[Table("Artists")]
public class ArtistModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<AlbumModel> Albums { get; set; } = new List<AlbumModel>();
    public ICollection<SongModel> Songs { get; set; } = new List<SongModel>();
}