using System.ComponentModel.DataAnnotations;

namespace Nhaccuatoi.Models;

public class UploadSongModel
{
    [Required(ErrorMessage = "Vui lòng chọn file mp3")]
    [DataType(DataType.Upload)]
    public IFormFile AudioFile { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên bài hát")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
    public string Singer { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn file ảnh")]
    [DataType(DataType.Upload)]
    public IFormFile ImageFile { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string Description { get; set; } = "";

    [Required]
    public int ReleasedYear { get; set; }
}
