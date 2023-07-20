using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDb<T, K>
    {
        Task CreateAsync(T item);

        Task<T> ReadAsync(K key);

        Task<IEnumerable<T>> ReadAllAsync();

        Task UpdateAsync(K key);

        Task DeleteAsync(K key);
    }
}
