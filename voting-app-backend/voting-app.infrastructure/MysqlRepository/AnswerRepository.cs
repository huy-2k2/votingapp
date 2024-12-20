﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;

namespace voting_app.infrastructure.MysqlRepository
{
    public class AnswerRepository : CRUDBaseRepository<AnswerEntity>, IAnswerRepository
    {
        public AnswerRepository(IConnectionManager connectionManager, IContextService contextService, IServiceProvider serviceProvider) : base(connectionManager, contextService, serviceProvider)
        {
        }

      
    }
}
