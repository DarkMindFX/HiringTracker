﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IDalBase<TEntity> : IInitializable
    {
        IList<TEntity> GetAll();

        TEntity Upsert(TEntity entity);

        
    }
}
