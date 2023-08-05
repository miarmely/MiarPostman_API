using Entities.DataModels;
using Entities.ViewModels;


namespace Services.Contracts
{
    public interface IViewConverterService
    {
        EmployeeView ConvertToEmployeeView(Employee employee);
        List<EmployeeView> MultipleConvertToEmployeeView(List<Employee> employeeList);
     }
}
