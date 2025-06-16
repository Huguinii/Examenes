using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RestAPI.Models.DTOs.UsuarioDTO;

public class CreateUsuarioDTO
{
    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string UserName { get; set; }


    [MaxLength(50, ErrorMessage = "Max char is 50")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string Password { get; set; }

    [Required(ErrorMessage = "Rol is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string Rol { get; set; }


}
