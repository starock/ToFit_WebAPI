using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;



using Suwen.ToFit.DBConn;
using Suwen.ToFit.DM;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using System.Configuration;
using System.Xml;




namespace Suwen.ToFit.DAL
{
	public class DAL_User
	{

        private string token;

		public DAL_User ()
		{
            token = ConfigurationManager.AppSettings["token"].ToString();// "stormer@2fit(MD5)";
		}




		/// <summary>
		/// 判断该登录名是否已经存在
		/// </summary>
		/// <returns><c>true</c> if this instance is login name exist the specified loginName; otherwise, <c>false</c>.</returns>
		/// <param name="loginName">Login name.</param>
		public bool IsLoginNameExist(string loginName)
		{
			string strSQL = "select * from User_BaseInfo where loginName='"+loginName+"'";
			DataTable dtResult = SQLHelper.ExecuteDt(strSQL);

			if (dtResult.Rows.Count > 0) 
			{
				return true;

			} 
			else 
			{
				return false;
			}

		}


		/// <summary>
		/// 新建用户信息
		/// </summary>
		/// <returns>成功返回1，失败返回0</returns>
		/// <param name="dmInfo">Dm info.</param>
		public string CreateUser(DM_UserInfo dmInfo)
		{
            try
            {
                //判断登录名是否已经被占用
                string strSQL = "select * from User_BaseInfo where loginName='"+dmInfo.loginName+"'";
                DataTable dtResult = SQLHelper.ExecuteDt(strSQL);

                if (dtResult.Rows.Count == 0) 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into User_BaseInfo(");
                    strSql.Append("loginName, cipher, realName, birthYear, sex, bodyHeight, bodyWeight, targetWeight, preferencePlace, aim, intensity, phone, addressProvince, addressCity, addressArea, addressDetail, remark, GUID, Platform, deviceID)");
                    strSql.Append(" values (");
                    strSql.Append("@loginName, @cipher, @realName,"+dmInfo.birthYear+", @sex, "+dmInfo.bodyHeight+", "+dmInfo.bodyWeight+", "+dmInfo.targetWeight+", @preferencePlace, @aim, @intensity, @phone, @addressProvince, @addressCity, @addressArea, @addressDetail, @remark, @GUID, @Platform, @deviceID)");
                    SqlParameter[] parameters = {
                        new SqlParameter("@loginName", SqlDbType.NVarChar,50),
                        new SqlParameter("@cipher", SqlDbType.NVarChar,50),
                        new SqlParameter("@realName", SqlDbType.NVarChar,50),
                        new SqlParameter("@sex", SqlDbType.NVarChar,10),
                        new SqlParameter("@preferencePlace", SqlDbType.NVarChar,50),
                        new SqlParameter("@aim", SqlDbType.NVarChar,50),
                        new SqlParameter("@intensity", SqlDbType.NVarChar,50),
                        new SqlParameter("@phone", SqlDbType.NVarChar,50),
                        new SqlParameter("@addressProvince", SqlDbType.NVarChar,50),
                        new SqlParameter("@addressCity", SqlDbType.NVarChar,50),
                        new SqlParameter("@addressArea", SqlDbType.NVarChar,50),
                        new SqlParameter("@addressDetail", SqlDbType.NVarChar,250),
                        new SqlParameter("@remark", SqlDbType.NVarChar,250),
                        new SqlParameter("@GUID", SqlDbType.NVarChar,250),
                        new SqlParameter("@Platform", SqlDbType.NVarChar,50),
                        new SqlParameter("@deviceID", SqlDbType.NVarChar,50)
                    };

                    parameters[0].Value = dmInfo.loginName == null?"":dmInfo.loginName;
                    parameters[1].Value = dmInfo.cipher == null?"":dmInfo.cipher;
                    parameters[2].Value = dmInfo.realName == null?"":dmInfo.realName;
                    parameters[3].Value = dmInfo.sex == null?"":dmInfo.sex;
                    parameters[4].Value = dmInfo.preferencePlace == null?"":dmInfo.preferencePlace;
                    parameters[5].Value = dmInfo.aim == null?"":dmInfo.aim;
                    parameters[6].Value = dmInfo.intensity == null?"":dmInfo.intensity;
                    parameters[7].Value = dmInfo.phone == null?"":dmInfo.phone;
                    parameters[8].Value = dmInfo.addressProvince == null?"":dmInfo.addressProvince;
                    parameters[9].Value = dmInfo.addressCity == null?"":dmInfo.addressCity;
                    parameters[10].Value = dmInfo.addressArea == null?"":dmInfo.addressArea;
                    parameters[11].Value = dmInfo.addressDetail == null?"":dmInfo.addressDetail;
                    parameters[12].Value = dmInfo.remark== null?"":dmInfo.remark;
                    parameters[13].Value = dmInfo.GUID== null?"":dmInfo.GUID;
                    parameters[14].Value = dmInfo.platform== null?"":dmInfo.platform;
                    parameters[15].Value = dmInfo.deviceID== null?"":dmInfo.deviceID;
         


                    int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);

                    if (rows > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }

            }
            catch(Exception ex)
            {
               
                /*
                string strError = ex.Message.Replace('\'',' ');
                strError = strError.Replace('@', ' ');
                string strSQL = "update user_baseInfo set remark='"+strError+"'";
                SQLHelper.ExecuteDt(strSQL);
                */


                return "-1";
            }

		}





