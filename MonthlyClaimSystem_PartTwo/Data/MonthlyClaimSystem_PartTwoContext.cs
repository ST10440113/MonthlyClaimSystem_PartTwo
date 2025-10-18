using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonthlyClaimSystem_PartTwo.Models;

namespace MonthlyClaimSystem_PartTwo.Data
{
    public class MonthlyClaimSystem_PartTwoContext : DbContext
    {
        public MonthlyClaimSystem_PartTwoContext (DbContextOptions<MonthlyClaimSystem_PartTwoContext> options)
            : base(options)
        {
        }

        public DbSet<MonthlyClaimSystem_PartTwo.Models.Lecturer> Lecturer { get; set; } = default!;
        public DbSet<MonthlyClaimSystem_PartTwo.Models.Coordinator> Coordinator { get; set; } = default!;
        public DbSet<MonthlyClaimSystem_PartTwo.Models.Manager> Manager { get; set; } = default!;
    }
}
