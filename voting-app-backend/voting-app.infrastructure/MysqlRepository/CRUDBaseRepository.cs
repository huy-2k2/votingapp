using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Contract;
using voting_app.share.CustomAttribute;

namespace voting_app.infrastructure.MysqlRepository
{
    public abstract class CRUDBaseRepository<TEntity> : BaseRepository<TEntity>, ICRUDBaseRepository<TEntity>
    {

        protected IContextService _contextService;
        public CRUDBaseRepository(IConnectionManager connectionManager, IContextService contextService, IServiceProvider serviceProvider) : base(connectionManager, serviceProvider)
        {
            this._contextService = contextService;
        }


        /// <summary>
        /// xử lý entity trước khi cất
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task ProcessEntityBeforeInsertAsync(object entity)
        {
            if (entity == null)
            {
                return;
            }

            // nếu có thông tin ngảy, giờ, người tạo thì gán lại
            if (typeof(TEntity).IsSubclassOf(typeof(RecordState)))
            {
                entity.GetType().GetProperty(nameof(RecordState.CreatedDate)).SetValue(entity, DateTime.Now);
                entity.GetType().GetProperty(nameof(RecordState.ModifiedDate)).SetValue(entity, DateTime.Now);


                var context = _contextService.GetContextData();

                if (context is not null)
                {
                    entity.GetType().GetProperty(nameof(RecordState.CreatedBy)).SetValue(entity, context.UserId);
                    entity.GetType().GetProperty(nameof(RecordState.ModifiedBy)).SetValue(entity, context.UserId);
                }
            }

            var primaryProperty = entity.GetType().GetProperties().Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() is not null).First();

            var primaryId = primaryProperty.GetValue(entity);

            if (primaryId is null || primaryId.ToString() == Guid.Empty.ToString())
            {
                var id = Guid.NewGuid();
                primaryProperty.SetValue(entity, id);
            }

        }


        public async Task CreateAsync(TEntity entity)
        {
            var param = new Dictionary<string, object>();

            var listSql = new List<string>();

            GenrerateCreateCommand(entity, param, listSql);

            var sql = string.Join(";", listSql);

            var cnn = await connectionManager.GetConnectionAsync();

            await cnn.ExecuteAsync(sql, param);

        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }


        private async Task GenrerateCreateCommand(object entity, Dictionary<string, object> param = null, List<string> listSql = null)
        {

            var tableAttr = entity.GetType().GetCustomAttribute<TableAttribute>();



            if (tableAttr != null)
            {
                var entityRepoType = tableAttr.RepositoryType;

                var entityRepo = serviceProvider.GetService(entityRepoType);

                var method = entityRepo.GetType().GetMethod(nameof(ProcessEntityBeforeInsertAsync));

                var task = (Task)method.Invoke(entityRepo, new object[] { entity });

                await task;
            }


            var properties = entity.GetType().GetProperties().ToList();

            var insertProperties = properties.Where(p => p.GetCustomAttribute<NotMapAttribute>() is null).OrderBy(p => p.Name).ToList();

            var listKey = new List<string>();

            insertProperties.ForEach(p =>
            {
                var key = $"{p.Name}_{Guid.NewGuid().ToString().Replace('-', '_')}";
                listKey.Add($"@{key}");
                param.Add(key, p.GetValue(entity));
            });


            var sql = $"INSERT INTO {GetTableName(entity.GetType())} ({string.Join(",", insertProperties.Select(p => p.Name).OrderBy(n => n))}) VALUE ({string.Join(",", listKey)})";

            listSql.Add(sql);

            var primaryProperty = insertProperties.Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() is not null).First();

            var primaryId = primaryProperty.GetValue(entity);

            properties.Where(p => p.GetCustomAttribute<DetailAttribute>() is not null).ToList().ForEach(p =>
            {
                var detailEntities = (IEnumerable)p.GetValue(entity);

                foreach (var detailEntity in detailEntities)
                {

                    var masterProperties = detailEntity.GetType().GetProperties().Where(p => p.GetCustomAttribute<MasterAttribute>()?.MasterKeyName == primaryProperty.Name);


                    foreach(var masterProperty in masterProperties)
                    {
                        masterProperty.SetValue(detailEntity, primaryId);
                    }

                    GenrerateCreateCommand(detailEntity, param, listSql);
                }
            });

        }

    }
}
