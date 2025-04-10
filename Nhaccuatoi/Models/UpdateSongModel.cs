using System.ComponentModel.DataAnnotations;

namespace Nhaccuatoi.Models;

public class UpdateSongModel
{

    [Required(ErrorMessage = "Vui lòng nhập tên bài hát")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
    public string Singer { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string Description { get; set; } = "";

    [Required]
    public int ReleasedYear { get; set; }
}
