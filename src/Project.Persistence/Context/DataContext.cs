using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Identity;

namespace Project.Persistence.Context;

public class DataContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>,
                                            IdentityUserRole<int>, IdentityUserLogin<int>,
                                            IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<ProjectList> Projects { get; set; }
    public DbSet<Approver> Approvers { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<TagCard> TagCards { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<TaskProject> Tasks { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<ProjectRole> ProjectRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectRole>()
            .HasData(new List<ProjectRole>(){
                new ProjectRole(1, "SuperAdmin"),
                new ProjectRole(2, "Admin"),
                new ProjectRole(3, "Leader"),
                new ProjectRole(4, "User"),
            });

        modelBuilder.Entity<User>(b =>
        {
            b.Property(u => u.Id).ValueGeneratedOnAdd();
            b.HasKey(u => u.Id);
        });

        modelBuilder.Entity<TagCard>()
            .HasKey(T => new {T.CardId, T.TagId});
    }
}