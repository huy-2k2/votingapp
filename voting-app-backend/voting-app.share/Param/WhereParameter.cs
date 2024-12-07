using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Common;

namespace voting_app.share.Param
{
    public class WhereParameter
    {
        public List<FilterItem> FilterItems { get; set; }


        public (Dictionary<string, object>, string) ToMySql()
        {
            var param = new Dictionary<string, object>();

            var listQuery = new List<string>();

            foreach (FilterItem item in FilterItems)
            {
                var data = item.ToMySql();

                foreach(var childParam in data.Item1)
                {
                    param.Add(childParam.Key, childParam.Value);
                }

                listQuery.Add($"( {data.Item2} )");
            }


            var query = $"( {string.Join("AND", listQuery)} )";

            return (Param: param, Query: query);
        }
    }
}
