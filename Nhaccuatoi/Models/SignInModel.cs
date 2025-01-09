using System.ComponentModel.DataAnnotations;

namespace Nhaccuatoi.Models;

public class SignInModel
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
