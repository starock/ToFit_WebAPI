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
    public class DAL_OnlineService
    {
        public DAL_OnlineService()
        {
        }



     
        /// <summary>
        /// 添加用户反馈
        /// </summary>
        /// <returns>The feedback.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string AddFeedback(DM_Feedback dmInfo)
        {
            try{
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into OnlineService(");
                strSql.Append("userID, title, feedback)");
                strSql.Append(" values (");
                strSql.Append(dmInfo.userID+", @title, @feedback)" );
                SqlParameter[] parameters = {
                    new SqlParameter("@title", SqlDbType.NVarChar,250),
                    new SqlParameter("@feedback", SqlDbType.NVarChar,-1)
                };

                parameters[0].Value = dmInfo.title;
                parameters[1].Value = dmInfo.feedback;


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
        /// 获取某位用户的反馈信息列表
        /// </summary>
        /// <returns>The feedback list.</returns>
        /// <param name="userID">大于0则选择指定用户，等于0则表示显示所有用户的反馈信息</param>
        /// <param name="state">0表示客服尚未回复的反馈，1表示已经反馈的，2表示获取全部状态的信息</param>
        public List<DM_Feedback> GetFeedbackList(int userID, int state)
        {
            List<DM_Feedback> listResult = new List<DM_Feedback>();


            string strSQL = "";


            if (userID > 0)
            {
                if (state > 1)
                {
                    strSQL = "select * from OnlineService where userID=" + userID + " order by ID DESC";
                }
                else
                {
                    strSQL = "select * from OnlineService where userID=" + userID + " and [state]="+state+" order by ID DESC";
                }
            }
            else
            {
                if (state > 1)
                {
                    strSQL = "select * from OnlineService order by ID DESC";
                }
                else
                {
                    strSQL = "select * from OnlineService where [state]="+state+" order by ID DESC";
                }
            }

     

            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_Feedback dmInfo = new DM_Feedback();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                    dmInfo.title = dtList.Rows[i]["title"].ToString();
                    dmInfo.feedback = dtList.Rows[i]["feedback"].ToString();
                    dmInfo.createTime = dtList.Rows[i]["createTime"].ToString();
                    dmInfo.reply = dtList.Rows[i]["reply"].ToString();

                    dmInfo.score = Convert.ToInt32(dtList.Rows[i]["score"].ToString());
                    dmInfo.state = Convert.ToInt32(dtList.Rows[i]["state"].ToString());

                    listResult.Add(dmInfo);

                }
            }

            return listResult;
        }






    }
}

