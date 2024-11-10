using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.DTO;
using voting_app.core.Entity;

namespace voting_app.application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserEntty>();
            CreateMap<UserEntty, UserDto>();

            CreateMap<VoteEntity, VoteDto>();
            CreateMap<VoteDto, VoteEntity>();

            CreateMap<EmailEntity, EmailDto>();
            CreateMap<EmailDto, EmailEntity>();

            CreateMap<AnswerEntity, AnswerDto>();
            CreateMap<AnswerDto, AnswerEntity>();

            CreateMap<ResultEntity, ResultDto>();
            CreateMap<ResultDto, ResultEntity>();


            CreateMap<ResultDetailEntity, ResultDetailDto>();
            CreateMap<ResultDetailDto, ResultDetailEntity>();

            CreateMap<QuestionEntity, QuestionDto>();
            CreateMap<QuestionDto, QuestionEntity>();
        }
    }
}
