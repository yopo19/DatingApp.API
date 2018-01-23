using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Debes de ingresar una contraseña")]
        public string Password { get; set; }
    }
}