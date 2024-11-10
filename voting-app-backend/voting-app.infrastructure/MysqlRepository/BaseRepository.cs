using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Entity;
using voting_app.core.Repository;
using voting_app.share.Common;
using voting_app.share.Contract;
using voting_app.share.CustomAttribute;

namespace voting_app.infrastructure.MysqlRepository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected IServiceProvider serviceProvider { get; set; }
        protected IConnectionManager connectionManager { get; set; }

        public BaseRepository(IConnectionManager connectionManager, IServiceProvider serviceProvider)
        {
            this.connectionManager = connectionManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => p.GetCustomAttribute<NotMapAttribute>() is null).ToList();

            var primaryKeyProperty = properties.Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() is not null).First();


            var sql = $"SELECT {string.Join(",", properties.Select(p => p.Name))} FROM {GetTableName()} WHERE {primaryKeyProperty.Name} = @id";

            var cnn = await connectionManager.GetConnectionAsync();

            var result = await cnn.QueryFirstAsync<TEntity>(sql, new Dictionary<string, object>()
            {
                {"id", id }
            });

            return result;
        }
        public string GetTableName(Type entityType = null)
        {
            if (entityType is null)
            {
                entityType = typeof(TEntity);
            }
            var tableAttr = entityType.GetCustomAttribute<TableAttribute>();

            if (tableAttr is null)
            {
                return string.Empty;
            }

            return tableAttr.Name;
        }


        public PropertyInfo GetPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetMasterTableInfo(PropertyInfo foreignKey)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Dictionary<string, string>> GetAllMasterTableInfo()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetEntireByIdAsync(Guid id)
        {

            // gọi để lấy master trước
            var entity = await GetByIdAsync(id);

            await GetEntireDetailByIdAsync(entity);

            return entity;
        }



        public async Task<IEnumerable<TEntity>> GetByMasterIdAsync(Guid masterId, PropertyInfo property)
        {
            var masterAttr = property.GetCustomAttribute<MasterAttribute>();

            var listProperty = typeof(TEntity).GetProperties().Where(p => p.GetCustomAttribute<NotMapAttribute>() is null).ToList();

            var sql = $"SELECT {string.Join(",", listProperty.Select(p => p.Name))} FROM {GetTableName()} WHERE {property.Name} = @id";

            var cnn = await connectionManager.GetConnectionAsync();

            var listResult = await cnn.QueryAsync<TEntity>(sql, new Dictionary<string, object>()
            {
                {"id", masterId}
            });

            return listResult;

        }
        private async Task GetEntireDetailByIdAsync(object entity)
        {
            var listDetailProperty = entity.GetType().GetProperties().Where(p => p.GetCustomAttribute<DetailAttribute>() is not null).ToList();

            var idProperty = entity.GetType().GetProperties().Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() is not null).First();

            foreach (var detailProperty in listDetailProperty)
            {

                var detailAttr = detailProperty.GetCustomAttribute<DetailAttribute>();
                var masterProperty = detailAttr.DetailType.GetProperty(detailAttr.MasterKeyName);

                var detailRepo = serviceProvider.GetService(detailAttr.RepositoryType);

                var method = detailRepo.GetType().GetMethod(nameof(GetByMasterIdInternalAsync));

                await (Task)method.Invoke(detailRepo, new object[] {entity, idProperty.GetValue(entity), detailProperty ,masterProperty});


                var detailEntities =  (IEnumerable)detailProperty.GetValue(entity);

                foreach (var detailSub in detailEntities)
                {
                    await GetEntireDetailByIdAsync(detailSub);
                }
            }
        }


        public async Task GetByMasterIdInternalAsync(object masterEntity, Guid masterId, PropertyInfo detailProperty, PropertyInfo masterProperty)
        {
            var detailEntities = await GetByMasterIdAsync(masterId, masterProperty);

            detailProperty.SetValue(masterEntity, detailEntities);

        }
    }
}
