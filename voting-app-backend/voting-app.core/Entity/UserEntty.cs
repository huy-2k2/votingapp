using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Repository;
using voting_app.share.CustomAttribute;

namespace voting_app.core.Entity
{
    [Table("user", typeof(IUserRepository))]
    public class UserEntty : RecordState
    {
        #region Field
        [PrimaryKey]
        public Guid UserID { get; set; }

        public string Email { get; set; }

        public string CurrentVersion { get; set; }
        #endregion

        #region Method


        #endregion




    }
}
