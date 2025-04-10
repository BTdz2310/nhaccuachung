
using Nhaccuatoi.Models;

public class AlbumModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string? CoverUrl { get; set; }

    public int? ArtistId { get; set; }
    public ArtistModel? Artist { get; set; }

    public int TotalSongs => Songs?.Count ?? 0;
    public int TotalPlays => Songs?.Sum(s => s.PlayCount) ?? 0;

    public ICollection<SongModel> Songs { get; set; } = new List<SongModel>();
}