using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace Services.Contracts
{
    public interface IManagerService
    {
        public void CreateManager(Manager manager);

        public IEnumerable<Manager> GetAllManagers(bool trackChanges);

        public Manager GetOneManagerById(int id, bool trackChanges);

        public void UpdateOneManager(int id, Manager manager, bool trackChanges);

        public Manager PartiallyUpdateOneManager(int id, JsonPatchDocument<Manager> managerPatch, bool trackChanges);

        public void DeleteOneManager(int id, bool trackChanges);
    }
}
