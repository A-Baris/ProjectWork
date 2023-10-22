using Restaurant.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly ProjectContext _context;
      

        public BaseRepository(ProjectContext context)
        {

            _context = context;
          
        }
        public string Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().Where(x => x.BaseStatus == Entity.Enums.BaseStatus.Active).ToList();
        
        }

        public async Task<T> GetbyIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public string Remove(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Update(T entity)
        {
            try
            {
                _context.Set<T>().Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}

