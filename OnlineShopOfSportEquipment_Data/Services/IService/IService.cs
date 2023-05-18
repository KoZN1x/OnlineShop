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
        public T Find(Guid id);
        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            bool isTracking = true);
        public T FirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? inclideProperties = null,
            bool isTracking = true
            );
        public void Add(T item);
        public void Remove(T item);
        public void Save();
    }
}
