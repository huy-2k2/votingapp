using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("answer", typeof(IAnswerRepository))]
    public class AnswerEntity
    {
        [PrimaryKey]
        public Guid AnswerID { get; set; }

        [Master("question", nameof(QuestionEntity.QuestionID))]
        public Guid QuestionID { get; set; }
        public string AnswerContent { get; set; } = string.Empty;

        public bool IsCustom { get; set; }

        [NotMap]
        [Detail(typeof(IResultDetailRepository), typeof(ResultDetailEntity), nameof(ResultDetailEntity.AnswerID))]
        public IEnumerable<ResultDetailEntity> ResultDetails { get; set; }
    }
}
