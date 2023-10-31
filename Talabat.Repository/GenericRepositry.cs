using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repoisteries.Contract;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepositry<T> : IgenericReposity<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext; 
        public GenericRepositry(StoreContext dbContext) // Ask Clr for Creating Object From dbContext Implicitly  
        {
            _dbContext = dbContext;
        }

        public StoreContext DbContext { get; }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
               
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        public async Task<T?> GetAsync(int id)
        {
            //return await _dbContext.Set<Product>().Where(P=>P.Id==id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T ;
            return await _dbContext.Set<T>().FindAsync(id); 
        }

        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec); 
        }
    }
}
