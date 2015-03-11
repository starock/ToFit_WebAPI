using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;



using Suwen.ToFit.DBConn;
using Suwen.ToFit.DM;


namespace Suwen.ToFit.DAL
{
    public class DAL_Foods
    {
        public DAL_Foods()
        {
        }


        /// <summary>
        /// 获取食品名称列表(仅包含ID和名字两个字段)
        /// </summary>
        /// <returns>The name list.</returns>
        public DataTable GetNameList()
        {
            string strSQL = "select ID, name from Foods_Info order by name";
            return SQLHelper.ExecuteDt(strSQL);
        }

    }
}

