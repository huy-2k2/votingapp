using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.CustomAttribute;

namespace voting_app.application.DTO
{
    public class ResultDto
    {
        public Guid ResultID { get; set; }
        public string SubmitterName { get; set; } = string.Empty;

        public IEnumerable<ResultDetailDto> ResultDetails { get; set; }
    }
}
