using System.ComponentModel.DataAnnotations;

namespace Th_Dpt.Models
{
    public class MasterUser
    {
        public int Id { get; set; }  // UserID

        [Required]
        public string? FirstName { get; set; } = "";

        public string? MiddleName { get; set; }

        [Required]
        public string? LastName { get; set; } = "";

        public int Age { get; set; }

        public string? Gender { get; set; } = "";

        public string? Address { get; set; } = "";

        public string? PhoneNumber { get; set; } = "";

        public DateTime DateOfBirth { get; set; }

        public string? District { get; set; } = "";

        public string? Work { get; set; } = "";
        public string? TTTID { get; set; } = "";
        public string? PasswordHash { get; set; } = "";


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? RoleName { get; set; }
        public string? ClassName { get; set; }
        public string? UserName { get; set; }

        // Navigation
        //public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = "";

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
    public class UserRole
    {
        public int MasterUserId { get; set; }
        public MasterUser MasterUser { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
    public class Class
    {
        public int id { get; set; }  
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public string Evangelist { get; set; }
        public string RegCount { get; set; }
        public string Level { get; set; }
    }

    public class Evangelism
    {
        public int id { get; set; }
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public string CenterStudentName { get; set; }
        public string ProspectName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    }

    public class ClassReport
    {
        public int Id { get; set; }
        public string? ClassName { get; set; }
        public string? Instructor { get; set; }
        public int? AttendanceCount { get; set; }
        public int? NoBB { get; set; }
        public string? SpecialNotes { get; set; }
        public DateTime Created_At { get; set; }
    }

    public class CenterReportVM
    {
        public string Level { get; set; }
        public string ClassName { get; set; }
        public int RegCount { get; set; }

        public int ParticipationCount { get; set; }
        public int EvangelismCount { get; set; }
        public int OfferingCount { get; set; }

        public double ParticipationRate { get; set; }
        public double EvangelismRate { get; set; }
        public double OfferingRate { get; set; }
    }


}
