using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleService
    {
        public void CreateRole(Role role);
        public IEnumerable<Role> GetAllRoles(bool trackChanges);
    }
}
