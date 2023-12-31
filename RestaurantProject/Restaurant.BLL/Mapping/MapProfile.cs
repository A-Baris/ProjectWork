﻿using AutoMapper;
using Restaurant.BLL.AbstractServices;

using Restaurant.Entity.DTOs;
using Restaurant.Entity.Entities;
using Restaurant.Entity.ViewModels;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using Restaurant.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Supplier,SupplierVM>().ReverseMap(); 
            CreateMap<Category,CategoryVm>().ReverseMap(); 
            CreateMap<Product,ProductVM>().ReverseMap(); 
            CreateMap<Product,ProductUpdateVM>().ReverseMap(); 
            CreateMap<AccountingTransaction,TransactionVM>().ReverseMap(); 
            CreateMap<Reservation,ReservationDTO>().ReverseMap(); 
            CreateMap<Reservation,ReservationUpdateDTO>().ReverseMap();
            CreateMap<Reservation,ReservationVM>().ReverseMap(); 
            CreateMap<Reservation,ReservationCreateVM>().ReverseMap(); 
            
            CreateMap<Customer,CustomerDTO>().ReverseMap();
            CreateMap<Customer,CustomerVM>().ReverseMap();
            CreateMap<Order,OrderVM>().ReverseMap();
            CreateMap<Order,OrderItemCreateVM>().ReverseMap();
            CreateMap<Employee,EmployeeVM>().ReverseMap();
            CreateMap<Ingredient,IngredientVM>().ReverseMap();
            CreateMap<Kitchen,KitchenVM>().ReverseMap();
            CreateMap<Menu,MenuVM>().ReverseMap();
            CreateMap<TableOfRestaurant,TableOfRestaurantVM>().ReverseMap();
            CreateMap<Invoice,InvoiceVM>().ReverseMap();
          
     


        }
    }
}
