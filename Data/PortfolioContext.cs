using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portfolio.Data.Entities;
//using Portfolio.

namespace Portfolio.Data
{

  // mPerholtz Point Of Contention wether to use IdentityDbContext or just DbContext
  // IdentityContext inherits from DbContext so add's asp.net identity support (if needed)
  public class PortfolioContext : DbContext
  {
    private readonly IConfiguration _config;

    public PortfolioContext(DbContextOptions<PortfolioContext> options, IConfiguration config)
      : base(options)
    {
      _config = config;
    }

    public DbSet<ContactUsMessage> ContactUsMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(_config.GetConnectionString("BabylonSystemDb"),
        sqlOptions => sqlOptions.UseNetTopologySuite()
      );
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    {
      base.OnModelCreating(bldr);

      bldr.Entity<ContactUsMessage>(e =>
      {
        e.ToTable("ContactUsMessage");
      });

    }
  }
}