using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Suwen.ToFit.DAL;
using Suwen.ToFit.DM;

using Newtonsoft.Json;

using System.Configuration;
using System.Xml;


namespace Suwen.ToFit.BLL
{
    public class BLL_User
    {
        DAL_User oDAL = new DAL_User();

        private string TheToken = ConfigurationManager.AppSettings["token"].ToString();

        public BLL_User()
        {
        }


        /// <summary>
        /// 判断该登录名是否已经存在
        /// </summary>
        /// <returns><c>true</c> if this instance is login name exist the specified loginName; otherwise, <c>false</c>.</returns>
        /// <param name="loginName">Login name.</param>
        public bool IsLoginNameExist(string loginName)
        {
            return oDAL.IsLoginNameExist(loginName);
        }




        /// <summary>
        /// 新建用户信息
        /// </summary>
        /// <returns>成功返回1，失败返回0</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string CreateUser(DM_UserInfo dmInfo)
        {
            return oDAL.CreateUser(dmInfo);
        }


        /// <summary>
        /// 是否已经绑定第三方登录
        /// </summary>
        /// <returns><c>true</c> if this instance is third part bind the specified thirdpartID; otherwise, <c>false</c>.</returns>
        /// <param name="thirdpartID">Thirdpart I.</param>
        public bool IsThirdPartBind(string thirdpartID)
        {
            return oDAL.IsThirdPartBind(thirdpartID);
        }


        /// <summary>
        /// 绑定用户的第三方登录账号
        /// </summary>
        /// <param name="SNS">SN.</param>
        /// <param name="userID">User I.</param>
        /// <param name="thirdpartID">Thirdpart I.</param>
        public void BindThirdpartID(int SNS, int userID, string thirdpartID)
        {
            oDAL.BindThirdpartID(SNS, userID, thirdpartID);
        }



        /// <summary>
        /// 用户登录 - 登录成功返回用户ID，登录失败返回0，返回-1表示程序出错，返回-2表示尚未绑定第三方登录
        /// </summary>
        /// <param name="type">登录类型-0用户名登录，1新浪微博，2，微信.</param>
        /// <param name="loginName">Login name.</param>
        /// <param name="password">Password.</param>
        /// <param name="platform">用户使用的手机平台.</param>
        public int Login(int type, string loginName, string password, string platform)
        {
            return oDAL.Login(type, loginName, password, platform);
        }


        public int LoginWithDeviceID(int type, string loginName, string password, string platform, string deviceID)
        {
            return oDAL.LoginWithDeviceID(type, loginName, password, platform, deviceID);
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <returns>The user info.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="token">Token.</param>
        public DM_UserInfo GetUserInfo(int userID, string token)
        {
            if (token == TheToken)
            {
                return oDAL.GetUserInfo(userID);
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 根据GUID获取用户信息
        /// </summary>
        /// <returns>The info by GUI.</returns>
        /// <param name="GUID">GUI.</param>
        public DM_UserInfo GetInfoByGUID(string GUID,  string token)
        {
            if (token == TheToken)
            {
                return oDAL.GetInfoByGUID(GUID);
            }
            else
            {
                return null;
            }
        }


        //获取用户的手机信息
        public DM_PhoneInfo GetUserPhoneInfo(int userID)
        {
            return oDAL.GetUserPhoneInfo(userID);
        }
    }
}

