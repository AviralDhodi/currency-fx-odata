using Microsoft.EntityFrameworkCore;
using CurrencyFxOData.Models;

namespace CurrencyFxOData.Data;

public class FxDbContext : DbContext
{
    public FxDbContext(DbContextOptions<FxDbContext> options) : base(options) {}

    public DbSet<CurrencyFXRate> CurrencyFXRates => Set<CurrencyFXRate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyFXRate>()
            .HasKey(x => x.UniqueNameDate);

        modelBuilder.Entity<CurrencyFXRate>()
            .HasIndex(x => x.RateDate);
    }
}