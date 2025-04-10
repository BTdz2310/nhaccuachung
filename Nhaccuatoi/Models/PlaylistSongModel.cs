using Nhaccuatoi.Models;

public class PlaylistSongModel
{
    public int Id { get; set; }

    public int PlaylistId { get; set; }
    public PlaylistModel Playlist { get; set; } = null!;

    public int SongId { get; set; }
    public SongModel Song { get; set; } = null!;

    public int Position { get; set; }
}
