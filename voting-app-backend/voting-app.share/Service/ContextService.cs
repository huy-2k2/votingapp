using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using voting_app.share.Common;
using voting_app.share.Contract;
namespace voting_app.share.Service
{
    public class ContextService : IContextService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ContextData _contextData = null;
        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor= httpContextAccessor;
        }

        public ContextData GetContextData()
        {
            if(_contextData is null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var context = httpContext.Items[nameof(ContextData)];
                if(context is not null)
                {
                    var contextData = JsonSerializer.Deserialize<ContextData>(JsonSerializer.Serialize(context));
                    this._contextData = contextData;
                }

            }

            return _contextData;
        }
    }
}
