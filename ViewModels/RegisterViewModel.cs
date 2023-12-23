using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Connectify.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Firstname")]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Lastname")]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [DisplayName("Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public TimeZoneInfo? Timezone { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
