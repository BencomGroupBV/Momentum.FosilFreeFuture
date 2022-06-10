using Microsoft.EntityFrameworkCore;

namespace Benchain.FosilFreeFuture.Web.Models.DbEntities;

public class MomentumContext : DbContext
{
  public MomentumContext(DbContextOptions<MomentumContext> options) : base(options)
  {
  }

  public DbSet<ProfileDb> ProfileDb { get; set; }
  public DbSet<PortfolioDb> PortfolioDb { get; set; }
  public DbSet<BadgeDb> BadgeDb { get; set; }
  public DbSet<ProjectDb> ProjectDb { get; set; }
  public DbSet<ApprovedProjectsDb> ApprovedProjectsDb { get; set; }
  public DbSet<ParticipantDb> ParticipantDb { get; set; }
  public DbSet<FundedProjectDb> FundedProjectDb { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ProfileDb>().ToTable("Profile");
    modelBuilder.Entity<PortfolioDb>().ToTable("Portfolio");
    modelBuilder.Entity<BadgeDb>().ToTable("Badge");
    modelBuilder.Entity<ProjectDb>().ToTable("Project");
    modelBuilder.Entity<ApprovedProjectsDb>().ToTable("ApprovedProjects");
    modelBuilder.Entity<ParticipantDb>().ToTable("Participant");
    modelBuilder.Entity<FundedProjectDb>().ToTable("FundedProject");
  }

}




