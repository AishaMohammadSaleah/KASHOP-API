using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public interface IGenericRepository<T>
    {
        Task   <List<T>> GetAllAsync<T>() where T : class;
        Task<T> CreateAsync<T>(T entity) where T : class;
    }
}
