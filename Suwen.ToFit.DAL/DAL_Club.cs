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
    public class DAL_Club
    {
        public DAL_Club()
        {
        }





        /// <summary>
        /// 添加会所信息
        /// </summary>
        /// <returns>The club info.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string AddClubInfo(DM_Club dmInfo)
        {
            try{
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Club_Info(");
                strSql.Append("clubName, address, phone, intro, GUID, province, city, area)");
                strSql.Append(" values (");
                strSql.Append("@clubName, @address, @phone, @intro, @GUID, @province, @city, @area)" );
                SqlParameter[] parameters = {
                    new SqlParameter("@clubName", SqlDbType.NVarChar,100),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@intro", SqlDbType.NVarChar,-1),
                    new SqlParameter("@GUID", SqlDbType.NVarChar,50),
                    new SqlParameter("@province", SqlDbType.NVarChar,50),
                    new SqlParameter("@city", SqlDbType.NVarChar,50),
                    new SqlParameter("@area", SqlDbType.NVarChar,50)
                };

                parameters[0].Value = dmInfo.clubName;
                parameters[1].Value = dmInfo.address;
                parameters[2].Value = dmInfo.phone;
                parameters[3].Value = dmInfo.intro;
                parameters[4].Value = dmInfo.GUID;
                parameters[5].Value = dmInfo.province;
                parameters[6].Value = dmInfo.city;
                parameters[7].Value = dmInfo.area;


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
            catch(Exception ex)
            {
                return "-1: " + ex.Message;
            }
        }




        /// <summary>
        /// 获取健身会所列表
        /// </summary>
        /// <returns>The club list.</returns>
        public List<DM_Club> GetClubList()
        {
            List<DM_Club> listResult = new List<DM_Club>();

            string strSQL = "select * from Club_Info order by clubName DESC";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_Club dmInfo = new DM_Club();
                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();
                    dmInfo.clubName = dtList.Rows[i]["clubName"].ToString();
                    dmInfo.phone = dtList.Rows[i]["phone"].ToString();
                    dmInfo.province = dtList.Rows[i]["province"].ToString();
                    dmInfo.city = dtList.Rows[i]["city"].ToString();
                    dmInfo.address = dtList.Rows[i]["address"].ToString();
                    dmInfo.intro = dtList.Rows[i]["intro"].ToString();

                    dmInfo.counterRecommend = Convert.ToInt32(dtList.Rows[i]["counterRecommend"].ToString());


                    listResult.Add(dmInfo);
                }
            }


            return listResult;
        }



        /// <summary>
        /// 根据ID获取会所信息
        /// </summary>
        /// <returns>The club info.</returns>
        /// <param name="ID">I.</param>
        public DM_Club GetClubInfo(int ID)
        {
            DM_Club dmInfo = null;

            string strSQL = "select * from Club_Info where ID="+ID;
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                dmInfo = new DM_Club();

                dmInfo.ID = Convert.ToInt32(dtList.Rows[0]["ID"].ToString());
                dmInfo.GUID = dtList.Rows[0]["GUID"].ToString();
                dmInfo.clubName = dtList.Rows[0]["clubName"].ToString();
                dmInfo.phone = dtList.Rows[0]["phone"].ToString();
                dmInfo.province = dtList.Rows[0]["province"].ToString();
                dmInfo.city = dtList.Rows[0]["city"].ToString();
                dmInfo.address = dtList.Rows[0]["address"].ToString();
                dmInfo.intro = dtList.Rows[0]["intro"].ToString();

                dmInfo.counterRecommend = Convert.ToInt32(dtList.Rows[0]["counterRecommend"].ToString());
            }


            return dmInfo;
        }






    }
}

