﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteMVC3.BL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get;}
        public IEmployeeRepository EmployeeRepository { get;}
        int Complete();
    }
}
