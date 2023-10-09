﻿
using Restaurant.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Restaurant.DAL.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<TableOfRestaurant> TableOfRestaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<BillCustomer> BillCustomers { get; set; }


        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<BillProduct> BillProducts { get; set; }



      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Waiter>()
              .HasMany(w => w.TableOfRestaurants)
              .WithOne(t => t.Waiter)
              .HasForeignKey(t => t.WaiterId);



            modelBuilder.Entity<Waiter>()
                .HasMany(w => w.Orders)
                .WithOne(o => o.Waiter)
                .HasForeignKey(o => o.WaiterId)
              .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<TableOfRestaurant>()
                .HasMany(t => t.Orders)
                .WithOne(o => o.TableOfRestaurant)
                .HasForeignKey(o => o.TableofRestaurantId);

            modelBuilder.Entity<TableOfRestaurant>()
           .HasMany(t => t.Bills)
           .WithOne(b => b.TableOfRestaurant)
           .HasForeignKey(b => b.TableOfRestaurantId);

            modelBuilder.Entity<TableOfRestaurant>()
           .HasMany(t => t.Customers)
           .WithOne(c => c.TableOfRestaurant)
           .HasForeignKey(c => c.TableOfRestaurantId);






            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(c => c.CategoryId);



            modelBuilder.Entity<BillCustomer>()
                .HasKey(bc => new { bc.BillId, bc.CustomerId });

            modelBuilder.Entity<BillCustomer>()
                .HasOne(bc => bc.Bill)
                .WithMany(b => b.BillCustomers)
                .HasForeignKey(bc => bc.BillId);



            modelBuilder.Entity<BillCustomer>()
                .HasOne(bc => bc.Customer)
                .WithMany(b => b.BillCustomers)
                .HasForeignKey(bc => bc.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CashAccount>()
                .HasMany(ca => ca.Bills)
                .WithOne(b => b.CashAccount)
                .HasForeignKey(ca => ca.CashAccountId);

            modelBuilder.Entity<Kitchen>()
                .HasMany(k => k.Orders)
                .WithOne(o => o.Kitchen)
                .HasForeignKey(k => k.KitchenId);

            modelBuilder.Entity<Kitchen>()
              .HasMany(k => k.Products)
              .WithOne(p => p.Kitchen)
              .HasForeignKey(k => k.KitchenId);


            modelBuilder.Entity<OrderProduct>()
            .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(od => od.Order)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(od => od.OrderId);



            modelBuilder.Entity<OrderProduct>()
          .HasOne(od => od.Product)
          .WithMany(x => x.OrderProducts)
          .HasForeignKey(od => od.ProductId)
           .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<BillProduct>()
           .HasKey(b => new { b.BillId, b.ProductId });

            modelBuilder.Entity<BillProduct>()
       .HasOne(bd => bd.Bill)
       .WithMany(x => x.BillProducts)
       .HasForeignKey(bd => bd.BillId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillProduct>()
          .HasOne(bd => bd.Product)
           .WithMany(x => x.BillProducts)
          .HasForeignKey(bd => bd.ProductId)
              .OnDelete(DeleteBehavior.Restrict);






            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Products)
                .WithOne(d => d.Menu)
                .HasForeignKey(m => m.MenuId);






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
