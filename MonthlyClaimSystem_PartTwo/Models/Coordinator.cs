using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonthlyClaimSystem_PartTwo.Models
{
    public class Coordinator
    {

        [Key]public int CoordinatorId { get; set; }


        [ForeignKey("ClaimId")]
        public int ClaimId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
