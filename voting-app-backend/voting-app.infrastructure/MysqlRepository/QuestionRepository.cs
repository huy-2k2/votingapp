using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;

namespace voting_app.infrastructure.MysqlRepository
{
    public class QuestionRepository : CRUDBaseRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(IConnectionManager connectionManager, IContextService contextService, IServiceProvider serviceProvider) : base(connectionManager, contextService, serviceProvider)
        {
        }

        public override async Task  ProcessEntityBeforeInsertAsync(object entity)
        {
            var userId = _contextService.GetContextData().UserId;

            entity.GetType().GetProperty(nameof(QuestionEntity.UserID)).SetValue(entity, userId);

            await base.ProcessEntityBeforeInsertAsync(entity);

        }
    }
}
