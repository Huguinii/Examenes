using System.Text.Json.Serialization;

namespace LoginRegister.Models
{
    public class UserRegistroDTO
    {
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
