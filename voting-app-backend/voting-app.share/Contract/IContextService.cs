using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;

namespace voting_app.share.Contract
{
    public interface IContextService
    {
        /// <summary>
        /// Lấy ra thông tin của người dùng hiện tại
        /// </summary>
        /// <returns></returns>
        ContextData GetContextData();
    }
}
