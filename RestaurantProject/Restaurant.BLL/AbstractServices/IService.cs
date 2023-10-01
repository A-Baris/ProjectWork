using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.AbstractServices
{
    public interface IService<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetbyIdAsync(int id);
        string Create(T entity);
        string Update(T entity);
        string Remove(T entity);
    }
}
