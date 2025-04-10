using Nhaccuatoi.Models;

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<SongModel> Songs { get; set; } = new List<SongModel>();
}