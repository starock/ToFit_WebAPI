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
    public class DAL_Plan
    {
        public DAL_Plan()
        {
        }


        /// <summary>
        /// 添加健身方案信息
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public int Plan_Add(DM_Plan dmInfo)
        {
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Plan_BaseInfo(");
                strSql.Append("planName, type_match, type_place, type_part, type_intensity, days, remark)");
                strSql.Append(" values (");
                strSql.Append("@planName,@type_match,@type_place,@type_part,@type_intensity,"+dmInfo.days + ",@remark)");
                SqlParameter[] parameters = {
                    new SqlParameter("@planName", SqlDbType.NVarChar,250),
                    new SqlParameter("@type_match", SqlDbType.NVarChar,50),
                    new SqlParameter("@type_place", SqlDbType.NVarChar,50),
                    new SqlParameter("@type_part", SqlDbType.NVarChar,50),
                    new SqlParameter("@type_intensity", SqlDbType.NVarChar,50),
                    new SqlParameter("@remark", SqlDbType.NVarChar,10000)
                };

                parameters[0].Value = dmInfo.planName;
                parameters[1].Value = dmInfo.type_match;
                parameters[2].Value = dmInfo.type_place;
                parameters[3].Value = dmInfo.type_part;
                parameters[4].Value = dmInfo.type_intensity;
                parameters[5].Value = dmInfo.remark;
     

                int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);

                if (rows > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return -1;
            }

        }




        /// <summary>
        /// 添加健身方案-项目内容
        /// </summary>
        /// <returns>The add item.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string Plan_AddItem(DM_PlanItem dmInfo)
        {
            try{
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Plan_Item(");
                strSql.Append("planID, itemContent, fitID, theGroup, theNumber, theUnit)");
                strSql.Append(" values (");
                strSql.Append(dmInfo.planID + ",@itemContent,"+dmInfo.fitID +","+dmInfo.theGroup +","+dmInfo.theNumber +", @theUnit)" );
                SqlParameter[] parameters = {
                    new SqlParameter("@itemContent", SqlDbType.NVarChar,250),
                    new SqlParameter("@theUnit", SqlDbType.NVarChar,50)
                };

                parameters[0].Value = dmInfo.itemContent;
                parameters[1].Value = dmInfo.theUnit;


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
        /// 获取健康方案列表
        /// </summary>
        /// <returns>The plan list.</returns>
        public List<DM_Plan> GetPlanList()
        {
            List<DM_Plan> listPlan = new List<DM_Plan>();

            string strSQL = "select * from Plan_BaseInfo order by sortID DESC";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_Plan dmInfo = new DM_Plan();
                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();
                    dmInfo.planName = dtList.Rows[i]["planName"].ToString();
                    dmInfo.type_match = dtList.Rows[i]["type_match"].ToString();
                    dmInfo.type_place = dtList.Rows[i]["type_place"].ToString();
                    dmInfo.type_part = dtList.Rows[i]["type_part"].ToString();
                    dmInfo.type_intensity = dtList.Rows[i]["type_intensity"].ToString();
                    dmInfo.days = Convert.ToInt32(dtList.Rows[i]["days"].ToString());

                    DateTime dtTime = DateTime.Parse(dtList.Rows[i]["createTime"].ToString());
                    dmInfo.createTime = dtTime.Year.ToString()+"年"+dtTime.Month.ToString()+"月"+dtTime.Day.ToString()+"日";

                    dmInfo.counterLike = Convert.ToInt32(dtList.Rows[i]["counterLike"].ToString());

                  
                    if(dtList.Rows[i]["thumb"].ToString().Trim() == "")
                    {
                        dmInfo.thumb = "thumbPlanDefault.jpg";
                    }
                    else
                    {
                        dmInfo.thumb = dtList.Rows[i]["thumb"].ToString();
                    }
                  

                    dmInfo.remark = dtList.Rows[i]["remark"].ToString();

                    listPlan.Add(dmInfo);
                }
            }


            return listPlan;
        }


        /// <summary>
        /// 列表只返回方案名称和ID
        /// </summary>
        /// <returns>The plan name list.</returns>
        public DataTable GetPlanNameList()
        {
            string strSQL = "select ID, planName from Plan_BaseInfo order by sortID";
            return SQLHelper.ExecuteDt(strSQL);
        }



        /// <summary>
        /// 根据方案ID返回属于它的方案具体内容项
        /// </summary>
        /// <returns>The plan items.</returns>
        /// <param name="planID">Plan I.</param>
        public List<DM_PlanItem>GetPlanItems(int planID)
        {
            List<DM_PlanItem> listResult = new List<DM_PlanItem>();

            string strSQL = "select * from Plan_Item where planID="+planID;
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_PlanItem dmItem = new DM_PlanItem();

                    dmItem.ID = Guid.Parse(dtList.Rows[i]["ID"].ToString());
                    dmItem.planID = planID;
                    dmItem.itemContent = dtList.Rows[i]["itemContent"].ToString();
                    dmItem.sortID = Convert.ToInt32(dtList.Rows[i]["sortID"].ToString());
                    dmItem.fitID = Convert.ToInt32(dtList.Rows[i]["fitID"].ToString());
                    dmItem.theGroup = Convert.ToInt32(dtList.Rows[i]["theGroup"].ToString());
                    dmItem.theNumber = Convert.ToInt32(dtList.Rows[i]["theNumber"].ToString());
                    dmItem.theUnit = dtList.Rows[i]["theUnit"].ToString();

                    listResult.Add(dmItem);

                }
            }

            return listResult;

        }




        /// <summary>
        /// 将方案添加到我的计划中
        /// </summary>
        /// <returns>The to my plan.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="planID">Plan I.</param>
        public string AddToMyPlan(int userID, string planID)
        {
            try{

                string strSQL = "insert into User_MyPlan(userID, planID) values ("+userID+", "+planID+")";
             

                int rows = SQLHelper.ExecuteSql(strSQL);
             

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
                return "-1 " + ex.Message;
            }
        }




        /// <summary>
        /// 获取用户添加的健身方案列表
        /// </summary>
        /// <returns>The my plan list.</returns>
        /// <param name="state">值为1表示当前正在进行中的方案，值为0表示之前已经完成了的方案，值为-1表示获取已经删除的方案，值为-2获取全部方案</param>
        public List<DM_MyPlanList> GetMyPlanList(int userID, int state)
        {
            List<DM_MyPlanList> listResult = new List<DM_MyPlanList>();

            string strSQL = "";

            if (state == -2)
            {
                strSQL = "select * from User_MyPlan where userID="+userID+" order by createTime DESC";
            }
            else
            {
                strSQL = "select * from User_MyPlan where userID="+userID+" and state="+state+" order by createTime DESC";
            }

            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            for(int i=0; i<dtList.Rows.Count; i++)
            {
                DM_MyPlanList dmInfo = new DM_MyPlanList();

                dmInfo.ID = dtList.Rows[i]["ID"].ToString();
                dmInfo.userID = Convert.ToInt32(dtList.Rows[i]["userID"].ToString());
                dmInfo.planID = dtList.Rows[i]["planID"].ToString();
                dmInfo.state = Convert.ToInt32(dtList.Rows[i]["state"].ToString());
                dmInfo.counterDays = Convert.ToInt32(dtList.Rows[i]["counterDays"].ToString());

                DateTime dtTime = DateTime.Parse(dtList.Rows[i]["createTime"].ToString());
                dmInfo.createTime = dtTime.Year.ToString()+"年"+dtTime.Month.ToString()+"月"+dtTime.Day.ToString()+"日";


                string strSQLPlanInfo = "select ID,planName,days from Plan_BaseInfo where ID='"+dmInfo.planID+"'";
                DataTable dtPlanInfo = SQLHelper.ExecuteDt(strSQLPlanInfo);

                if(dtPlanInfo.Rows.Count>0)
                {
                    dmInfo.planName = dtPlanInfo.Rows[0]["planName"].ToString();
                    dmInfo.planDays = Convert.ToInt32(dtPlanInfo.Rows[0]["days"].ToString());
                }
                else
                {
                    dmInfo.planName = "";
                    dmInfo.planDays = 0;
                }

              

                listResult.Add(dmInfo);
            }


            return listResult;
        }



        //根据ID获取方案名称
        private string GetPlanName(string planID)
        {
            string strSQL = "select ID,planName from Plan_BaseInfo where ID='"+planID+"'";
            DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

            if(dtInfo.Rows.Count>0)
            {
                return dtInfo.Rows[0]["planName"].ToString();
            }
            else
            {
                return "";
            }
        }














    }
}

