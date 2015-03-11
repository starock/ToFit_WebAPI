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
    public class DAL_Trainer
    {
        public DAL_Trainer()
        {
        }



        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <returns>The trainer list.</returns>
        public DataTable GetTrainerList()
        {
            string strSQL = "select * from Trainer_Info order by sortID DESC ";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);
           
            return dtList;
        }




        /// <summary>
        /// 获取教练详细介绍
        /// </summary>
        /// <returns>The trainer info.</returns>
        /// <param name="ID">I.</param>
        public DM_Trainer GetTrainerInfo(int ID)
        {
            DM_Trainer dmInfo = null;


            string strSQL = "select * from Trainer_Info where ID="+ID;
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                dmInfo = new DM_Trainer();

                dmInfo.ID = Convert.ToInt32(dtList.Rows[0]["ID"].ToString());
                dmInfo.name = dtList.Rows[0]["name"].ToString();
                dmInfo.sex = dtList.Rows[0]["sex"].ToString();
                dmInfo.school = dtList.Rows[0]["school"].ToString();
                dmInfo.position = dtList.Rows[0]["position"].ToString();
                dmInfo.honor = dtList.Rows[0]["honor"].ToString();
                dmInfo.goodAt = dtList.Rows[0]["goodAt"].ToString();
                dmInfo.motto = dtList.Rows[0]["motto"].ToString();
                dmInfo.photo = dtList.Rows[0]["photo"].ToString();
                dmInfo.sortID = Convert.ToInt32(dtList.Rows[0]["sortID"].ToString());
                dmInfo.state = Convert.ToInt32(dtList.Rows[0]["state"].ToString());
                dmInfo.remark = dtList.Rows[0]["remark"].ToString();
            }

            return dmInfo;
        }


        /// <summary>
        /// 根据ID获取教练名字
        /// </summary>
        /// <returns>The trainer name.</returns>
        /// <param name="ID">I.</param>
        public string GetTrainerName(int ID)
        {
            string name = "";

            string strSQL = "select * from Trainer_Info where ID="+ID;
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if (dtList.Rows.Count > 0)
            {
                name = dtList.Rows[0]["name"].ToString();
            }

            return name;
        }



        /// <summary>
        /// 添加问教练的记录
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="dmInfo">dmInfo.type值为0表示留言来自用户，值为1表示来自教练</param>
        public string Ask_Add(DM_Ask dmInfo)
        {
            try{

                string GUID = System.Guid.NewGuid().ToString();


                string strSQL = "";

                //先检查该用户是否与教练有正在交谈的对话
                strSQL = "select * from Ask_State where userID="+dmInfo.userID+" and trainerID="+dmInfo.trainerID+" and state=0";

                DataTable dtChat = SQLHelper.ExecuteDt(strSQL);

                if(dtChat.Rows.Count>0)
                {
                    GUID = dtChat.Rows[0]["GUID"].ToString();

                    //如果有正在进行的对话则更新replied字段的值
                    strSQL = "update Ask_State set replied="+dmInfo.type+" where userID="+dmInfo.userID+" and trainerID="+dmInfo.trainerID+" and state=0";
                    SQLHelper.ExecuteSql(strSQL);
                }
                else
                {
                    GUID = System.Guid.NewGuid().ToString();

                    strSQL ="insert into Ask_State(userID, trainerID, replied, GUID) values("+dmInfo.userID+", "+dmInfo.trainerID+", 0, '"+GUID+"')";
                    SQLHelper.ExecuteSql(strSQL);
                }


                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Ask_Record(");
                strSql.Append("userID, trainerID, type, GUID, words)");
                strSql.Append(" values (");
                strSql.Append(dmInfo.userID+","+dmInfo.trainerID+","+dmInfo.type+", '"+GUID+"', @words)" );
                SqlParameter[] parameters = {
                    new SqlParameter("@words", SqlDbType.NVarChar,250)
                };

                parameters[0].Value = dmInfo.words;
              

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
        /// 教练回复
        /// </summary>
        /// <param name="GUID">GUI.</param>
        /// <param name="words">Words.</param>
        public string Reply(string GUID, string words)
        {
            try{

                string strSQL = "select * from Ask_State where GUID='"+GUID+"'";
                DataTable dtState = SQLHelper.ExecuteDt(strSQL);

                if (dtState.Rows.Count > 0)
                {
                    int userID = Convert.ToInt32(dtState.Rows[0]["userID"].ToString());
                    int trainerID = Convert.ToInt32(dtState.Rows[0]["trainerID"].ToString());

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into Ask_Record(");
                    strSql.Append("userID, trainerID, type, GUID, words)");
                    strSql.Append(" values (");
                    strSql.Append(userID+","+trainerID+",1, '"+GUID+"', @words)" );
                    SqlParameter[] parameters = {
                        new SqlParameter("@words", SqlDbType.NVarChar,250)
                    };

                    parameters[0].Value = words;


                    int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);

                    if (rows > 0)
                    {
                        strSQL = "update Ask_State set replied=1 where GUID='"+GUID+"'";
                        SQLHelper.ExecuteSql(strSQL);

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
                return "-1: " + ex.Message;
            }

        }







        /// <summary>
        /// 获取等待回复的问题列表
        /// </summary>
        /// <returns>The ask waitting.</returns>
        public List<DM_AskState> GetAskWaiting()
        {
            List<DM_AskState> listResult = new List<DM_AskState>();

            string strSQL = "select * from Ask_State where replied=0 and state=0 order by createTime";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_AskState dmInfo = new DM_AskState();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                    dmInfo.trainerID = Convert.ToInt32(dtList.Rows[i]["trainerID"].ToString());
                    dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();

                    string strSQLChat = "select * from Ask_Record where type=0 and GUID='"+dmInfo.GUID+"' order by ID DESC";
                    DataTable dtChat = SQLHelper.ExecuteDt(strSQLChat);

                    if (dtChat.Rows.Count > 0)
                    {
                        dmInfo.lastTime = dtChat.Rows[0]["createTime"].ToString();
                        dmInfo.lastWords = dtChat.Rows[0]["words"].ToString();
                    }

                    listResult.Add(dmInfo);
                }
            }


            return listResult;

        }



        /// <summary>
        /// 获取用户的提问列表
        /// </summary>
        /// <returns>The my ask list.</returns>
        public List<DM_AskLast> GetMyAskList(int userID)
        {

            List<DM_AskLast> listResult = new List<DM_AskLast>();

            try
            {
            string strSQL = "select * from Ask_State where userID="+userID+" and state>=0 order by createTime DESC";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_AskLast dmInfo = new DM_AskLast();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                    dmInfo.trainerID = Convert.ToInt32(dtList.Rows[i]["trainerID"].ToString());

                    if(dmInfo.trainerID>0)
                    {
                        DM_Trainer dmTrainer = GetTrainerInfo(dmInfo.trainerID);

                            dmInfo.name = dmTrainer.name;
                            dmInfo.position = dmTrainer.position;
                            dmInfo.photo = dmTrainer.photo;
                    }
                    else
                    {
                            dmInfo.name = "快健身教练群";
                            dmInfo.position = "";
                            dmInfo.photo = "";
                    }


                    dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();

                    string strSQLChat = "select * from Ask_Record where GUID='"+dmInfo.GUID+"' order by ID DESC";
                    DataTable dtChat = SQLHelper.ExecuteDt(strSQLChat);

                    if (dtChat.Rows.Count > 0)
                    {
                        dmInfo.lastTime = dtChat.Rows[0]["createTime"].ToString();
                        //dmInfo.lastWords = dtChat.Rows[0]["words"].ToString();
                    }

                    listResult.Add(dmInfo);
                }
            }
            }
            catch(Exception ex)
            {
                //DM_AskLast dmInfo = new DM_AskLast();

                //dmInfo.photo = ex.Message;

                //listResult.Add(dmInfo);
            }

            return listResult;
        }






        /// <summary>
        /// 获取用户与教练的对话列表
        /// </summary>
        /// <returns>The ask record.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="trainerID">Trainer I.</param>
        public List<DM_Ask> GetAskRecord(int userID, int trainerID)
        {
            List<DM_Ask> listResult = new List<DM_Ask>();

            string strSQL = "select * from Ask_Record where userID="+userID+" and trainerID="+trainerID+" order by createTime";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_Ask dmInfo = new DM_Ask();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                    dmInfo.trainerID = Convert.ToInt32(dtList.Rows[i]["trainerID"].ToString());
                    dmInfo.createTime = dtList.Rows[i]["createTime"].ToString();
                    dmInfo.words = dtList.Rows[i]["words"].ToString();
                    dmInfo.type = Convert.ToInt32(dtList.Rows[i]["type"].ToString());

                    listResult.Add(dmInfo);
                }
            }


            return listResult;
        }


        /// <summary>
        /// 根据GUID来获取对话列表
        /// </summary>
        /// <returns>The ask record.</returns>
        /// <param name="GUID">GUI.</param>
        public List<DM_Ask> GetAskRecord_ByGUID(string GUID)
        {
            List<DM_Ask> listResult = new List<DM_Ask>();

            string strSQL = "select * from Ask_Record where GUID='"+GUID+"' order by createTime";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_Ask dmInfo = new DM_Ask();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                    dmInfo.trainerID = Convert.ToInt32(dtList.Rows[i]["trainerID"].ToString());
                    dmInfo.createTime = dtList.Rows[i]["createTime"].ToString();
                    dmInfo.words = dtList.Rows[i]["words"].ToString();
                    dmInfo.type = Convert.ToInt32(dtList.Rows[i]["type"].ToString());

                    listResult.Add(dmInfo);
                }
            }


            return listResult;
        }












    }
}

