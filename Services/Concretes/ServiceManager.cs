using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;

        private readonly Lazy<IRoleService> _roleService;

        private readonly Lazy<IEmployeeAndRoleService> _employeeAndRoleService;

        private readonly Lazy<IViewConverterService> _viewConverterService;

        private readonly Lazy<IDataConverterService> _dataConverterService;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IRoleService RoleSevice => _roleService.Value;
        public IEmployeeAndRoleService EmployeeAndRoleService => _employeeAndRoleService.Value;
        public IViewConverterService ViewConverterService => _viewConverterService.Value;
        public IDataConverterService DataConverterService => _dataConverterService.Value;

        
        public ServiceManager(IRepositoryManager manager)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(manager));
            _roleService = new Lazy<IRoleService>(() => new RoleService(manager));
            _employeeAndRoleService = new Lazy<IEmployeeAndRoleService>(() => new EmployeeAndRoleService(manager));
            _viewConverterService = new Lazy<IViewConverterService>(() => new ViewConverterService());
            _dataConverterService = new Lazy<IDataConverterService>(() => new DataConverterService());
        }
    }
}