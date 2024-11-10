using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;

namespace voting_app.application.Service
{
    public class QuestionService : CRUDBaseService<QuestionDto, QuestionEntity>, IQuestionService
    {
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IConnectionManager connectionManager) : base(questionRepository, mapper, connectionManager)
        {

        }
    }
}
