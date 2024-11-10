using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;

namespace voting_app.infrastructure.MysqlRepository
{
    public class UserRepository : CRUDBaseRepository<UserEntty>, IUserRepository
    {


        public UserRepository(IConnectionManager connectionManager, IContextService contextService, IServiceProvider serviceProvider) : base(connectionManager, contextService, serviceProvider) { 
        
        }
        public async Task<UserEntty> GetByEmailAsync(string email)
        {
            var sql = $"SELECT * FROM {GetTableName()} WHERE {nameof(UserEntty.Email)} = @Email";
            var param = new
            {
                email = email
            };

            var cnn = await connectionManager.GetConnectionAsync();

            var user = await cnn.QueryFirstOrDefaultAsync<UserEntty>(sql, param);
            return user;
        }
    }
}
