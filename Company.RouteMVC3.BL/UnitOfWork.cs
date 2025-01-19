using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.BL.Repositories;
using Company.RouteMVC3.DAL.Data.Contexts;

namespace Company.RouteMVC3.BL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
            _departmentRepository = new DepartmentRepository(context);
            _context = context;
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
