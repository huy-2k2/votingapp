using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.application.Contract
{
    public interface IProxyService
    {
        Task<string> GetIndexFileAsync();
    }
}
