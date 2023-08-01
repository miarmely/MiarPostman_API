using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = 1, RoleName = "Admin" },
                new Role() { Id = 2, RoleName = "Client" },
                new Role() { Id = 3, RoleName = "Manager" },
                new Role() { Id = 4, RoleName = "Guest" }
            );  
        }
    }
}
