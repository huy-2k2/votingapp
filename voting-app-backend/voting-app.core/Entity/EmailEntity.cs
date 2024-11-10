using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("email", typeof(IEmailRepository))]
    public class EmailEntity : RecordState
    {
        [PrimaryKey]
        public Guid EmailID { get; set; }
        public string EmailAddress { get; set; } = string.Empty;

        [Master("vote", nameof(VoteEntity.VoteID))]
        public Guid VoteID { get; set; }

        [Master("user", nameof(UserEntty.UserID))]
        public Guid UserID { get; set; }
    }
}
