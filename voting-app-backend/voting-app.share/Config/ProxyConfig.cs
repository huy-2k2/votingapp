using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.Config
{
    public class ProxyConfig
    {
        public string ProductionCdnUrl { get; set; }

        public string StagingCdnUrl { get; set; }

        public string OriginKeyCdn {get;set;}

        public string ProductionIndexUrl { get; set; }

        public string StagingIndexUrl { get; set; }

        public string VersionnKeyProduction { get; set; }

        public string VersionnKeyStaging { get; set; }

        public string VersionnKeyDefault { get; set; }

    }
}
