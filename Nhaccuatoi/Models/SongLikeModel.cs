using Nhaccuatoi.Models;

public class SongLikeModel
{
    public int Id { get; set; }
    public String UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public int SongId { get; set; }
    public SongModel Song { get; set; } = null!;

    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
}