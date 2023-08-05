using Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleService
    {
        void CreateRole(Role role);
        IEnumerable<Role> GetAllRoles(bool trackChanges);
    }
}
