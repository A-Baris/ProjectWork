
using Restaurant.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;

namespace Restaurant.DAL.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<TableOfRestaurant> TableOfRestaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountingTransaction> AccountingTransactions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("server=DESKTOP-KUQ9PNH;database=RestaurantDB;uid=sa;pwd=1234;TrustServerCertificate=True");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);


            modelBuilder.Entity<Employee>()
              .HasMany(w => w.TableOfRestaurants)
              .WithOne(t => t.Employee)
              .HasForeignKey(t => t.EmployeeId);



            modelBuilder.Entity<Employee>()
                .HasMany(w => w.Orders)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeId)
              .OnDelete(DeleteBehavior.Restrict);





            modelBuilder.Entity<TableOfRestaurant>()
           .HasMany(t => t.Customers)
           .WithOne(c => c.TableOfRestaurant)
           .HasForeignKey(c => c.TableOfRestaurantId);

            modelBuilder.Entity<TableOfRestaurant>()
                .HasMany(t => t.Reservations)
                .WithOne(r => r.TableOfRestaurant)
                .HasForeignKey(r => r.TableOfRestaurantId)
                 .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Category>()
              .HasMany(c => c.Ingredients)
              .WithOne(i => i.Category)
              .HasForeignKey(c => c.CategoryId);



            modelBuilder.Entity<Kitchen>()
              .HasMany(k => k.Products)
              .WithOne(p => p.Kitchen)
              .HasForeignKey(k => k.KitchenId);









            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Products)
                .WithOne(d => d.Menu)
                .HasForeignKey(m => m.MenuId);


            modelBuilder.Entity<ProductIngredient>()
                .HasKey(x => new { x.ProductId, x.IngredientId });
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(i => i.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(i => i.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Ingredients)
                .WithOne(i => i.Supplier)
                .HasForeignKey(s => s.SupplierId);

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Transactions)
                .WithOne(t => t.Supplier)
                .HasForeignKey(s => s.SupplierId);

            modelBuilder.Entity<Supplier>()
              .HasMany(s => s.Products)
              .WithOne(p => p.Supplier)
              .HasForeignKey(s => s.SupplierId);


            modelBuilder.Entity<AccountingTransaction>()
               .Property(a => a.RemainingDebt)
               .HasComputedColumnSql("[Debit] - [Payment]");


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifierEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach (var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseEntity;
                    if (entityRepository != null)
                    {

                        if (item.State == EntityState.Added)
                        {
                            entityRepository.CreatedDate = DateTime.Now;
                        }
                        if (item.State == EntityState.Modified)
                        {
                            entityRepository.UpdatedDate = DateTime.Now;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return base.SaveChanges();
        }
    }
}
