
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
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

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

        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<OrderDrink> OrderDrinks { get; set; }
        public DbSet<BillDish> BillDishes { get; set; }
        public DbSet<BillDrink> BillDrinks { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }

      

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

            modelBuilder.Entity<OrderDish>()
            .HasKey(od => new { od.OrderId, od.DishId });

            modelBuilder.Entity<OrderDish>()
                .HasOne(od => od.Order)
                .WithMany(x => x.OrderDishes)
                .HasForeignKey(od => od.OrderId);
               


            modelBuilder.Entity<OrderDish>()
          .HasOne(od => od.Dish)
          .WithMany(x => x.OrderDishes)
          .HasForeignKey(od => od.DishId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDrink>()
          .HasKey(od => new { od.OrderId, od.DrinkId });

            modelBuilder.Entity<OrderDrink>()
                .HasOne(od => od.Order)
                .WithMany(x => x.OrderDrinks)
                .HasForeignKey(od => od.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDrink>()
          .HasOne(od => od.Drink)
          .WithMany(x => x.OrderDrinks)
          .HasForeignKey(od => od.DrinkId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillDish>()
           .HasKey(b => new { b.BillId, b.DishId });

            modelBuilder.Entity<BillDish>()
       .HasOne(bd => bd.Bill)
       .WithMany(x => x.BillDishes)
       .HasForeignKey(bd => bd.BillId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillDish>()
          .HasOne(bd => bd.Dish)
           .WithMany(x => x.BillDishes)
          .HasForeignKey(bd => bd.DishId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillDrink>()
       .HasKey(b => new { b.BillId, b.DrinkId });

            modelBuilder.Entity<BillDrink>()
       .HasOne(bd => bd.Bill)
       .WithMany(x => x.BillDrinks)
       .HasForeignKey(bd => bd.BillId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillDrink>()
          .HasOne(bd => bd.Drink)
           .WithMany(x => x.BillDrinks)
          .HasForeignKey(bd => bd.DrinkId)
              .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<IngredientCategory>()
                .HasMany(c => c.Ingredients)
                .WithOne(i => i.IngredientCategory)
                .HasForeignKey(c => c.IngredientCatgoryId);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Dishes)
                .WithOne(d => d.Menu)
                .HasForeignKey(m => m.MenuId);

            modelBuilder.Entity<Menu>()
             .HasMany(m => m.Drinks)
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
