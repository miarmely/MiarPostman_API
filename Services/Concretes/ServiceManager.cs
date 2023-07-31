using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;

        private readonly Lazy<IRoleService> _roleService;

        private readonly Lazy<IEmployeeAndRoleService> _employeeAndRoleService;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IRoleService RoleSevice => _roleService.Value;
        public IEmployeeAndRoleService EmployeeAndRoleService => _employeeAndRoleService.Value;


        public ServiceManager(IRepositoryManager manager)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(manager));
            _roleService = new Lazy<IRoleService>(() => new RoleService(manager));
            _employeeAndRoleService = new Lazy<IEmployeeAndRoleService>(() => new EmployeeAndRoleService(manager));
        }
    }
}