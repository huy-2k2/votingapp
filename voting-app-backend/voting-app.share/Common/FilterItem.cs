using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using voting_app.share.Enum;

namespace voting_app.share.Common
{
    public class FilterItem
    {
        /// <summary>
        /// Field cần kiểm tra
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Biểu thức kiểm tra
        /// </summary>
        public WhereOperator Operator { get; set; }

        /// <summary>
        /// giá trị kiểm tra
        /// </summary>
        public object FieldValue { get; set; }


        /// <summary>
        /// Điều kiện hoạc
        /// </summary>
        public List<FilterItem> OrFilterItems { get; set; }


        private string GetOperatorString()
        {
            switch (Operator)
            {
                case WhereOperator.Equal:
                    return "=";
                case WhereOperator.NotEqual:
                    return "<>";
                case WhereOperator.IN:
                    return "IN";
                case WhereOperator.Null:
                    return "IS NULL";
                case WhereOperator.NotNull:
                    return "IS NOT NULL";
                default:
                    throw new Exception("NotValidOperator");
            }
        }


        private (Dictionary<string, object>, string) SelfToMySql()
        {
            var query = $"( {FieldName} {GetOperatorString()}";

            var param = new Dictionary<string, object>();

            if (FieldValue is not null)
            {
                var uuid = Guid.NewGuid();

                var uuidString = uuid.ToString().Replace("-", "_");

                query += $" @{uuidString}";

                param.Add(uuidString, FieldValue);
            }

            query += " )";


            return (Param: param, Query: query);
        }


        /// <summary>
        /// Chuyền từ Filter về string
        /// </summary>
        /// <returns></returns>
        public (Dictionary<string, object>, string) ToMySql()
        {
            var (Param, Query) = SelfToMySql();

            if (OrFilterItems is null || OrFilterItems.Count == 0)
            {
                return (Param, Query);
            }

            var listOrQuery = new List<string>();

            foreach (var orfilterItem in OrFilterItems)
            {
                var orData = orfilterItem.ToMySql();

                listOrQuery.Add($"( {orData.Item2} )");

                foreach (var orParam in orData.Item1)
                {
                   Param.Add(orParam.Key, orParam.Value);
                }
            }


            Query = $"( {Query} OR ({string.Join("AND", listOrQuery)}))";

            return (Param, Query);
        }
    }
}
