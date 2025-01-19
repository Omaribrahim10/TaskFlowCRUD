using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.DAL.Data.Contexts;
using Company.RouteMVC3.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.RouteMVC3.BL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
			if (typeof(T) == typeof(Employee))
			{
				return (IEnumerable<T>) _context.Employees.Include(E => E.WorkFor).ToList();
			}
			return _context.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Add(T entity)
        {
            _context.Add(entity);
            
        }
        public void Update(T entity)
        {
            _context.Update(entity);
            
        }
        public void Delete(T entity)
        {
            _context.Remove(entity);
            
        }

    }
}
