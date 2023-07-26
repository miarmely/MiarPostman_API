using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.EFCore.Config;


namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext  // our database
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeAndRole> EmployeesAndRoles { get; set; }  
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Maid> Maids { get; set; }


        public RepositoryContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfig());
            builder.ApplyConfiguration(new ManagerConfig());
            builder.ApplyConfiguration(new RoleConfig());
        }
    }
}
