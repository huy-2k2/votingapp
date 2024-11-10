using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.application.Contract
{
    public interface ICRUDBaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
    {
        Task UpdateAsync(TDto dto);

        Task DeleteAsync(TDto dto);

        Task<TDto> CreateAsync(TDto dto);
    }
}
