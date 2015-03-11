using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Suwen.ToFit.DAL;
using Suwen.ToFit.DM;

namespace Suwen.ToFit.BLL
{
    public class BLL_Admin
    {

        DAL_Admin oDAL = new DAL_Admin();


         /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns>登录成功返回记录ID，失败返回0</returns>
        public int Login(string loginName, string password)
        {
            return oDAL.Login(loginName, password);
        }
    }
}
