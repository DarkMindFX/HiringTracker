using HRT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    public class DalBaseImpl<TEntity, TDal> : IDalBase<TEntity> where TDal : HRT.Interfaces.IDalBase<TEntity>
    {
        protected TDal _dalImpl;

        protected DalBaseImpl(TDal dalImpl)
        {
            _dalImpl = dalImpl;
        }

        public bool Delete(long id)
        {
            return _dalImpl.Delete(id);
        }

        public TEntity Get(long id)
        {
            return _dalImpl.Get(id);
        }

        public IList<TEntity> GetAll()
        {
            return _dalImpl.GetAll();
        }

        public TEntity Upsert(TEntity entity)
        {
            return _dalImpl.Upsert(entity);
        }

        public IDictionary<long, TEntity> GetAllAsDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
