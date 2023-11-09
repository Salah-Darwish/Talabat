﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Core.Repoisteries.Contract
{
    public interface IgenericReposity<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
     
        Task <IReadOnlyList<T>> GetAllAsync();
        Task <IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T>spec);
        Task<T?> GetWithSpecAsync(ISpecification<T>spec);
    }
}
