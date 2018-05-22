using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAuction.Core.Entities;
using OnlineAuction.Infrastructure.Identity;

namespace OnlineAuction.Infrastructure
{
    public class OnlineAuctionDbContext : IdentityDbContext<AppUser>
    {
        public OnlineAuctionDbContext(DbContextOptions<OnlineAuctionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lot> Lots { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LotState> LotStates { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lot>(ConfigureLot);
            modelBuilder.Entity<Category>(ConfigureCategory);
            modelBuilder.Entity<Bid>(ConfigureBid);
            modelBuilder.Entity<AppUser>(ConfigureUser);
        }

        private void ConfigureLot(EntityTypeBuilder<Lot> typeBuilder)
        {
            typeBuilder.Property(l => l.LotName)
                .IsRequired(true)
                .HasMaxLength(50);

            typeBuilder.Property(l => l.Description)
                .IsRequired(true)
                .HasMaxLength(255);

            typeBuilder.Property(l => l.StartPrice)
                .IsRequired(true);

            typeBuilder.Property(l => l.EndTime)
                .IsRequired(true);
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> typeBuilder)
        {
            // Self relationship.
            typeBuilder.HasKey(c => c.CategoryID);

            typeBuilder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID);
        }

        private void ConfigureBid(EntityTypeBuilder<Bid> typeBuilder)
        {
            typeBuilder.Property(b => b.Amount)
                .IsRequired(true);
        }

        private void ConfigureUser(EntityTypeBuilder<AppUser> typeBuilder)
        {
            typeBuilder.HasMany(u => u.Bids)
                .WithOne();

            typeBuilder.HasMany(au => au.Lots)
                .WithOne();
        }
    }
}
