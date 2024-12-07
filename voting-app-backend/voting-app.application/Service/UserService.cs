using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.application.DTO;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;

namespace voting_app.application.Service
{
    public class UserService : CRUDBaseService<UserDto, UserEntty>, IUserService
    {
        public UserService(IUserRepository userRepository, IServiceProvider serviceProvider) : base(userRepository, serviceProvider) { 
        
        }

        protected override async Task processDataBeforeCreate(UserDto dto, UserEntty entity)
        {
            await base.processDataBeforeCreate(dto, entity);
            entity.CreatedBy = dto.UserID;
            entity.ModifiedBy = dto.UserID;
        }
    }
}
