using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.CustomAttribute
{
    public class DetailAttribute : Attribute
    {
        public Type RepositoryType { get; set; }

        public Type DetailType { get; set; }

        public string MasterKeyName { get; set; }


        public DetailAttribute(Type repositorytype, Type entityType, string masterKeyName)
        {
            RepositoryType = repositorytype;
            MasterKeyName = masterKeyName;
            DetailType = entityType;
        }
    }
}
