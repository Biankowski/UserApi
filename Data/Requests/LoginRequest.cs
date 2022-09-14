using System.ComponentModel.DataAnnotations;

namespace UsersApi.Data.Requests
{
    public class LoginRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
