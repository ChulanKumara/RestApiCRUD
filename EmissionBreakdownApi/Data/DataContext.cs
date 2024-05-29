using Atmoz.EmissionBreakdownApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmissionBreakdownApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmissionBreakdownRow> EmissionBreakdownRow { get; set; }
        public DbSet<EmissionCategory> EmissionCategory { get; set; }
        public DbSet<EmissionSubCategory> EmissionSubCategory { get; set; }
    }
}
