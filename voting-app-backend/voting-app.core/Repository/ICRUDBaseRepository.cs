using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.core.Repository
{
    public interface ICRUDBaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task CreateAsync(TEntity entity);
    }
}
