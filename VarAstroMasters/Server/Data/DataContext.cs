using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Star> Stars { get; set; }
    public DbSet<LightCurve> LightCurves { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Observatory> Observatories { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<StarCatalog> StarCatalog { get; set; }
    public DbSet<StarCatalog> StarVariability { get; set; }
    public DbSet<StarPublish> StarPublish { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<UserStarIdentification> UserStarIdentifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StarCatalog>()
            .HasKey(sc => new { sc.CatalogId, sc.StarId });
        builder.Entity<UserStarIdentification>()
            .HasKey(usi => new { usi.UserId, usi.StarId });

        // When Catalog is deleted, remove all entries of stars associated with the catalog
        builder.Entity<Catalog>()
            .HasMany<StarCatalog>();

        builder.Entity<Star>().HasData(
            new Star
            {
                Id = 1,
                Name = "CzeV 343",
                RA = 89.679166d,
                DEC = 30.950833d
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
                Ra = 89.679166d,
                Dec = 30.950833d,
                Mag = 13.71d,
                Primary = true
            }
        );

        builder.Entity<StarVariability>().HasData(
            new StarVariability
            {
                Id = 1,
                Epoch = 2455958.36058d,
                Period = 1.209373d,
                PrimaryMinimum = 13.72,
                StarId = 1,
                VariabilityType = VariabilityType.Extrinsic
            }
        );

        base.OnModelCreating(builder);
    }
}