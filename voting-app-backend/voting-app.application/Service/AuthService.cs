using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using Google.Apis.Auth;
using voting_app.application.DTO;
using voting_app.core.Repository;
using voting_app.share.Config;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using voting_app.core.Entity;

namespace voting_app.application.Service
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private TokenConfig _tokenConfig;
        private IUserService _userService;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, TokenConfig tokenConfig, IUserService userService, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._tokenConfig = tokenConfig;
            this._userService = userService;
            this._mapper = mapper;
        }


        public async Task<string> LoginWithGoogle(string googleToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken);


                var userData = await _userRepository.GetByEmailAsync(payload.Email);


                // nếu đăng nhập lần đầu thì add vào db
                if (userData is null)
                {
                    var userDto = new UserDto
                    {
                        Email = payload.Email,
                    };

                    var userResult =  await _userService.CreateAsync(userDto);
                    userData = _mapper.Map<UserDto, UserEntty>(userResult);
                }

                var userToken = new UserDto()
                {
                    Email = payload.Email,
                    UserID = userData.UserID,
                };
                var tokenResult = createToken(userToken);

                return tokenResult;

            }
            catch (InvalidJwtException invalidTokenEx)
            {
                throw new UnauthorizedAccessException();
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {

            }

        }


        private string createToken(UserDto user)
        {
            var secretKey = _tokenConfig.SecurityKey;
            var expiredTime = _tokenConfig.ExpirationMinutes;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),       // UserId
                new Claim(JwtRegisteredClaimNames.Email, user.Email),  // UserEmail
            };


            var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddHours(expiredTime),
              signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
