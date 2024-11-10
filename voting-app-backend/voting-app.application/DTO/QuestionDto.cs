using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.CustomAttribute;

namespace voting_app.application.DTO
{
    public class QuestionDto
    {
        public Guid QuestionID { get; set; }
        public string QuestionTitle { get; set; } = string.Empty;
        public bool IsMultiAnswer { get; set; }
        public bool IsAnswerAddable { get; set; }
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
