using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.DAL.Data.Contexts;
using Company.RouteMVC3.DAL.Models;

namespace Company.RouteMVC3.BL.Repositories
{
	//CLR
	public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
	{
		//private readonly AppDbContext _context;

		public DepartmentRepository(AppDbContext context) :base(context) // ASK CLR to Create Object from AppDbContext
		{
			
		}
		//public IEnumerable<Department> GetAll()
		//{
		//	return _context.Departments.ToList();
		//}
		//public Department Get(int id)
		//{
		//	//return _context.Departments.FirstOrDefault(D => D.Id == id);
		//	return _context.Departments.Find(id);
		//}


		//public int Add(Department entity)
		//{
		//	_context.Departments.Add(entity);
		//	return _context.SaveChanges();
		//}

		//public int Update(Department entity)
		//{
		//	_context.Departments.Update(entity);
		//	return _context.SaveChanges();
		//}

		//public int Delete(Department entity)
		//{
		//	_context.Departments.Remove(entity);
		//	return _context.SaveChanges();
		//}





	}
}
