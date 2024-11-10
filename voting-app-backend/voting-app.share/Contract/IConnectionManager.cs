using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;

namespace voting_app.share.Contract

{
    public interface IConnectionManager
    {

        /// <summary>
        /// lấy ra connection
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> GetConnectionAsync();


        /// <summary>
        /// lấy ra transaction
        /// </summary>
        /// <returns></returns>
        Task<IDbTransaction> GetTransactionAsync();

        /// <summary>
        /// giải phóng connection
        /// </summary>
        /// <returns></returns>
        void DisposeConnection();


        /// <summary>
        /// tạo ra 1 phiên làm việc mới với database
        /// </summary>
        void CreateNewSession();
    }
}
