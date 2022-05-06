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
                Name = "CzeV 343"
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


        builder.Entity<StarCatalog>().HasData(
            new StarCatalog
            {
                StarId = 1,
                CatalogId = "UCAC4",
                CrossId = "605-025126",
                Ra = "05:48:24.012",
                Dec = "+30:57:03.59",
                Mag = 13.71m,
                Primary = true
            }
        );

        builder.Entity<StarVariability>().HasData(
            new StarVariability
            {
                Id = 1,
                Epoch = 2455958.36058m,
                Period = 1.209373m,
                PrimaryMinimum = 13.72,
                StarId = 1,
                VariabilityType = VariabilityType.Extrinsic
            }
        );

        base.OnModelCreating(builder);
    }

    public DbSet<Star> Stars { get; set; }
    public DbSet<StarDraft> StarsDrafts { get; set; }
    public DbSet<LightCurve> LightCurves { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Observatory> Observatories { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<StarCatalog> StarCatalog { get; set; }
    public DbSet<StarCatalog> StarVariability { get; set; }
    public DbSet<StarPublish> StarPublish { get; set; }
}