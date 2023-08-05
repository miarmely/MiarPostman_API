using Entities.DataModels;
using Entities.RelationModels;
using Entities.ViewModels;
using Repositories.Contracts;
using Services.Contracts;


namespace Services.Concretes
{
    public class EmployeeAndRoleService : IEmployeeAndRoleService
    {
        private readonly IRepositoryManager _manager;


        public EmployeeAndRoleService(IRepositoryManager manager) =>
            _manager = manager;


        public void CreateEmpAndRole(EmployeeView employeeView)
        {
            foreach (var roleName in employeeView.Roles)
            {
                var role = _manager
                    .Role
                    .GetByRoleName(roleName, false);

                // create employeeAndRole
                _manager
                    .EmployeeAndRole
                    .CreateEmployeeAndRole(new EmployeeAndRole()
                    {
                        EmployeeId = Convert.ToInt32(employeeView.Id),
                        RoleId = role.Id
                    });
            }

            _manager.Save();
        }


        public List<EmployeeView> FillRoles(List<EmployeeView> employeeViews)
        {
            // add roles of employee
            foreach (var employeeView in employeeViews)
                employeeView.Roles = AddRoles(
                    Convert.ToInt32(employeeView.Id));

            return employeeViews;
        }


        public EmployeeView FillRole(EmployeeView employeeView)
        {
            employeeView.Roles = AddRoles(
                Convert.ToInt32(employeeView.Id));

            return employeeView;
        }


        public void UpdateRelations(EmployeeView employeeView)
        {
            var empAndRoles = _manager.EmployeeAndRole
                .FindByEmployeeId(Convert.ToInt32(employeeView.Id), false)
                .ToList();

            // update
            DeleteIfNotExistsView(employeeView, empAndRoles);
            AddIfNotExistsDatabase(employeeView, empAndRoles);

            _manager.Save();
        }


        public void DeleteEmpAndRolesByEmployeeId(int employeeId)
        {
            var empAndRoles = _manager
                .EmployeeAndRole
                .FindByEmployeeId(employeeId, false)
                .ToList();

            // delete
            _manager
                .EmployeeAndRole
                .DeleteEmployeeAndRoles(empAndRoles);

            _manager.Save();
        }


        public void MultiDeleteEmpAndRoles(List<Employee> employees)
        {
            foreach (var employee in employees)
                DeleteEmpAndRolesByEmployeeId(employee.Id);
        }


        private List<string> AddRoles(int employeeId)
        {
            // get employeeAndRole with matched employee
            var empAndRoles = _manager.EmployeeAndRole
                .FindByEmployeeId(employeeId, false)
                .ToList();  //  disconnect from database to close the DataReader.

            // add role names to list
            var roleNames = empAndRoles
                .Select(er => _manager.Role
                .GetById(er.RoleId, false)
                    .RoleName)
                .ToList();

            return roleNames;
        }


        private void AddIfNotExistsDatabase(EmployeeView employeeView, List<EmployeeAndRole> empAndRoles)
        {
            // when exists on view but not exists on database
            foreach (var roleName in employeeView.Roles)
            {
                var roleId = _manager.Role
                    .GetByRoleName(roleName, false)
                    .Id;

                // add employeeAndRole
                if (!empAndRoles.Any(er => er.RoleId == roleId))
                    _manager.EmployeeAndRole.CreateEmployeeAndRole(new EmployeeAndRole()
                    {
                        EmployeeId = Convert.ToInt32(employeeView.Id),
                        RoleId = roleId
                    });
            }
        }


        private void DeleteIfNotExistsView(EmployeeView employeeView, List<EmployeeAndRole> empAndRoles)
        {
            // when exists on database but not exists on view
            foreach (var empAndRole in empAndRoles)
            {
                var roleName = _manager.Role.GetById(empAndRole.RoleId, false)
                    .RoleName;

                // delete empAndRole
                if (!employeeView.Roles.Contains(roleName))
                    _manager.EmployeeAndRole.DeleteEmployeeAndRole(empAndRole);
            }
        }
    }
}
