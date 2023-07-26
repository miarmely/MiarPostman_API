using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class EmployeeAndRoleService
    {
        private readonly IRepositoryManager _manager;

        public EmployeeAndRoleService(IRepositoryManager manager) =>
            _manager = manager;


        public List<string> FillRoles(int id)
        {
            var roles = new List<string>();
            var empAndRoles = _manager.EmployeeAndRole.FindByEmployeeId(id, false);
            

            foreach (var empAndRole in empAndRoles)
                roles.Add(
                    _manager.Role.GetById(empAndRole.RoleId, false)
                    .RoleName);

            return roles;
        }
    }
}
