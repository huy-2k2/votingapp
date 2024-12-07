using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("result", typeof(IResultDetailRepository))]
    public class ResultEntity
    {
        [PrimaryKey]
        public Guid ResultID { get; set; }
        public string? SubmitterName { get; set; } = string.Empty;

        [Master("vote", nameof(VoteEntity.VoteID))]
        public Guid VoteID { get; set; }

        [Master("email", nameof(EmailEntity.EmailID))]
        public Guid? EmailID { get; set; }

        [NotMap]
        [Detail(typeof(IResultDetailRepository), typeof(ResultDetailEntity), nameof(ResultDetailEntity.ResultID))]
        public IEnumerable<ResultDetailEntity> ResultDetails { get; set; }
    }
}
