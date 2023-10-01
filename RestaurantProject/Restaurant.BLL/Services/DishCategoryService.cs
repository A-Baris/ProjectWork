using Entity.Entities;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class DishCategoryService : BaseService<DishCategory>, IDishCategoryService
    {
        //private readonly IRepository<DishCategory> _repository;

        //public DishCategoryService(IRepository<DishCategory> repository)
        //{
        //    _repository = repository;
        //}
        //public string Create(DishCategory entity)
        //{
        //    return _repository.Create(entity);
        //}

        //public IEnumerable<DishCategory> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        //public Task<DishCategory> GetbyIdAsync(int id)
        //{
        //    return _repository.GetbyIdAsync(id);
        //}

        //public string Remove(DishCategory entity)
        //{
        //  return _repository.Remove(entity);
        //}

        //public string Update(DishCategory entity)
        //{
        //    return _repository.Update(entity);
        //}
        public DishCategoryService(IRepository<DishCategory> repository) : base(repository)
        {
        }
    }
}
