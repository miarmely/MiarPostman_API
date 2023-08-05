namespace Services.Contracts
{
    public interface IServiceManager
    {
        IEmployeeService EmployeeService { get; }
        IRoleService RoleSevice { get; }
        IEmployeeAndRoleService EmployeeAndRoleService { get; }
        IDataConverterService DataConverterService { get; }
        IViewConverterService ViewConverterService { get; }
    }
}   
