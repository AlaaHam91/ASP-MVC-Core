using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
  public  interface IBookRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity find(int id);
        void Add(TEntity entity);
        void Update(TEntity entity,int id);
       void Delete(int id);
        List<TEntity> Search(string term);
    }
}
