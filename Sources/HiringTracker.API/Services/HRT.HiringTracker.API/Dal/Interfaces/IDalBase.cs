using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IDalBase<TEntity> 
    {
        IList<TEntity> GetAll();

        TEntity Get(long id);

        long? Upsert(TEntity entity, long? editorID);

        TEntity Upsert(TEntity entity);

        bool Delete(long id);

        IDictionary<long, TEntity> GetAllAsDictionary();
    }
}
