using System.Text;
using System.Security.Cryptography;

namespace InnoApi.Service
{
    public class PasswordHashing
    {
        public string HashPassword(string Password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                var passwordBytes = Encoding.UTF8.GetBytes(Password);

                // Compute the hash
                var hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a Base64-encoded string
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
