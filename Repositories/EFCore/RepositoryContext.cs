using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.EFCore.Config;
using Entities.RelationModels;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext  // our database
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeAndRole> EmployeesAndRoles { get; set; }  
        

        public RepositoryContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfig());
            builder.ApplyConfiguration(new RoleConfig());
        }
    }
}
