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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        //private readonly AppDbContext _context;

        //private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext context) :base(context) 
        {
           
        }

		public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
		{
			return await _context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.WorkFor).ToListAsync();
		}

		//public IEnumerable<Employee> GetAll()
		//{
		//   return _context.Employees.ToList();
		//}
		//public Employee Get(int id)
		//{
		//    return _context.Employees.Find();
		//}

		//public int Add(Employee entity)
		//{
		//     _context.Employees.Add(entity);
		//    return _context.SaveChanges();
		//}

		//public int Update(Employee entity)
		//{
		//    _context.Employees.Update(entity);
		//    return _context.SaveChanges();
		//}
		//public int Delete(Employee entity)
		//{
		//    _context.Employees.Remove(entity);
		//    return _context.SaveChanges();
		//}


	}
}
