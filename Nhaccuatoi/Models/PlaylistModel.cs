using Nhaccuatoi.Models;

public class PlaylistModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public String UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public ICollection<PlaylistSongModel> Songs { get; set; } = new List<PlaylistSongModel>();
}
