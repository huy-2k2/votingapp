using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Param;

namespace voting_app.core.Repository
{
    public interface ICRUDBaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task CreateAsync(TEntity entity);

        Task UpdateByFilterAsync(List<UpdateField> updateFields, WhereParameter whereParameter);
    }
}