		/// <summary>
		/// 是否已经绑定第三方登录
		/// </summary>
		/// <returns><c>true</c> if this instance is third part bind the specified thirdpartID; otherwise, <c>false</c>.</returns>
		/// <param name="thirdpartID">Thirdpart I.</param>
		public bool IsThirdPartBind(string thirdpartID)
		{
			string strSQL = "select * from User_ThirdpartInfo where thirdpartID='"+thirdpartID+"'";
			DataTable dtResult = SQLHelper.ExecuteDt(strSQL);

			if (dtResult.Rows.Count > 0) 
			{
				return true;

			} 
			else 
			{
				return false;
			}
		}


		/// <summary>
		/// 绑定用户的第三方登录账号
		/// </summary>
		/// <param name="SNS">SN.</param>
		/// <param name="userID">User I.</param>
		/// <param name="thirdpartID">Thirdpart I.</param>
		public void BindThirdpartID(int SNS, int userID, string thirdpartID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into User_ThirdpartInfo(");
			strSql.Append("SNS, userID, thirdpartID)");
			strSql.Append(" values (");
			strSql.Append("@SNS, @userID, @thirdpartID)");
			SqlParameter[] parameters = {
				new SqlParameter("@loginName", SqlDbType.Int,8),
				new SqlParameter("@userID", SqlDbType.Int,8),
				new SqlParameter("@thirdpartID", SqlDbType.NVarChar,100)

			};

			parameters[0].Value = SNS;
			parameters[1].Value = userID;
			parameters[2].Value = thirdpartID;


			SQLHelper.ExecuteSql(strSql.ToString(), parameters);

		}


       

        /// <summary>
        /// 用户登录 - 登录成功返回用户ID，登录失败返回0，返回-1表示程序出错，返回-2表示尚未绑定第三方登录
        /// </summary>
        /// <param name="type">登录类型-0用户名登录，1新浪微博，2，微信.</param>
        /// <param name="loginName">Login name.</param>
        /// <param name="password">Password.</param>
        /// <param name="platform">用户使用的手机平台.</param>
        public int Login(int type, string loginName, string password, string platform )
        {
            try
            {
                string strSQL = "";

                if(type==0)
                {
                    strSQL = "select * from User_BaseInfo where loginName='" + loginName + "' and cipher='" + password + "'";

                    DataTable dtUser = SQLHelper.ExecuteDt(strSQL);
                    if (dtUser.Rows.Count > 0)
                    {
                        int userID = Convert.ToInt32(dtUser.Rows[0]["ID"].ToString());

                        //更新用户当前的手机平台
                        UpdatePlatform(userID, platform);
                           
                        return userID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    strSQL = "select * from User_thirdpartInfo where thirdpartID='" + loginName + "' and SNS="+type ;
                    DataTable dt3rd = SQLHelper.ExecuteDt(strSQL);
                    if (dt3rd.Rows.Count > 0)
                    {
                        strSQL = "select * from User_BaseInfo where loginName='" + loginName + "' and cipher='" + password + "'";
                        DataTable dtUser = SQLHelper.ExecuteDt(strSQL);
                        if (dtUser.Rows.Count > 0)
                        {
                            int userID = Convert.ToInt32(dtUser.Rows[0]["ID"].ToString());

                            //更新用户当前的手机平台
                            UpdatePlatform(userID, platform);

                            return userID;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return -2; //尚未绑定第三方登录账号
                    }
                }
               

            }
            catch(Exception ex)
            {
                return -1;
            }

        }



        /// <summary>
        /// 用户登录 - 登录成功返回用户ID，登录失败返回0，返回-1表示程序出错，返回-2表示尚未绑定第三方登录
        /// </summary>
        /// <param name="type">登录类型-0用户名登录，1新浪微博，2，微信.</param>
        /// <param name="loginName">Login name.</param>
        /// <param name="password">Password.</param>
        /// <param name="platform">用户使用的手机平台.</param>
        public int LoginWithDeviceID(int type, string loginName, string password, string platform, string deviceID)
        {
            try
            {
                string strSQL = "";

                if(type==0)
                {
                    strSQL = "select * from User_BaseInfo where loginName='" + loginName + "' and cipher='" + password + "'";

                    DataTable dtUser = SQLHelper.ExecuteDt(strSQL);
                    if (dtUser.Rows.Count > 0)
                    {
                        int userID = Convert.ToInt32(dtUser.Rows[0]["ID"].ToString());

                        //更新用户当前的手机平台
                        UpdatePlatform(userID, platform);

                        //更新用户的设备ID
                        UpdateDeviceID(userID, deviceID);

                        return userID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    strSQL = "select * from User_thirdpartInfo where thirdpartID='" + loginName + "' and SNS="+type ;
                    DataTable dt3rd = SQLHelper.ExecuteDt(strSQL);
                    if (dt3rd.Rows.Count > 0)
                    {
                        strSQL = "select * from User_BaseInfo where loginName='" + loginName + "' and cipher='" + password + "'";
                        DataTable dtUser = SQLHelper.ExecuteDt(strSQL);
                        if (dtUser.Rows.Count > 0)
                        {
                            int userID = Convert.ToInt32(dtUser.Rows[0]["ID"].ToString());

                            //更新用户当前的手机平台
                            UpdatePlatform(userID, platform);

                            //更新用户的设备ID
                            UpdateDeviceID(userID, deviceID);

                            return userID;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return -2; //尚未绑定第三方登录账号
                    }
                }


            }
            catch(Exception ex)
            {
                return -1;
            }

        }




        /// <summary>
        /// 更新用户的手机平台
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="platform">Platform.</param>
        private void UpdatePlatform(int userID, string platform)
        {
            string strSQL = "update User_BaseInfo set platform='"+platform+"' where ID="+userID;
            SQLHelper.ExecuteSql(strSQL);
        }


        /// <summary>
        /// 更新用户的设备ID
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="platform">Platform.</param>
        private void UpdateDeviceID(int userID, string deviceID)
        {
            string strSQL = "update User_BaseInfo set deviceID='"+deviceID+"' where ID="+userID;
            SQLHelper.ExecuteSql(strSQL);
        }




        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <returns>The user info.</returns>
        /// <param name="userID">User I.</param>
        public DM_UserInfo GetUserInfo(int userID)
        {
            DM_UserInfo dmResult = null;

            if(token == this.token)
            {
                string strSQL = "select * from User_BaseInfo where ID="+userID;
                DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

                if(dtInfo.Rows.Count>0)
                {
                    dmResult = new DM_UserInfo();

                    dmResult.ID = userID;

                    dmResult.loginName = dtInfo.Rows[0]["loginName"].ToString();
                    dmResult.realName = dtInfo.Rows[0]["realName"].ToString();
                    dmResult.birthYear = Convert.ToInt32(dtInfo.Rows[0]["birthYear"].ToString());
                    dmResult.sex = dtInfo.Rows[0]["sex"].ToString();

                    dmResult.bodyHeight = Convert.ToDecimal(dtInfo.Rows[0]["bodyHeight"].ToString());
                    dmResult.bodyWeight = Convert.ToDecimal(dtInfo.Rows[0]["bodyWeight"].ToString());
                    dmResult.targetWeight = Convert.ToDecimal(dtInfo.Rows[0]["targetWeight"].ToString());

                    dmResult.aim = dtInfo.Rows[0]["aim"].ToString();
                    dmResult.intensity = dtInfo.Rows[0]["intensity"].ToString();
                    dmResult.phone = dtInfo.Rows[0]["phone"].ToString();
                 
                    dmResult.addressProvince = dtInfo.Rows[0]["addressProvince"].ToString();
                    dmResult.addressCity = dtInfo.Rows[0]["addressCity"].ToString();
                    dmResult.addressArea = dtInfo.Rows[0]["addressArea"].ToString();
                    dmResult.addressDetail = dtInfo.Rows[0]["addressDetail"].ToString();


                    dmResult.remark = dtInfo.Rows[0]["remark"].ToString();

                    dmResult.platform = dtInfo.Rows[0]["platform"].ToString();

                    dmResult.deviceID = dtInfo.Rows[0]["deviceID"].ToString();

                    dmResult.GUID =  dtInfo.Rows[0]["GUID"].ToString();
                }
            }

            return dmResult;
        }




        /// <summary>
        /// 根据GUID获取用户信息
        /// </summary>
        /// <returns>The info by GUI.</returns>
        /// <param name="GUID">GUI.</param>
        public DM_UserInfo GetInfoByGUID(string GUID)
        {

            DM_UserInfo dmResult = null;

            if(token == this.token)
            {
                string strSQL = "select * from User_BaseInfo where GUID='"+GUID+"'";
                DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

                if(dtInfo.Rows.Count>0)
                {
                    dmResult = new DM_UserInfo();

                    dmResult.ID = Convert.ToInt32(dtInfo.Rows[0]["ID"].ToString());

                    dmResult.loginName = dtInfo.Rows[0]["loginName"].ToString();
                    dmResult.realName = dtInfo.Rows[0]["realName"].ToString();
                    dmResult.birthYear = Convert.ToInt32(dtInfo.Rows[0]["birthYear"].ToString());
                    dmResult.sex = dtInfo.Rows[0]["sex"].ToString();

                    dmResult.bodyHeight = Convert.ToDecimal(dtInfo.Rows[0]["bodyHeight"].ToString());
                    dmResult.bodyWeight = Convert.ToDecimal(dtInfo.Rows[0]["bodyWeight"].ToString());
                    dmResult.targetWeight = Convert.ToDecimal(dtInfo.Rows[0]["targetWeight"].ToString());

                    dmResult.aim = dtInfo.Rows[0]["aim"].ToString();
                    dmResult.intensity = dtInfo.Rows[0]["intensity"].ToString();
                    dmResult.phone = dtInfo.Rows[0]["phone"].ToString();

                    dmResult.addressProvince = dtInfo.Rows[0]["addressProvince"].ToString();
                    dmResult.addressCity = dtInfo.Rows[0]["addressCity"].ToString();
                    dmResult.addressArea = dtInfo.Rows[0]["addressArea"].ToString();
                    dmResult.addressDetail = dtInfo.Rows[0]["addressDetail"].ToString();


                    dmResult.remark = dtInfo.Rows[0]["remark"].ToString();

                    dmResult.platform = dtInfo.Rows[0]["platform"].ToString();

                    dmResult.deviceID = dtInfo.Rows[0]["deviceID"].ToString();

                    dmResult.GUID =  dtInfo.Rows[0]["GUID"].ToString();
                }
            }

            return dmResult;
        }



        //获取用户的手机信息
        public DM_PhoneInfo GetUserPhoneInfo(int userID)
        {
            DM_PhoneInfo dmPhone = null;

            string strSQL = "select ID, platform, deviceID from User_BaseInfo where ID="+userID;
           
            DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

            if (dtInfo.Rows.Count > 0)
            {
                dmPhone = new DM_PhoneInfo();

                dmPhone.userID = userID;
                dmPhone.platform = dtInfo.Rows[0]["platform"].ToString();
                dmPhone.deviceID = dtInfo.Rows[0]["deviceID"].ToString();
            }

            return dmPhone;
        }


	}
}

