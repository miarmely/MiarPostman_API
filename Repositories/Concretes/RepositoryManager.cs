using Microsoft.EntityFrameworkCore.Query.Internal;
using Repositories.Concretes;
using Repositories.Contracts;
using Repositories.EFCore;

namespace Repositories.Concrete
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        
        private readonly Lazy<IEmployeeAndRoleRepository> _employeeAndRoleRepository;
        
        private readonly Lazy<IRoleRepository> _roleRepository;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IEmployeeAndRoleRepository EmployeeAndRole => _employeeAndRoleRepository.Value;
        public IRoleRepository Role => _roleRepository.Value;


        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_context));
            _employeeAndRoleRepository = new Lazy<IEmployeeAndRoleRepository>(() => new EmployeeAndRoleRepository(_context));
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
        }
            

        public void Save()  // Her CRUD işlemi için tek tek "_context.SaveChanges()" yapmamak içindir.
        {
            _context.SaveChanges();
        }
    }
}

// Satır 10: Burdaki işlemin Adı "Lazy Loading" tir. RepositoryManager'dan bir nesne oluşturulduğunda "_employeeRepository" kullanılmamışsa gereksiz yere IEmployeeRepository 'inin new'lenmesini engeller. Aksi Halde RepositoryManager'dan bir nesne yaratıldığında IEmployeeRepository'sini kullanmasakda otomatik olarak new'lenmiş olacak. (bu işlemin adı da "Eager Loading")
// ÖRN: EmployeeController kullanılacağı zamanda; RepositoryManager'daki, IManagerRepository ile varsa diğer repositorylerde gereksiz yere new'lenecek.(Eager Loading) Bu işlemin önüne geçmek için "Lazy Loading" kullanılır.