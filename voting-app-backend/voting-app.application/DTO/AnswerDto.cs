using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.CustomAttribute;

namespace voting_app.application.DTO
{
    public class AnswerDto
    {
        public Guid AnswerID { get; set; }
        public Guid QuestionID { get; set; }
        public string AnswerContent { get; set; } = string.Empty;

        public IEnumerable<ResultDetailDto> ResultDetails { get; set; }
    }
}
