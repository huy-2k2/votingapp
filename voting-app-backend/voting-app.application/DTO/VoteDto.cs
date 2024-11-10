using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.CustomAttribute;

namespace voting_app.application.DTO
{
    public class VoteDto
    {
        public Guid VoteID { get; set; }
        public string VoteTitle { get; set; } = string.Empty;
        public bool IsSendQrEmail { get; set; }
        public bool IsRequireName { get; set; }
        public int? TotalJoiner { get; set; }
        public Guid UserID { get; set; }
        public int VoteStatus { get; set; } = 1;

        public DateTime CreatedDate { get; set; }

        public int VoteTime { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }

        public IEnumerable<EmailDto> Emails { get; set; }

     
        public IEnumerable<ResultDto> Results { get; set; }
    }
}
