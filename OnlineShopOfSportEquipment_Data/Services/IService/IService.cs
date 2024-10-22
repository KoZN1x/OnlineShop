using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Data.Services.IService
{
    public interface IService<T> where T : class
    {
        public Task<T> FindAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            bool isTracking = true);
        public Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            string? inclideProperties = null,
            bool isTracking = true
            );
        public Task AddAsync(T item);
        public void Remove(T item);
        public Task SaveAsync();
    }
}
