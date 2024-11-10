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

namespace voting_app.api.Middleware
{
    public class AuthContextMiddleware
    {
        private readonly TokenConfig _tokenConfig;
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthContextMiddleware(RequestDelegate next, TokenConfig tokenConfig, IHttpContextAccessor httpContextAccessor)
        {
            this._tokenConfig = tokenConfig;
            this._next = next;
            this._httpContextAccessor = httpContextAccessor;
        }


        public async Task Invoke(HttpContext context)
        {

            // Kiểm tra xem API có attribute AllowAnonymous không
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>() != null)
            {
                // Nếu có [AllowAnonymous], bỏ qua việc giải mã token và tiếp tục middleware pipeline
                await _next(context);
                return;
            }

            // Lấy JWT token từ cookie
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                return;
            }
            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Xử lý token (giải mã, xác thực, v.v.)
            if (!string.IsNullOrEmpty(token))
            {
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

                    try
                    {
                        await _next(context);

                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 999;
                    }


                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
