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
    public class DAL_Admin
    {

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns>登录成功返回记录ID，失败返回0</returns>
        public int Login(string loginName, string password)
        {
            string strSQL = "select * from CMS_AdminInfo where loginName='" + loginName + "' and cipher='" + password + "' and state>=0";

            DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

            if (dtInfo.Rows.Count > 0)
            {
                return Convert.ToInt32(dtInfo.Rows[0]["ID"].ToString());
            }
            else
            {
                return 0;
            }
        }
    }
}
