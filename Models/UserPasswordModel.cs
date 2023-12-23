using System.ComponentModel.DataAnnotations;

namespace Connectify.Models
{
    public class UserPasswordModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
