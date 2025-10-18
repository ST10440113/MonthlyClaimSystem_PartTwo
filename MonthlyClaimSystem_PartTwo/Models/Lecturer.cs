using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MonthlyClaimSystem_PartTwo.Models
{
    public class Lecturer
    {
        [Key]public int LecturerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Faculty { get; set; }

        public string ClaimName { get; set; }

          public int Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public int HoursWorked { get; set; }

        [Column(TypeName = "decimal(18, 2)")] public decimal HourlyRate { get; set; }

        public string Description { get; set; }
        public string Email { get; set; }

        public DateTime SubmittedDate { get; set; }

        public int SubmittedBy { get; set; }

        public ClaimStatus Status { get; set; }

        public string ReviewedBy { get; set; }
        public DateTime ReviewedDate { get; set; }

        public string ContactNum { get; set; }
        public List<FileModel> UploadedFiles { get; set; }

        public List<ClaimReview> Reviews { get; set; } = new List<ClaimReview>();


    }
}
