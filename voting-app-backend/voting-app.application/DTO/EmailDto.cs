using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.application.DTO
{
    public class EmailDto
    {
        public Guid EmailID { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
    }
}
