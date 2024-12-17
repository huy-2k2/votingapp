using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.core.Repository;
using voting_app.share.Config;
using voting_app.share.Contract;

namespace voting_app.application.Service
{
    public class ProxyService : IProxyService
    {
        private readonly ProxyConfig _config;
        private IUserRepository _userRepository;
        private IContextService _contextService;
        private readonly HttpClient _httpClient;
        public ProxyService(IServiceProvider serviceProvider) {
            _config = serviceProvider.GetRequiredService<ProxyConfig>();
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _contextService = serviceProvider.GetRequiredService<IContextService>();
            _httpClient = serviceProvider.GetRequiredService<HttpClient>();
        }

        public async Task<string> GetIndexFileAsync()
        {
            var context = _contextService.GetContextData();
            string version = _config.VersionnKeyDefault;
            var indexUrl = string.Empty;
            var cdnUrl = string.Empty;
            // nếu chưa đăng nhập
            if(context is not null)
            {
                Console.WriteLine("5: ", context.UserId);
                var userId = context.UserId;
                var user = await _userRepository.GetByIdAsync(userId);
                version = user.CurrentVersion;
                Console.WriteLine("6: ", version);
            }

            if (version == _config.VersionnKeyProduction)
            {
                indexUrl = _config.ProductionIndexUrl;
                cdnUrl= _config.ProductionCdnUrl;
            } else
            {
                indexUrl = _config.StagingIndexUrl;
                cdnUrl = _config.StagingCdnUrl;
            }

            var response = await _httpClient.GetAsync(indexUrl);
            var htmlContent = await response.Content.ReadAsStringAsync();

            htmlContent = htmlContent.Replace(_config.OriginKeyCdn, cdnUrl);

            htmlContent = htmlContent
                                .Replace("<head>", $"<head><script>window.cdnSite='{cdnUrl}';</script>");

            Console.WriteLine("7: ", indexUrl);
            Console.WriteLine("8: ", cdnUrl);



            return htmlContent;
        }
    }
}
