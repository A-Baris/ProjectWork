using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Restaurant.Entity.Entities;
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
                 .OnDelete(DeleteBehavior.ClientSetNull);


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
                .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<MenuDish>()
              .HasOne(md => md.Dish)
              .WithMany(d => d.MenuDishes)
              .HasForeignKey(md => md.DishId)
                  .OnDelete(DeleteBehavior.ClientSetNull);


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
                   .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<DishIngredient>()
              .HasOne(di => di.Ingredient)
              .WithMany(d => d.DishIngredients)
               .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<DrinkCategory>()
                .HasMany(dc => dc.Drinks)
                .WithOne(d => d.DrinkCategory)
                .HasForeignKey(dc => dc.DrinkCategoryId);

            modelBuilder.Entity<BillCustomer>()
                .HasKey(bc => new { bc.BillId, bc.CustomerId });

            modelBuilder.Entity<BillCustomer>()
                .HasOne(bc => bc.Bill)
                .WithMany(b => b.BillCustomers)
                .HasForeignKey(bc => bc.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<BillCustomer>()
                .HasOne(bc => bc.Customer)
                .WithMany(b => b.BillCustomers)
                .HasForeignKey(bc => bc.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


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
                    .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<MenuDrink>()
                .HasOne(md => md.Menu)
                .WithMany(m => m.MenuDrinks)
                .HasForeignKey(md => md.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull);



            base.OnModelCreating(modelBuilder);
        }
    }
}
