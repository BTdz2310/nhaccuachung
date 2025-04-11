using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhaccuatoi.Models;

[Table("Songs")]
public class SongModel
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
  
  [Required]
  public string Source { get; set; }

  [Required]
  public string Singer { get; set; }

  [Required]
  public string Image { get; set; }

  public int PlayCount { get; set; } = 0;

  public string Description { get; set; } = "";

  public int ReleasedYear { get; set; }

  public int? AlbumId { get; set; }
  public AlbumModel? Album { get; set; }

  public int? ArtistId { get; set; }
  public ArtistModel? Artist { get; set; }

  public int? GenreId { get; set; }
  public GenreModel? Genre { get; set; }

  public string DatetimeStr { get; set; } = "";
  public string SessionId { get; set; } = "";

  public ICollection<PlaylistSongModel> PlaylistSongs { get; set; } = new List<PlaylistSongModel>();
  public ICollection<SongLikeModel> LikedByUsers { get; set; } = new List<SongLikeModel>();

}
