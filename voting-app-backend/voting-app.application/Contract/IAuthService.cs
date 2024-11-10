using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.application.Contract
{
    public interface IAuthService
    {
        /// <summary>
        /// đăng nhập với google
        /// </summary>
        /// <param name="googleToken">token của google</param>
        /// <returns>token để xác thực ứng dụng</returns>
        Task<string> LoginWithGoogle(string googleToken);
    }
}
