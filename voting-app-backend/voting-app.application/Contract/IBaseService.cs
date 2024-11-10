using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;

namespace voting_app.application.Contract
{
    public interface IBaseService<TDto, TEntity>
    {
        Task<TDto> GetByIdAsync(Guid id);

        Task<TDto> GetEntireByByIdAsync(Guid id);
    }
}
