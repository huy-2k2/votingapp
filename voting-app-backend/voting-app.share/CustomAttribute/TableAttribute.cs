using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.CustomAttribute
{
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }

        public Type RepositoryType { get; set; }

        public TableAttribute(string name, Type repositoryType) : base()
        {
            Name = name;
            RepositoryType = repositoryType;
        }
    }
}
