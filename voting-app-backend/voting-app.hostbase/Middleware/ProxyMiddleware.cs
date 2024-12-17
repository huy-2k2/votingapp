using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using voting_app.infrastructure;
using voting_app.share.Common;
using voting_app.share.Config;

namespace voting_app.hostbase.Middleware
{
    public class ProxyMiddleware
    {
        private readonly TokenConfig _tokenConfig;
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProxyMiddleware(RequestDelegate next, TokenConfig tokenConfig, IHttpContextAccessor httpContextAccessor)
        {
            this._tokenConfig = tokenConfig;
            this._next = next;
            this._httpContextAccessor = httpContextAccessor;
        }


        public async Task Invoke(HttpContext context)
        {

            var authHeader = string.Empty;

            var tokenCookie = context.Request.Cookies["vta_token"];
            if (tokenCookie != null)
            {
                authHeader = tokenCookie.ToString();
            }

            Console.WriteLine("1: ", authHeader);


            if (string.IsNullOrEmpty(authHeader))
            {
                await _next(context);
                return;
            }
            var token = authHeader.Trim();
            Console.WriteLine("2: ", authHeader);



            // Xử lý token (giải mã, xác thực, v.v.)
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine("3: ", authHeader);

                try
                {
                    // Giải mã token
                    var secretKey = _tokenConfig.SecurityKey;
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                    var tokenHandler = new JwtSecurityTokenHandler();
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateLifetime = true
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;

                    // Lấy thông tin userId và userEmail từ token
                    var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                    var userEmail = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                    context.Items[nameof(ContextData)] = new ContextData()
                    {
                        UserId = Guid.Parse(userId),
                        Email = userEmail,
                    };

                    Console.WriteLine("4: ", userId);
                }
                finally
                {
                    await _next(context);
                }
            } 
            else
            {
                await _next(context);
            }
        }
    }
}
