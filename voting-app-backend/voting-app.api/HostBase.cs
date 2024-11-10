using Microsoft.Extensions.DependencyInjection;
using voting_app.application.Contract;
using voting_app.application.Service;
using voting_app.core.Repository;
using voting_app.infrastructure;
using voting_app.infrastructure.MysqlRepository;
using voting_app.share.Config;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using voting_app.application;
using voting_app.share.Constant;
using voting_app.share.Contract;
using voting_app.share.Service;

namespace voting_app.api
{
    public static class HostBase
    {
        public static void AddInfrastructureRepo(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

            builder.Services.AddScoped<IVoteRepository, VoteRepository>();
            builder.Services.AddScoped<IResultDetailRepository, ResultDetailRepository>();
            builder.Services.AddScoped<IResultRepository, ResultRepository>();
            builder.Services.AddScoped<IEmailRepository, EmailRepository>();
            builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();


        }

        public static void AddApplicationService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IVoteService, VoteService>();
        }

        public static void AddOtherService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IConnectionManager, ConnectionManager>();

            // Thêm AutoMapper vào DI container
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // thêm context service
            builder.Services.AddScoped<IContextService, ContextService>();


            builder.Services.AddHttpContextAccessor();

        }

        public static void AddConfig(this WebApplicationBuilder builder)
        {

            var tokenConfig = new TokenConfig();
            builder.Configuration.GetSection(KeyConfig.TOKEN).Bind(tokenConfig);
            builder.Services.AddSingleton<TokenConfig>(tokenConfig);


            var connectionConfig = new ConnectionConfig();
            builder.Configuration.GetSection(KeyConfig.CONNECTION).Bind(connectionConfig);
            builder.Services.AddSingleton<ConnectionConfig>(connectionConfig);


        }
    }
}
