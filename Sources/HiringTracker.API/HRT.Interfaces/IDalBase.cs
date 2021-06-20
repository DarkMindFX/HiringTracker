﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IDalBase<TEntity> : IInitializable
    {
        IList<TEntity> GetAll();

        TEntity Get(long id);

        TEntity Upsert(TEntity entity);

        bool Delete(long id);
    }
}
