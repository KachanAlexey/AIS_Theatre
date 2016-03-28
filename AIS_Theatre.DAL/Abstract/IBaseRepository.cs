using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        TKey Insert(TEntity entity);
        bool Update(TEntity entity);
        
        TEntity GetById(TKey id);
        bool Delete(TKey id);

        List<TEntity> GetAll();
    }
}
