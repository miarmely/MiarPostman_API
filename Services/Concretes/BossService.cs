using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class BossService : IBossService
    {
        private readonly IRepositoryManager _manager;

        public BossService(IRepositoryManager manager) =>
            _manager = manager;


        public void CreateBoss(Boss boss)
        {
            _manager.Boss.CreateBoss(boss);
            _manager.Save();
        }


        public IEnumerable<Boss> GetAllBosses(bool trackChanges)
        {
            var entity = _manager.Boss.GetAllBoss(trackChanges);
            return entity;
        }
    }
}
