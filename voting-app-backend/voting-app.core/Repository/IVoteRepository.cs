using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;

namespace voting_app.core.Repository
{
    public interface IVoteRepository : ICRUDBaseRepository<VoteEntity>
    {
    }
}
