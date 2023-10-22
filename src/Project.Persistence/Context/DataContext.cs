using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Identity;

namespace Project.Persistence.Context;

public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                            UserRole, IdentityUserLogin<int>,
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

        modelBuilder.Entity<UserRole>(userRole => {
            userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<TagCard>()
            .HasKey(T => new {T.CardId, T.TagId});
    }
}