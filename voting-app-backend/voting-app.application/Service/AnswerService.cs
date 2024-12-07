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
    public class AnswerService : CRUDBaseService<AnswerDto, AnswerEntity>, IAnswerService
    {
        public AnswerService(IAnswerRepository repository, IServiceProvider serviceProvider) : base(repository, serviceProvider)
        {
        }

        public async Task CreateMultiAsync(List<AnswerDto> answers)
        {

            foreach(var answer in answers)
            {
                await base.CreateAsync(answer);
            }
        }
    
    }
}
