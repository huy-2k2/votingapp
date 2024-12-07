using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.core.Contract;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Common;
using voting_app.share.Config;
using voting_app.share.Constant;
using voting_app.share.Contract;
using voting_app.share.Enum;
using voting_app.share.Param;

namespace voting_app.application.Service
{
    public class VoteService : CRUDBaseService<VoteDto, VoteEntity>, IVoteService
    {
        private IVoteRepository _voteRepository;

        private IMailSender _mailSender;

        private IEmailRepository _emailRepository;


        private VoteConfig _voteConfig;

        public VoteService(IVoteRepository voteRepository, IServiceProvider serviceProvider) : base(voteRepository, serviceProvider)
        {
            _voteRepository = voteRepository;
            _mailSender = serviceProvider.GetRequiredService<IMailSender>();
            _emailRepository = serviceProvider.GetRequiredService<IEmailRepository>();
            _voteConfig = serviceProvider.GetRequiredService<VoteConfig>();
        }

        public async Task<IEnumerable<VoteDto>> GetAllByUserId()
        {
            var userId = _contextService.GetContextData().UserId;

            var whereParameter = new WhereParameter()
            {
                FilterItems = new List<FilterItem>()
               {
                   new FilterItem()
                   {
                       FieldName= nameof(VoteDto.UserID),
                       FieldValue= userId,
                       Operator = WhereOperator.Equal
                   }
               }
            };

            var entities = await _voteRepository.GetByFilterAsync(whereParameter);

            var result = _mapper.Map<List<VoteDto>>(entities);

            return result;
        }

        public async Task UpdateStatusAsync(int voteStatus, Guid voteID)
        {
            // valdate xem vote id này có phải của người dùng đang đăng nhập không

            var vote = await _baseRepository.GetByIdAsync(voteID);

            var userId = _contextService.GetContextData().UserId;

            if (vote.UserID != userId)
            {
                throw new Exception("NotHavePermission");
            }


            var whereParameter = new WhereParameter()
            {
                FilterItems = new List<FilterItem>()
               {
                   new FilterItem()
                   {
                       FieldName= nameof(VoteDto.VoteID),
                       FieldValue= voteID,
                       Operator = WhereOperator.Equal
                   }
               }
            };

            var updateFields = new List<UpdateField>() {
                new UpdateField()
                {
                    FieldName = nameof(VoteDto.VoteStatus),
                    FieldValue = voteStatus,
                },
            };


            await _voteRepository.UpdateByFilterAsync(updateFields, whereParameter);
        }

        protected override async Task afterCreateAsync(VoteDto dto)
        {
            // gửi email
            if(dto.IsSendQrEmail)
            {
                var contextData = _contextService.GetContextData();

                var emails = await _emailRepository.GetByMasterIdAsync(dto.VoteID, typeof(EmailEntity).GetProperty(nameof(EmailEntity.VoteID)));


                foreach(var email in emails)
                {
                    var body = InviteToVote.InviteToVoteBody;
                    var title = InviteToVote.InviteToVoteTitle;

                    var linkToVote = string.Format(_voteConfig.VoteUrl, dto.VoteID, email.EmailID);

                    body = string.Format(body, contextData.Email, dto.VoteTitle, linkToVote);

                    // không cần await để trả về kết quả luôn cho người dùng
                    _mailSender.SendAsync(email.EmailAddress, title, body);
                }
            }


            await base.afterCreateAsync(dto);
        }
    }
}
