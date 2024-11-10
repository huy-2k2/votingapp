using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.core.Entity
{
    public class RecordState
    {
        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set;}
    }
}
