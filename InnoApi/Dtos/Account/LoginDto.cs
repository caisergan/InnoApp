using System.ComponentModel.DataAnnotations;

namespace InnoApi.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
    }
}
