using Entities.Models;


namespace Repositories.Contracts
{
    public interface IManagerRepository: IRepositoryBase<Manager>  
    {
        public void CreateManager(Manager entity);
        public IQueryable<Manager> GetAllManagers(bool trackChanges);
        public Manager? GetManagerById(int id, bool trackChanges);
        public List<Manager> GetManagersByFullName(string fullName, bool trackChanges);
        public List<Manager> GetManagersByLastName(string lastName, bool trackChanges);
        public List<Manager> GetManagersByFactory(string factory, bool trackChanges);
        public void UpdateManager(Manager entity);
        public void DeleteManager(Manager entity);
    }
}