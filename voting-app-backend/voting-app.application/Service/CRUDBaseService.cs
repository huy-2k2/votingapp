using AutoMapper;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.application.Contract;
using voting_app.core.Repository;
using voting_app.share.Contract;
using voting_app.share.CustomAttribute;

namespace voting_app.application.Service
{
    public class CRUDBaseService<TDto, TEntity> : BaseService<TDto, TEntity>, ICRUDBaseService<TDto, TEntity>
    {

        private ICRUDBaseRepository<TEntity> _repository;

        protected IConnectionManager _connectionManager;

        public CRUDBaseService(ICRUDBaseRepository<TEntity> repository, IMapper mapper, IConnectionManager connectionManager) : base(repository, mapper)
        {
            _repository = repository;
            _connectionManager = connectionManager;
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entityCreate = _mapper.Map<TDto, TEntity>(dto);

            await processDataBeforeCreate(dto, entityCreate);

            await beforeCreateAsync(dto, entityCreate);

            await _repository.CreateAsync(entityCreate);

            var idProperty = entityCreate.GetType().GetProperties().Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null).First();
            var id = (Guid)idProperty.GetValue(entityCreate);

            _connectionManager.CreateNewSession();

            var entityResut = await _repository.GetByIdAsync(id);

            var dtoResult = _mapper.Map<TEntity, TDto>(entityResut);

            await afterCreateAsync(dtoResult);

            return dtoResult;

        }

        public Task DeleteAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }


        protected virtual async Task processDataBeforeCreate(TDto dto, TEntity entity)
        {
            // gán khóa chính
            var primaryKeyInfo = entity.GetType().GetProperties().Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null).First();

            var primaryKeyInfoDto = dto.GetType().GetProperty(primaryKeyInfo.Name);

            var primaryId = primaryKeyInfoDto.GetValue(dto);

            if (primaryId is null || primaryId.ToString() == Guid.Empty.ToString())
            {
                var id = Guid.NewGuid();
                primaryKeyInfoDto.SetValue(dto, id);
                primaryKeyInfo.SetValue(entity, id);
            }


            return;
        }

        protected virtual async Task beforeCreateAsync(TDto dto, TEntity entity)
        {
            return;
        }

        protected virtual async Task afterCreateAsync(TDto dto)
        {
            return;
        }

    }
}
