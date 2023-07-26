using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _manager;
       

        public RoleService(IRepositoryManager manager) =>
            _manager = manager;


        public void CreateRole(Role role)
        {
            var entity = _manager.Role.GetByRoleName(role.RoleName, false);

            // when role name is already exists
            if (entity is not null)
                throw new Exception("Conflict");

            _manager.Role.CreateRole(role);
            _manager.Save();
        }


        public IEnumerable<Role> GetAllRoles(bool trackChanges)
        {
            var entity = _manager.Role.GetAllRoles(trackChanges);

            // when database is empty
            if (entity is null)
                throw new Exception("Empty Database");

            return entity;
        }
    }
}
