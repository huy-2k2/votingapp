using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using voting_app.share.Enum;

namespace voting_app.share.Param
{
    public class WhereClause
    {
        public string FieldName { get; set; }

        public string Operator { get; set; }

        public object FieldValue { get; set; }

        public List<WhereClause> OrWhereClause { get; set; }

        public static string ParseOperator(WhereOperator updateField)
        {
            if (updateField == WhereOperator.Equal)
            {
                return "=";
            }
            else if (updateField == WhereOperator.NotEqual)
            {
                return "!=";
            }
            else if (updateField == WhereOperator.IN)
            {
                return "IN";
            }
            return "=";
        }

        public static string ParseWhereClause(List<WhereClause> whereClauses)
        {
            var listSql = new List<string>();

            return string.Join(" AND ", listSql);
        }

    }
}
