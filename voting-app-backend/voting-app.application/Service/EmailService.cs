using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Common;
using voting_app.share.Enum;
using voting_app.share.Param;

namespace voting_app.application.Service
{
    public class EmailService : CRUDBaseService<EmailDto, EmailEntity>, IEmailService
    {
        public EmailService(IEmailRepository repository, IServiceProvider serviceProvider) : base(repository, serviceProvider)
        {
        }

        public async Task<IEnumerable<string>> GetByUserAsync()
        {
            var userId = _contextService.GetContextData().UserId;

            var whereParameter = new WhereParameter()
            {
                FilterItems = new List<FilterItem>() {
                    new FilterItem()
                    {
                        FieldName = nameof(EmailDto.UserID),
                        FieldValue = userId,
                        Operator = WhereOperator.Equal,
                    }
                }
            };

            var listEntity = await _baseRepository.GetByFilterAsync(whereParameter);

            var listDto = _mapper.Map<List<EmailDto>>(listEntity);

            var emailGrouped = listDto.GroupBy(x => x.EmailAddress.Trim()).ToList();

            var emailAddresses = emailGrouped.Select(x => x.Key);

            return emailAddresses;
        }
    }
}
