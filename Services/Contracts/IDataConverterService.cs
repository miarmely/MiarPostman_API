using Entities.DataModels;
using Entities.ViewModels;


namespace Services.Contracts
{
    public interface IDataConverterService
    {
        Employee ConvertToEmployee(EmployeeView employee);
        List<Employee> MultiConvertToEmployee(List<EmployeeView> employeeViewList);
    }
}
