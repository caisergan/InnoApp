using System.ComponentModel;

namespace InnoMvc.Models
{
    public class AppUserViewModel
    {
        [DisplayName("Username")]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
