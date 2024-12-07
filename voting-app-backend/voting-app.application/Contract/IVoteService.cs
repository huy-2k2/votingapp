using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.DTO;
using voting_app.core.Entity;

namespace voting_app.application.Contract
{
    public interface IVoteService : ICRUDBaseService<VoteDto, VoteEntity>
    {

        Task UpdateStatusAsync(int voteStatus, Guid voteID);


        Task<IEnumerable<VoteDto>>  GetAllByUserId();
    }
}
