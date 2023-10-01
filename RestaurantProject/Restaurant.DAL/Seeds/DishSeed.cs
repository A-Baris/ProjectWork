using Restaurant.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Seeds
{
    internal class DishSeed : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasData(new Dish
            {
                Id = 1,
                DishName = "Hamburger",
                Price = 100,
                Quantity = 1,
              

            },
            new Dish
            {
                Id = 2,
                DishName = "Kebab",
                Price = 150,
                Quantity = 2,
                
            });
        }
    }
}
