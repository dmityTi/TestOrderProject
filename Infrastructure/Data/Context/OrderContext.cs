using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context
{
 public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(ConfigureOrder);
            modelBuilder.Entity<Article>(ConfigureOrder);
            modelBuilder.Entity<Payment>(ConfigureOrder);
            modelBuilder.Entity<BillingAddress>(ConfigureOrder);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BillingAddress)
                .WithOne(p => p.Order)
                .HasForeignKey<Order>(p => p.BillingAddressId);
        }

        private void ConfigureOrder(EntityTypeBuilder<Article> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(p => p.Title)
                .HasMaxLength(250)
                .IsRequired();

            entity.HasOne(d => d.Order)
                .WithMany(p => p.Articles)
                .HasForeignKey(d => d.OrderId);
        }

        private void ConfigureOrder(EntityTypeBuilder<Payment> entity)
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(p => p.MethodName)
                .HasMaxLength(100)
                .IsRequired();

            
            entity.HasOne(d => d.Order)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId);
        }

        private void ConfigureOrder(EntityTypeBuilder<BillingAddress> entity)
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(p => p.Email)
                .HasMaxLength(200)
                .IsRequired();
            
            entity.Property(p => p.FullName)
                .HasMaxLength(250)
                .IsRequired();
            
            entity.Property(p => p.City)
                .HasMaxLength(100)  
                .IsRequired();
            
            entity.Property(p => p.Street)
                .HasMaxLength(250)
                .IsRequired();
        }
        
    }
}