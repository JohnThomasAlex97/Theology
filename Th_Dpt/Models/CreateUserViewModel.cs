using System.ComponentModel.DataAnnotations;

namespace Th_Dpt.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; } = "";

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; } = "";

        public int Age { get; set; }

        public string Gender { get; set; } = "";

        public string Address { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public DateTime DateOfBirth { get; set; }

        public string District { get; set; } = "";

        public string Work { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string TTTID { get; set; } = "";
        public string Password { get; set; } = "";
        public string? PasswordHash { get; set; } = "";

        public List<int> SelectedRoleIds { get; set; } = new();
    }
}
