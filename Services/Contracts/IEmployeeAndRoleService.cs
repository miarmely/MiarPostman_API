using Entities.DataModels;
using Entities.ViewModels;

namespace Services.Contracts
{
    public interface IEmployeeAndRoleService
    {
        void CreateEmpAndRole(EmployeeView employeeView);
        List<EmployeeView> FillRoles(List<EmployeeView> employeeViews);
        EmployeeView FillRole(EmployeeView employeeView);
        void UpdateRelations(EmployeeView employeeView);
        void DeleteEmpAndRolesByEmployeeId(int employeeId);
        void MultiDeleteEmpAndRoles(List<Employee> employees);
    }
}
