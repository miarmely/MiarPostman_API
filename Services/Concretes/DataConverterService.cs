using Entities.DataModels;
using Entities.ViewModels;
using Services.Contracts;


namespace Services.Concretes
{
    public class DataConverterService : IDataConverterService
    {
        public Employee ConvertToEmployee(EmployeeView employeeView)
        {
            return new Employee()
            {
                Id = Convert.ToInt32(employeeView.Id),
                FullName = employeeView.FullName,
                LastName = employeeView.LastName,
                Job = employeeView.Job,
                Salary = Convert.ToDecimal(employeeView.Salary),
                RegisterDate = Convert.ToDateTime(employeeView.RegisterDate)
            };
        }


        public List<Employee> MultiConvertToEmployee(List<EmployeeView> employeeViewList)
        {
            return employeeViewList
                .Select(ew => new Employee()
                {
                    Id = Convert.ToInt32(ew.Id),
                    FullName = ew.FullName,
                    LastName = ew.LastName,
                    Job = ew.Job,
                    Salary = Convert.ToDecimal(ew.Salary),
                    RegisterDate = Convert.ToDateTime(ew.RegisterDate)
                })
                .ToList();
        }
    }
}
