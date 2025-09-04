using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChoTotAsp.Entity
{
    public partial class DBConnection : DbContext
    {
        public DBConnection()
            : base("name=DBConnection")
        {
        }

        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<cart_items> cart_items { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<order_items> order_items { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product_comments> product_comments { get; set; }
        public virtual DbSet<product_images> product_images { get; set; }
        public virtual DbSet<product_variants> product_variants { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cart>()
                .HasMany(e => e.cart_items)
                .WithOptional(e => e.cart)
                .WillCascadeOnDelete();

            modelBuilder.Entity<cart_items>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<order_items>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.total_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<order>()
                .HasMany(e => e.order_items)
                .WithOptional(e => e.order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<product_comments>()
                .HasMany(e => e.product_comments1)
                .WithOptional(e => e.product_comments2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<product_variants>()
                .Property(e => e.additional_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_comments)
                .WithOptional(e => e.product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_variants)
                .WithOptional(e => e.product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<product>()
                .HasMany(e => e.reviews)
                .WithOptional(e => e.product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .HasMany(e => e.carts)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .HasMany(e => e.product_comments)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .HasMany(e => e.reviews)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();
        }
    }
}
