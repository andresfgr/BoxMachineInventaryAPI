using Microsoft.EntityFrameworkCore;

namespace BoxMachineInventary.Models
{
    public class BoxMachineInventaryContext : DbContext
    {
        public BoxMachineInventaryContext(DbContextOptions<BoxMachineInventaryContext> options) : base(options)
        {

        }

        public DbSet<CardbordPalletInventary> CardbordPalletInventary { get; set; }
        public DbSet<BoxMachineProductionSheet> BoxMachineProductionSheet { get; set; }
        public DbSet<BoxSize> BoxSize { get; set; }
    }
}
