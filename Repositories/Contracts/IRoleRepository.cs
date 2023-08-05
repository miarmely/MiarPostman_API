using Entities.DataModels;
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
        void CreateRole(Role entity);
        IQueryable<Role> GetAllRoles(bool trackChanges);
        Role? GetById(int id, bool trackChanges);
        Role? GetByRoleName(string roolName, bool trackChanges);
    }
}
