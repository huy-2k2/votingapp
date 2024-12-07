using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.core.Contract
{
    public interface IMailSender
    {
        Task SendAsync(string receiverEmail, string subject, string body);
    }
}
