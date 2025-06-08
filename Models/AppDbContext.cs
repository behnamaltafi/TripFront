
// Interfaces
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripFront.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Main Entities
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Family> Families { get; set; }
    public DbSet<DebtRecord> DebtRecords { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    // Join Tables
    public DbSet<TripFamily> TripFamilies { get; set; }
    public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; }
    public DbSet<FamilyFriendship> FamilyFriendships { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        // Friendships
        builder.Entity<FamilyFriendship>()
            .HasOne(f => f.Family1)
            .WithMany(f => f.FriendshipsA)
            .HasForeignKey(f => f.FamilyId1)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FamilyFriendship>()
            .HasOne(f => f.Family2)
            .WithMany(f => f.FriendshipsB)
        
            .HasForeignKey(f => f.FamilyId2)
            .OnDelete(DeleteBehavior.Restrict);

        // Friend Requests
        builder.Entity<FriendRequest>()
            .HasOne(r => r.SenderFamily)
            .WithMany(f => f.SentFriendRequests)
            .HasForeignKey(r => r.SenderFamilyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FriendRequest>()
            .HasOne(r => r.ReceiverFamily)
            .WithMany(f => f.ReceivedFriendRequests)
            .HasForeignKey(r => r.ReceiverFamilyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure relationships
        builder.Entity<TripFamily>()
            .HasKey(tf => new { tf.Id, tf.FamilyId });

        builder.Entity<TripFamily>()
            .HasOne(tf => tf.Trip)
            .WithMany(t => t.Families)
            .HasForeignKey(tf => tf.TripId);

        builder.Entity<TripFamily>()
            .HasOne(tf => tf.Family)
            .WithMany(f => f.TripFamilies)
            .HasForeignKey(tf => tf.FamilyId);

        builder.Entity<ExpenseParticipant>()
            .HasKey(ep => new { ep.ExpenseId, ep.FamilyId });

        builder.Entity<ExpenseParticipant>()
            .HasOne(ep => ep.Expense)
            .WithMany(e => e.Participants)
            .HasForeignKey(ep => ep.ExpenseId).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ExpenseParticipant>()
            .HasOne(ep => ep.Family)
            .WithMany()
            .HasForeignKey(ep => ep.FamilyId);


        // Configure decimal precision for money values
        builder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<DebtRecord>()
            .HasOne(d => d.FromFamily)
            .WithMany()
            .HasForeignKey(d => d.FromFamilyId)
            .OnDelete(DeleteBehavior.NoAction); // Keep cascade if needed

        builder.Entity<DebtRecord>()
            .HasOne(d => d.ToFamily)
            .WithMany()
            .HasForeignKey(d => d.ToFamilyId)
            .OnDelete(DeleteBehavior.NoAction);

    }


}
