
// Interfaces
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripFront.Data;
using TripFront.Models;

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

        // Configure the relationship from Family side
        builder.Entity<FamilyFriendship>().HasOne(ff => ff.Family)
            .WithMany(f => f.SentFriendshipRequests)
            .HasForeignKey(ff => ff.FamilyId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete cycles

        // Configure the relationship from FriendFamily side
        builder.Entity<FamilyFriendship>().HasOne(ff => ff.FriendFamily)
            .WithMany(f => f.ReceivedFriendshipRequests)
            .HasForeignKey(ff => ff.FriendFamilyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Additional configurations
        builder.Entity<FamilyFriendship>().Property(ff => ff.FriendshipDate)
            .HasDefaultValueSql("GETUTCDATE()"); // SQL Server
                                                 // For PostgreSQL: .HasDefaultValueSql("CURRENT_TIMESTAMP")

        builder.Entity<FamilyFriendship>().Property(ff => ff.Status)
            .HasDefaultValue(FriendshipStatus.Pending)
            .HasConversion<int>(); // Store enum as int

        builder.Entity<FamilyFriendship>().Property(ff => ff.RequestMessage)
            .HasMaxLength(500);

        // Index for performance
        builder.Entity<FamilyFriendship>().HasIndex(ff => ff.Status);
        builder.Entity<FamilyFriendship>().HasIndex(ff => ff.FriendshipDate);

        builder.Entity<FamilyInterest>().HasOne(fi => fi.Family)
           .WithMany(f => f.FamilyInterests)
           .HasForeignKey(fi => fi.FamilyId);

        builder.Entity<FamilyInterest>().HasOne(fi => fi.Interest)
           .WithMany(i => i.FamilyInterests)
           .HasForeignKey(fi => fi.InterestId);
        builder.Entity<Trip>()
       .HasOne(t => t.OwnerFamily)
       .WithMany()
       .HasForeignKey(t => t.OwnerFamilyId)
       .OnDelete(DeleteBehavior.NoAction); // or NoAction

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
