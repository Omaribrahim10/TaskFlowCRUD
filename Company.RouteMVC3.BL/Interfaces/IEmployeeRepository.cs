﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.RouteMVC3.DAL.Models;

namespace Company.RouteMVC3.BL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        IEnumerable<Employee>GetByName(string name);

        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);

        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);

    }
}
