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

        private readonly Lazy<IManagerService> _managerService;

        private readonly Lazy<IRoleService> _roleService;

        private readonly Lazy<IBossService> _bossService;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IManagerService ManagerService => _managerService.Value;
        public IRoleService RoleSevice => _roleService.Value;
        public IBossService BossService => _bossService.Value; 


        public ServiceManager(IRepositoryManager manager)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(manager));
            _managerService = new Lazy<IManagerService>(() => new ManagerService(manager));
            _roleService = new Lazy<IRoleService>(() => new RoleService(manager));
            _bossService = new Lazy<IBossService>(() => new BossService(manager));
        }
    }
}
