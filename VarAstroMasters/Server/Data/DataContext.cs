using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StarCatalog>()
            .HasKey(sc => new { sc.CatalogId, sc.StarId });

        builder.Entity<Star>().HasData(
            new Star
            {
                Id = 1,
                Name = "CzeV 612",
                RA = 123.456m
            }
        );
        builder.Entity<Catalog>().HasData(
            new Catalog
            {
                Name = "UCAC4"
            },
            new Catalog
            {
                Name = "2MASS"
            });
        base.OnModelCreating(builder);
    }

    public DbSet<Star> Stars { get; set; }
    public DbSet<LightCurve> LightCurves { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Observatory> Observatories { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<StarCatalog> StarCatalog { get; set; }
    public DbSet<StarCatalog> StarVariability { get; set; }
}