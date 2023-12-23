namespace Connectify.ViewModels
{
	public class ProfileViewModel
	{
		public string Id { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Picture { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public string Phone { get; set; } = string.Empty;
        public LocationViewModel? Location { get; set; }
        public DateTime RegisterDate { get; set; }
	}
}
