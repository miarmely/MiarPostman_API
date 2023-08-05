using Microsoft.EntityFrameworkCore;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repositories.EFCore.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            /*
            // add new employees on database table
            builder.HasData(
                new Employee() { Id = 101, FullName = "Mert", LastName = "Akdemir", Job = "Computer Engineering", Salary = 15000 },
                new Employee() { Id = 102, FullName = "Gülay", LastName = "Akdemir", Job = "Home Women", Salary = 0 },
                new Employee() { Id = 103, FullName = "Göksel", LastName = "Akdemir", Job = "Carpanter", Salary = 8000 }
            );
            */
        }
    }
}
