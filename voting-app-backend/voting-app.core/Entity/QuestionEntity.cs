using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("question", typeof(IQuestionRepository))]
    public class QuestionEntity : RecordState
    {
        [PrimaryKey]
        public Guid QuestionID { get; set; }
        public string QuestionTitle { get; set; } = string.Empty;
        public bool IsMultiAnswer { get; set; }
        public bool IsAnswerAddable { get; set; }

        [Master("vote", nameof(VoteEntity.VoteID))]

        public Guid VoteID { get; set; }

        [Master("user", nameof(UserEntty.UserID))]

        public Guid UserID { get; set; }

        [NotMap]
        [Detail(typeof(IAnswerRepository), typeof(AnswerEntity), nameof(AnswerEntity.QuestionID))]
        public IEnumerable<AnswerEntity> Answers { get; set; }
    }
}
