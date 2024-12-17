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
using voting_app.core.Contract;
using voting_app.infrastructure.Service;
using Microsoft.Extensions.Configuration;

namespace voting_app.hostbase
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


            // add other infra service
            builder.Services.AddScoped<IMailSender, MailkitMailSender>();
        }

        public static void AddApplicationService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IVoteService, VoteService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAnswerService, AnswerService>();
            builder.Services.AddScoped<IResultService, ResultService>();
        }

        public static void AddOtherService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IConnectionManager, ConnectionManager>();

            // Thêm AutoMapper vào DI container
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // thêm context service
            builder.Services.AddScoped<IContextService, ContextService>();


            builder.Services.AddHttpContextAccessor();


            // Cấu hình HttpClient để sử dụng DI
            builder.Services.AddHttpClient();

        }

        public static void AddConfig(this WebApplicationBuilder builder)
        {

            var tokenConfig = new TokenConfig();
            builder.Configuration.GetSection(KeyConfig.TOKEN).Bind(tokenConfig);
            builder.Services.AddSingleton<TokenConfig>(tokenConfig);


            var connectionConfig = new ConnectionConfig();
            builder.Configuration.GetSection(KeyConfig.CONNECTION).Bind(connectionConfig);
            builder.Services.AddSingleton<ConnectionConfig>(connectionConfig);


            var emailServiceConfig = new EmailServiceConfig();
            builder.Configuration.GetSection(KeyConfig.EMAIL_SERVICE).Bind(emailServiceConfig);
            builder.Services.AddSingleton<EmailServiceConfig>(emailServiceConfig);



            var voteConfig = new VoteConfig();
            builder.Configuration.GetSection(KeyConfig.VOTE).Bind(voteConfig);
            builder.Services.AddSingleton<VoteConfig>(voteConfig);
        }


        public static void AddProxyConfig(this WebApplicationBuilder builder)
        {

            var tokenConfig = new ProxyConfig();
            builder.Configuration.GetSection(KeyConfig.PROXY).Bind(tokenConfig);
            builder.Services.AddSingleton<ProxyConfig>(tokenConfig);
        }

        public static void AddProxyService(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IProxyService, ProxyService>();
        }
    }
}
