using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;
using voting_app.share.Param;

namespace voting_app.core.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);


        Task<TEntity> GetEntireByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetByMasterIdAsync(Guid masterId, PropertyInfo property);

        Task<IEnumerable<TEntity>> GetByFilterAsync(WhereParameter whereParameter);
    }
}
