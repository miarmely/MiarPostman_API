using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEmployeeRepository Employee { get; }
        IManagerRepository Manager { get; }
        IEmployeeAndRoleRepository EmployeeAndRole { get; }
        IRoleRepository Role { get; }
        IBossRepository Boss { get; }
        public void Save();  // for to save changes of all CRUD process as Dynamic
    }
}
