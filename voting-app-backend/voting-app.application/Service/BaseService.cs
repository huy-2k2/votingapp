using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Common;
using voting_app.share.Contract;

namespace voting_app.application.Service
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IContextService _contextService;
        protected IConnectionManager _connectionManager;


        public BaseService(IBaseRepository<TEntity> baseRepository, IServiceProvider serviceProvider)
        {
            _baseRepository = baseRepository;
            _mapper = serviceProvider.GetRequiredService<IMapper>(); ;
            _contextService =  serviceProvider.GetRequiredService<IContextService>();
            _connectionManager = serviceProvider.GetRequiredService<IConnectionManager>();

        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            var dto = _mapper.Map<TEntity, TDto>(entity);
            return dto;
        }

        public async Task<TDto> GetEntireByByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetEntireByIdAsync(id);

            var dto = _mapper.Map<TEntity, TDto>(entity);
            return dto;
        }
    }
}
