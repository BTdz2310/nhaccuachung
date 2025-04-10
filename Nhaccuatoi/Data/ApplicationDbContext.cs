using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nhaccuatoi.Models;

namespace Nhaccuatoi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SongModel> Songs { get; set; }
    public DbSet<ArtistModel> Artists { get; set; }
    public DbSet<AlbumModel> Albums { get; set; }
    public DbSet<GenreModel> Genres { get; set; }
    public DbSet<PlaylistModel> Playlists { get; set; }
    public DbSet<PlaylistSongModel> PlaylistSongs { get; set; }
    public DbSet<SongLikeModel> SongLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PlaylistSong (Composite Key)
        modelBuilder.Entity<PlaylistSongModel>()
            .HasKey(ps => new { ps.PlaylistId, ps.SongId });

        modelBuilder.Entity<PlaylistSongModel>()
            .HasOne(ps => ps.Playlist)
            .WithMany(p => p.Songs)
            .HasForeignKey(ps => ps.PlaylistId);

        modelBuilder.Entity<PlaylistSongModel>()
            .HasOne(ps => ps.Song)
            .WithMany(s => s.PlaylistSongs)
            .HasForeignKey(ps => ps.SongId);

        // SongLike (Composite Key)
        modelBuilder.Entity<SongLikeModel>()
            .HasKey(sl => new { sl.UserId, sl.SongId });

        modelBuilder.Entity<SongLikeModel>()
            .HasOne(sl => sl.User)
            .WithMany(u => u.LikedSongs)
            .HasForeignKey(sl => sl.UserId);

        modelBuilder.Entity<SongLikeModel>()
            .HasOne(sl => sl.Song)
            .WithMany(s => s.LikedByUsers)
            .HasForeignKey(sl => sl.SongId);

        // Song - Album (optional)
        modelBuilder.Entity<SongModel>()
            .HasOne(s => s.Album)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.AlbumId)
            .OnDelete(DeleteBehavior.SetNull);

        // Song - Artist (optional)
        modelBuilder.Entity<SongModel>()
            .HasOne(s => s.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.SetNull);

        // Song - Genre (optional)
        modelBuilder.Entity<SongModel>()
            .HasOne(s => s.Genre)
            .WithMany(g => g.Songs)
            .HasForeignKey(s => s.GenreId)
            .OnDelete(DeleteBehavior.SetNull);

        // Album - Artist (optional)
        modelBuilder.Entity<AlbumModel>()
            .HasOne(a => a.Artist)
            .WithMany(ar => ar.Albums)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.SetNull);

        // Playlist - User
        modelBuilder.Entity<PlaylistModel>()
            .HasOne(p => p.User)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UserId);
    }
}
