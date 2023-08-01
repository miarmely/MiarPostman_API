using Entities.Models;
using Repositories.Concrete;
using Repositories.Contracts;
using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context) : base(context)
        { }


        public void CreateRole(Role entity) =>
            base.Create(entity);
        

        public IQueryable<Role> GetAllRoles(bool trackChanges) =>
            base.FindAll(trackChanges)
            .OrderBy(r => r.Id);


        public Role? GetById(int id, bool trackChanges) =>
            base.FindWithCondition(r => r.Id == id, trackChanges)
            .FirstOrDefault();


        public Role? GetByRoleName(string roleName, bool trackChanges) =>
            base.FindWithCondition(r => r.RoleName == roleName, trackChanges)
            .FirstOrDefault();
    }
}
