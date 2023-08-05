using Entities.DataModels;
using Entities.ViewModels;
using Services.Contracts;


namespace Services.Concretes
{
    public class ViewConverterService : IViewConverterService
    {
        public EmployeeView ConvertToEmployeeView(Employee employee)
        {
            return new EmployeeView()
            {
                Id = employee.Id,
                FullName = employee.FullName,
                LastName = employee.LastName,
                Job = employee.Job,
                Salary = employee.Salary,
                RegisterDate = employee.RegisterDate.ToString()
            };
        }


        public List<EmployeeView> MultipleConvertToEmployeeView(List<Employee> employeeList)
        {
            return employeeList
                .Select(e => new EmployeeView()
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    LastName = e.LastName,
                    Job = e.Job,
                    Salary = e.Salary,
                    RegisterDate = e.RegisterDate.ToString()
                })
                .ToList();
        }
    }
}
