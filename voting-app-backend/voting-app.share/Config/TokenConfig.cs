using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.Config
{
    public class TokenConfig
    {
        public string SecurityKey { get; set; }

        public int ExpirationMinutes { get; set; }
    }
}
