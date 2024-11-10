using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;
using voting_app.share.Config;
using voting_app.share.Contract;

namespace voting_app.share.Service
{
    public class ConnectionManager : IConnectionManager
    {
        private string connectionString { get; set; }
        private IDbConnection connection { get; set; }

        private IDbTransaction transaction { get; set; }

        public ConnectionManager(ConnectionConfig connectionConfig)
        {
            this.connectionString = connectionConfig.MysqlConnectionString;
        }

        public void DisposeConnection()
        {

            if (this.connection == null) return;
            this.connection.Close();
            this.connection.Dispose();
            this.connection = null;
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            if (this.connection == null)
            {
                var cnn = new MySqlConnection(this.connectionString);
                await cnn.OpenAsync();
                this.connection = cnn;
            }
            return connection;
        }

        public async Task<IDbTransaction> GetTransactionAsync()
        {
            var connection = await GetConnectionAsync();

            if (this.transaction == null)
            {
                var transaction = connection.BeginTransaction();
                this.transaction = transaction;

            }
            return this.transaction;
        }

        public void CreateNewSession()
        {

            if (this.connection == null) return;
            this.connection.Close();
            this.connection.Dispose();
            this.connection = null;
        }
    }
}
