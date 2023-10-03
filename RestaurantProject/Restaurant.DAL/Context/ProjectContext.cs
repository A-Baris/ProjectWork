﻿
using Restaurant.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Context
{
    public class ProjectContext:DbContext
    {
        public DbSet<TableOfRestaurant> TableOfRestaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkCategory> DrinkCategories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<BillCustomer> BillCustomers { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<MenuDish> MenuDishes { get; set; }
        public DbSet<MenuDrink> MenuDrinks { get; set; }
        public DbSet<MenuBill> MenuBills { get; set; }
        public DbSet<MenuOrder> MenuOrders { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-KUQ9PNH;database=RestaurantDB;uid=sa;pwd=1234;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

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

          

            modelBuilder.Entity<MenuDish>()
                .HasKey(md => new { md.MenuId, md.DishId });

            modelBuilder.Entity<MenuDish>()
                .HasOne(md => md.Menu)
                .WithMany(m => m.MenuDishes)
                .HasForeignKey(md => md.MenuId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<MenuDish>()
              .HasOne(md => md.Dish)
              .WithMany(d => d.MenuDishes)
              .HasForeignKey(md => md.DishId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<DishCategory>()
                .HasMany(dc => dc.Dishes)
                .WithOne(d => d.DishCategory)
                .HasForeignKey(dc => dc.DishCategoryId);

            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId)
                  .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<DishIngredient>()
              .HasOne(di => di.Ingredient)
              .WithMany(d => d.DishIngredients)
               .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<DrinkCategory>()
                .HasMany(dc => dc.Drinks)
                .WithOne(d => d.DrinkCategory)
                .HasForeignKey(dc => dc.DrinkCategoryId);

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
              .HasMany(k => k.Dishes)
              .WithOne(d => d.Kitchen)
              .HasForeignKey(k => k.KitchenId);

            modelBuilder.Entity<Kitchen>()
              .HasMany(k => k.Ingredients)
              .WithOne(i => i.Kitchen)
              .HasForeignKey(k => k.KitchenId);

            modelBuilder.Entity<MenuDrink>()
                .HasKey(md => new { md.DrinkId, md.MenuId });

            modelBuilder.Entity<MenuDrink>()
                .HasOne(md => md.Drink)
                .WithMany(d => d.MenuDrinks)
                .HasForeignKey(md => md.DrinkId)
                 .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MenuDrink>()
                .HasOne(md => md.Menu)
                .WithMany(m => m.MenuDrinks)
                .HasForeignKey(md => md.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<MenuBill>()
                .HasKey(md => new { md.BillId, md.MenuId });

            modelBuilder.Entity<MenuBill>()
                .HasOne(md => md.Bill)
                .WithMany(b => b.MenuBills)
                .HasForeignKey(md => md.BillId)
                 .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MenuBill>()
                .HasOne(md => md.Menu)
                .WithMany(m => m.MenuBills)
                .HasForeignKey(md => md.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuOrder>()
                .HasKey(md => new { md.OrderId, md.MenuId });

            modelBuilder.Entity<MenuOrder>()
                .HasOne(md => md.Order)
                .WithMany(d => d.MenuOrders)
                .HasForeignKey(md => md.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<MenuOrder>()
                .HasOne(md => md.Menu)
                .WithMany(m => m.MenuOrders)
                .HasForeignKey(md => md.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngredientCategory>()
                .HasMany(c => c.Ingredients)
                .WithOne(i => i.IngredientCategory)
                .HasForeignKey(c => c.IngredientCatgoryId);



            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifierEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach(var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseEntity;
                    
                    if(item.State==EntityState.Added)
                    {
                        entityRepository.CreatedDate = DateTime.Now;
                    }
                    if (item.State == EntityState.Modified)
                    {
                        entityRepository.UpdatedDate = DateTime.Now;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return base.SaveChanges();
        }
    }
}
