using Restaurant.Entity.Entities;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity

    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public string Create(T entity)
        {
            return _repository.Create(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<T> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }

        public string Remove(T entity)
        {
            return _repository.Remove(entity);
        }

        public string Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
