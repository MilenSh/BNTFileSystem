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

        Task<T> ReadAsync(K key, bool useNavigationalProperties, bool isReadOnly);

        Task<List<T>> ReadAllAsync(bool useNavigationalProperties, bool isReadOnly);

        Task UpdateAsync(T item, bool isReadOnly);

        Task DeleteAsync(K key);
    }
}
