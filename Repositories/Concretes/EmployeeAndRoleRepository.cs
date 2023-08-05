using Entities.RelationModels;
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
    public class EmployeeAndRoleRepository : RepositoryBase<EmployeeAndRole>, IEmployeeAndRoleRepository
    {
        public EmployeeAndRoleRepository(RepositoryContext context) : base(context)
        { }


        public void CreateEmployeeAndRole(EmployeeAndRole entity) => Create(entity);


        public IQueryable<EmployeeAndRole> FindAllEmployeesAndRoles(bool trackChanges) =>
            base.FindAll(trackChanges)
            .OrderBy(k => k.Id);


        public IQueryable<EmployeeAndRole> FindByEmployeeId(int employeeId, bool trackChanges) =>
            base.FindWithCondition(e => e.EmployeeId == employeeId, trackChanges)
            .OrderBy(e => e.EmployeeId);


        public EmployeeAndRole FindByEmployeeAndRoleId(int employeeId, int roleId, bool trackChanges) =>
            base.FindWithCondition(e => e.EmployeeId == employeeId
                && e.RoleId == roleId
                , trackChanges)
            .First();
        

        public void DeleteEmployeeAndRole(EmployeeAndRole empAndRole) =>
            base.Delete(empAndRole);


        public void DeleteEmployeeAndRoles(List<EmployeeAndRole> empAndRole) =>
            base.MultipleDelete(empAndRole);
    }
}
