using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.core.Entity;
using voting_app.core.Repository;

namespace voting_app.application.Service
{
    public class ResultService : CRUDBaseService<ResultDto, ResultEntity>, IResultService
    {

        private readonly IVoteRepository _voteRepository;

        public ResultService(IResultRepository repository, IServiceProvider serviceProvider) : base(repository, serviceProvider)
        {
            _voteRepository = serviceProvider.GetRequiredService<IVoteRepository>();

        }

        protected override async Task beforeCreateAsync(ResultDto dto, ResultEntity entity)
        {
            var voteId = dto.VoteID;

            var vote = await _voteRepository.GetEntireByIdAsync(voteId);

            if (vote.IsSendQrEmail)
            {
                var isEmailSubmitted = vote.Results.Where(r => r.EmailID == dto.EmailID).Count() > 0;

                if (isEmailSubmitted)
                {

                    throw new Exception("EmailSubmitted");
                }
            }
            else
            {
                if (vote.Results?.Count() >= vote.TotalJoiner)
                {
                    throw new Exception("LimitedTotalJoin");
                }
            }

        }
    }
}
