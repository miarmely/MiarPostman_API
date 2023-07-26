using Entities.Models;
using Repositories.Concrete;
using Repositories.Contracts;
using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class BossRepository : RepositoryBase<Boss>, IBossRepository
    {
        public BossRepository(RepositoryContext context) : base(context)
        {}


        public void CreateBoss(Boss boss) =>
            base.Create(boss);


        public IQueryable<Boss> GetAllBoss(bool trackChanges) =>
            base.FindAll(trackChanges)
            .OrderBy(b => b.Id);
    }
}
