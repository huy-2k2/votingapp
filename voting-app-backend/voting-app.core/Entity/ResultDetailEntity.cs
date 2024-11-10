using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{

    [Table("resultdetail", typeof(IResultDetailRepository))]
    public class ResultDetailEntity
    {
        [PrimaryKey]
        public Guid ResultDetailID { get; set; }

        [Master("user", nameof(ResultEntity.ResultID))]

        public Guid ResultID { get; set; }
        public Guid AnswerID { get; set; }
    }
}
