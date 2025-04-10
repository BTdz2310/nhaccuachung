using Microsoft.AspNetCore.Identity;

namespace Nhaccuatoi.Models
{
    public class ApplicationUser : IdentityUser
    {
        // public string Username { get; set;}
        // public string Password { get; set;}

        public ICollection<PlaylistModel> Playlists { get; set; } = new List<PlaylistModel>();
        public ICollection<SongLikeModel> LikedSongs { get; set; } = new List<SongLikeModel>();

    }
}
