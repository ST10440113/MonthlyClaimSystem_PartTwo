using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonthlyClaimSystem_PartTwo.Models
{
    public class Manager
    {
        [Key]public int ManagerId { get; set; }


        [ForeignKey("ClaimId")]
        public int ClaimId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
