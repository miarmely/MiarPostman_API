using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concretes
{
    public class ManagerService : IManagerService
    {
        private readonly IRepositoryManager _manager;


        public ManagerService(IRepositoryManager manager)
        {
            _manager = manager;
        }


        public Manager CreateManager(Manager manager)
        {
            // when employee parameter entered as null
            if (manager is null)
                throw new Exception("Null Argument");

            // create
            _manager.Manager.CreateManager(manager);
            _manager.Save();

            return manager;
        }


        public void DeleteOneManager(int id, bool trackChanges)
        {
            var entity = _manager.Manager.GetManagerById(id, false);

            // when id not matched
            if (entity is null)
                throw new Exception($"Id Not Found");

            // delete
            _manager.Manager.DeleteManager(entity);
            _manager.Save();
        }


        public IEnumerable<Manager> GetAllManagers(bool trackChanges)
        {
            // when database is empty
            if (_manager.Manager.Count == 0)
                throw new Exception("Empty Database");

            return _manager.Manager.GetAllManagers(trackChanges);
        }


        public Manager GetOneManagerById(int id, bool trackChanges)
        {
            var entity = _manager.Manager.GetManagerById(id, trackChanges);

            // when id not matched
            if (entity is null)
                throw new Exception("Id Not Found");

            return entity;
        }


        public void UpdateOneManager(int id, Manager manager, bool trackChanges)
        {
            var entity = _manager.Manager.GetManagerById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Id Not Found");

            // update
            _manager.Manager.UpdateManager(manager);
            _manager.Save();
        }


        public Manager PartiallyUpdateOneManager(int id, JsonPatchDocument<Manager> managerPatch, bool trackChanges)
        {
            var entity = _manager.Manager.GetManagerById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Id Not Found");

            managerPatch.ApplyTo(entity);
            _manager.Save();

            return entity;
        }

        void IManagerService.CreateManager(Manager manager)
        {
            throw new NotImplementedException();
        }
    }
}
