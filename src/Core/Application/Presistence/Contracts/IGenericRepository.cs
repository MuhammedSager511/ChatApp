using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presistence.Contracts
{
    public interface IGenericRepository< T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetAsync(int entity );
        Task DeleteAsync(int entity);
        Task AddAsync(T entity);
        Task UpdateAsync( T entity );
        

    }
}
