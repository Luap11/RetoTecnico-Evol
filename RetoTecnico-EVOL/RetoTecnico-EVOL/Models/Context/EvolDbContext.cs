using Microsoft.EntityFrameworkCore;

namespace RetoTecnico_EVOL.Models.Context
{
    public partial class EvolDbContext:DbContext
    {
        public EvolDbContext(DbContextOptions<EvolDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
