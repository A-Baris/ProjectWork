﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant.DAL.Context;

#nullable disable

namespace Restaurant.DAL.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Entity.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<int>("CashAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TableOfRestaurantId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CashAccountId");

                    b.HasIndex("TableOfRestaurantId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.BillCustomer", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("BillId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("BillCustomers");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.BillProduct", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("BillId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("BillProducts");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.CashAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CashAccounts");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReservationDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TableOfRestaurantId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TableOfRestaurantId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Notes")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TcNo")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Kitchen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("KitchenName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Kitchens");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TableOfRestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TableOfRestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StatusOfOrder")
                        .HasColumnType("int");

                    b.Property<int>("TableofRestaurantId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TableofRestaurantId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("KitchenId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("KitchenId");

                    b.HasIndex("MenuId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.ProductIngredient", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("ProductIngredients");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.TableOfRestaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TableCapacity")
                        .HasColumnType("int");

                    b.Property<string>("TableLocation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TableOfRestaurants");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Bill", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.CashAccount", "CashAccount")
                        .WithMany("Bills")
                        .HasForeignKey("CashAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.TableOfRestaurant", "TableOfRestaurant")
                        .WithMany("Bills")
                        .HasForeignKey("TableOfRestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CashAccount");

                    b.Navigation("TableOfRestaurant");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.BillCustomer", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Bill", "Bill")
                        .WithMany("BillCustomers")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.Customer", "Customer")
                        .WithMany("BillCustomers")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.BillProduct", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Bill", "Bill")
                        .WithMany("BillProducts")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.Product", "Product")
                        .WithMany("BillProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Customer", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.TableOfRestaurant", "TableOfRestaurant")
                        .WithMany("Customers")
                        .HasForeignKey("TableOfRestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TableOfRestaurant");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Order", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.TableOfRestaurant", "TableOfRestaurant")
                        .WithMany("Orders")
                        .HasForeignKey("TableOfRestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TableOfRestaurant");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.OrderItem", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.TableOfRestaurant", "TableOfRestaurant")
                        .WithMany("OrderItems")
                        .HasForeignKey("TableofRestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("TableOfRestaurant");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Product", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.Kitchen", "Kitchen")
                        .WithMany("Products")
                        .HasForeignKey("KitchenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.Menu", "Menu")
                        .WithMany("Products")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Kitchen");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.ProductIngredient", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Ingredient", "Ingredient")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Restaurant.Entity.Entities.Product", "Product")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.TableOfRestaurant", b =>
                {
                    b.HasOne("Restaurant.Entity.Entities.Employee", "Employee")
                        .WithMany("TableOfRestaurants")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Bill", b =>
                {
                    b.Navigation("BillCustomers");

                    b.Navigation("BillProducts");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.CashAccount", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Customer", b =>
                {
                    b.Navigation("BillCustomers");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Employee", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("TableOfRestaurants");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Ingredient", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Kitchen", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Menu", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.Product", b =>
                {
                    b.Navigation("BillProducts");

                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("Restaurant.Entity.Entities.TableOfRestaurant", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Customers");

                    b.Navigation("OrderItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
