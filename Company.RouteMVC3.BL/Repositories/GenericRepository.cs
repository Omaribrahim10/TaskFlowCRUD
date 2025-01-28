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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
			if (typeof(T) == typeof(Employee))
			{
				return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).ToListAsync();
			}
			return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            
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
