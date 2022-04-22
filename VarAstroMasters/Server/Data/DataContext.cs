using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Star>().HasData(
            new Star
            {
                Id = 1,
                Name = "CzeV 612",
                RA = 123.456m
            }
        );
        base.OnModelCreating(builder);
    }
    
    public DbSet<Star> Stars { get; set; }
    public DbSet<LightCurve> LightCurves { get; set; }
}
