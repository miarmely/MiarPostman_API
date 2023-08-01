using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        public void CreateRole(Role entity);
        public IQueryable<Role> GetAllRoles(bool trackChanges);
        public Role? GetById(int id, bool trackChanges);
        public Role? GetByRoleName(string roolName, bool trackChanges);
    }
}
