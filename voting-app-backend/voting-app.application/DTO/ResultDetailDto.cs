using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.CustomAttribute;

namespace voting_app.application.DTO
{

    public class ResultDetailDto
    {
        public Guid ResultDetailID { get; set; }
        public Guid ResultID { get; set; }
        public Guid AnswerID { get; set; }
    }
}
