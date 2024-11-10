using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voting_app.share.Common
{
    public class FilterItem
    {
        /// <summary>
        /// Field cần kiểm tra
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Biểu thức kiểm tra
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// giá trị kiểm tra
        /// </summary>
        public object Value { get; set; }


        /// <summary>
        /// Điều kiện hoạc
        /// </summary>
        public FilterItem OrFilterItem { get; set; }

        /// <summary>
        /// Hàm này kiểm tra xem giá trị của Operator có là 1 trong các giá trị của Constant FilterOperator hay không
        /// </summary>
        public bool IsValidOperator()
        {
            return true;
        }


        /// <summary>
        /// Chuyền từ Filter về string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// chuyển từ string về filter item
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static FilterItem GetFilterItem(string filter)
        {
            throw new NotImplementedException();
        }

    }
}
