using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;


namespace Repositories.Concrete
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public ManagerRepository(RepositoryContext context) : base(context)
        { }

        public void CreateManager(Manager entity) => Create(entity);


        public IQueryable<Manager> GetAllManagers(bool trackChanges) => 
            FindAll(trackChanges)
            .OrderBy(m => m.Id);

        public Manager? GetManagerById(int id, bool trackChanges) => 
            FindWithCondition(m => m.Id == id, trackChanges)
            .FirstOrDefault();

        public List<Manager> GetManagersByFullName(string fullName, bool trackChanges) => 
            FindWithCondition(m => m.FullName.Equals(fullName), trackChanges)
            .ToList();

        public List<Manager> GetManagersByLastName(string lastName, bool trackChanges) =>
            FindWithCondition(m => m.LastName.Equals(lastName), trackChanges)
            .ToList();
        
        public List<Manager> GetManagersByFactory(string factory, bool trackChanges) =>
            FindWithCondition(m => m.Factory.Equals(factory), trackChanges)
            .ToList();

        public void UpdateManager(Manager entity) => Update(entity);

        public void DeleteManager(Manager entity) => Delete(entity);
    }
}
