using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("vote", typeof(IVoteRepository))]

    public class VoteEntity : RecordState
    {
        [PrimaryKey]
        public Guid VoteID { get; set; }
        public string VoteTitle { get; set; } = string.Empty;
        public bool IsSendQrEmail { get; set; }
        public bool IsRequireName { get; set; }
        public int? TotalJoiner { get; set; }
        public int VoteTime { get; set; }

        public int VoteStatus { get; set; } = 1;

        [Master("user", nameof(UserEntty.UserID))]
        public Guid UserID { get; set; }

        [NotMap]
        [Detail(typeof(IQuestionRepository), typeof(QuestionEntity), nameof(QuestionEntity.VoteID))]
        public IEnumerable<QuestionEntity> Questions { get; set; }

        [NotMap]
        [Detail(typeof(IEmailRepository), typeof(EmailEntity), nameof(EmailEntity.VoteID))]
        public IEnumerable<EmailEntity> Emails { get; set; }

        [NotMap]
        [Detail(typeof(IResultRepository), typeof(ResultEntity), nameof(ResultEntity.VoteID))]
        public IEnumerable<ResultEntity> Results { get; set; }
    }
}
