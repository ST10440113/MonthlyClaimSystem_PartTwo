using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MonthlyClaimSystem_PartTwo.Models
{
    public class Lecturer
    {
        [Key]public int ClaimId { get; set; }

        public string LecturerRefID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Faculty { get; set; }

        public string ClaimName { get; set; }

        [DataType(DataType.Currency)] public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int HoursWorked { get; set; }

        [DataType(DataType.Currency)] public decimal HourlyRate { get; set; }

        public string Description { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }

        [DataType(DataType.Date)] public DateTime? SubmittedDate { get; set; }

        public int? SubmittedBy { get; set; }

        public ClaimStatus Status { get; set; }

        public string? ReviewedBy { get; set; }
        [DataType(DataType.Date)] public DateTime? ReviewedDate { get; set; }

        [DataType(DataType.PhoneNumber)] public string ContactNum { get; set; }
        public List<FileModel> UploadedFiles { get; set; } = new List<FileModel>();

        public List<ClaimReview> Reviews { get; set; } = new List<ClaimReview>();


    }
}
