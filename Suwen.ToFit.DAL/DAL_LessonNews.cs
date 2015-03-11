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
    public class DAL_LessonNews
    {


        /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <param name="dmNews"></param>
        /// <returns>成功返回1，失败0</returns>
        public int NewsAdd(DM_News dmInfo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Lesson_News(");
            strSql.Append("GUID, classID, title, mainBody, thumb)");
            strSql.Append(" values (");
            strSql.Append("@GUID,"+dmInfo.classID + ",@title, @mainBody,'"+dmInfo.thumb+"')");
            SqlParameter[] parameters = {
                    new SqlParameter("@GUID", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,1000),
                    new SqlParameter("@mainBody", SqlDbType.NVarChar,10000)
                                        };

            parameters[0].Value = dmInfo.GUID;
            parameters[1].Value = dmInfo.title;
            parameters[2].Value = dmInfo.mainBody;

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





        /// <summary>
        /// 获取健康资讯列表
        /// </summary>
        /// <returns></returns>
        DataTable GetNewsList0(int classID)
        {
            string strSQL = "select * from Lesson_News where classID="+classID+" order by ID DESC";
            return SQLHelper.ExecuteDt(strSQL);
        }


		/// <summary>
		/// 获取健康资讯列表
		/// </summary>
		/// <returns></returns>
		public List<DM_News> GetNewsList(int classID)
		{
            string strSQL = "select * from Lesson_News where classID="+classID+" order by ID DESC";
			DataTable dtList = SQLHelper.ExecuteDt(strSQL);

			List<DM_News> listNews = new List<DM_News> ();

			if (dtList.Rows.Count > 0) 
			{
				for(int i=0; i<dtList.Rows.Count; i++)
				{
					DM_News dmInfo = new DM_News ();
                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
					dmInfo.title = dtList.Rows[i]["title"].ToString();
					dmInfo.classID = Convert.ToInt32(dtList.Rows[i]["classID"].ToString());

					DateTime dtTime = DateTime.Parse(dtList.Rows[i]["createTime"].ToString());
					dmInfo.createTime = dtTime.Year.ToString()+"年"+dtTime.Month.ToString()+"月"+dtTime.Day.ToString()+"日";
				
					dmInfo.mainBody = "";//dtList.Rows[i]["mainBody"].ToString();
					dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();
					dmInfo.thumb = dtList.Rows [i] ["thumb"].ToString ();



					listNews.Add(dmInfo);

				}
			}

			return listNews;
		}


        /// <summary>
        /// 获取健康资讯列表 - 用户（加了isMyFav字段，不过处于性能因素不建议使用这个方法）
        /// </summary>
        /// <returns></returns>
        public List<DM_News> GetNewsList_User(int classID, int userID)
        {
            string strSQL = "select * from Lesson_News where classID="+classID+" order by ID DESC";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            List<DM_News> listNews = new List<DM_News> ();

            if (dtList.Rows.Count > 0) 
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_News dmInfo = new DM_News ();
                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.title = dtList.Rows[i]["title"].ToString();
                    dmInfo.classID = Convert.ToInt32(dtList.Rows[i]["classID"].ToString());

                    DateTime dtTime = DateTime.Parse(dtList.Rows[i]["createTime"].ToString());
                    dmInfo.createTime = dtTime.Year.ToString()+"年"+dtTime.Month.ToString()+"月"+dtTime.Day.ToString()+"日";

                    dmInfo.mainBody = "";//dtList.Rows[i]["mainBody"].ToString();
                    dmInfo.GUID = dtList.Rows[i]["GUID"].ToString();
                    dmInfo.thumb = dtList.Rows [i] ["thumb"].ToString ();

                    dmInfo.isMyFav = IsMyFav(userID, dmInfo.ID);

                    listNews.Add(dmInfo);

                }
            }

            return listNews;
        }




        /// <summary>
        /// 获取用户收藏的资讯列表
        /// </summary>
        /// <returns>The fav list.</returns>
        /// <param name="userID">User I.</param>
        public List<DM_News> GetFavList(int userID)
        {

            List<DM_News> listNews = new List<DM_News> ();

            string strSQL = "select * from Fav_News where userID="+userID+" order by ID DESC";
            DataTable dtFav = SQLHelper.ExecuteDt(strSQL);

            if (dtFav.Rows.Count > 0)
            {
                for (int i = 0; i < dtFav.Rows.Count; i++)
                {
                    int newsID = Convert.ToInt32(dtFav.Rows[i]["newsID"].ToString());

                    strSQL = "select * from Lesson_News where ID="+newsID;
                    DataTable dtNews = SQLHelper.ExecuteDt(strSQL);


                    if (dtNews.Rows.Count > 0) 
                    {
                        DM_News dmInfo = new DM_News ();
                        dmInfo.ID = Convert.ToInt32(dtNews.Rows[0]["ID"].ToString());
                        dmInfo.title = dtNews.Rows[0]["title"].ToString();
                        dmInfo.classID = Convert.ToInt32(dtNews.Rows[0]["classID"].ToString());

                        DateTime dtTime = DateTime.Parse(dtNews.Rows[0]["createTime"].ToString());
                        dmInfo.createTime = dtTime.Year.ToString()+"年"+dtTime.Month.ToString()+"月"+dtTime.Day.ToString()+"日";

                        dmInfo.mainBody = "";//dtList.Rows[i]["mainBody"].ToString();
                        dmInfo.GUID = dtNews.Rows[0]["GUID"].ToString();
                        dmInfo.thumb = dtNews.Rows[0]["thumb"].ToString();

                        listNews.Add(dmInfo);
                    }
                }
            }



            return listNews;
        }



        /// <summary>
        /// 获取健康课堂的文章内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DM_News GetNewsDetail(int ID)
        {
            DM_News dmInfo = null;// = new DM_News();

            string strSQL = "select * from Lesson_News where ID="+ID;
            DataTable dtNews = SQLHelper.ExecuteDt(strSQL);

            if(dtNews.Rows.Count>0)
            {
                dmInfo = new DM_News();

                dmInfo.ID = ID;
                dmInfo.GUID = dtNews.Rows[0]["GUID"].ToString();
                dmInfo.title = dtNews.Rows[0]["title"].ToString();
                dmInfo.mainBody = dtNews.Rows[0]["mainBody"].ToString();
                dmInfo.createTime = dtNews.Rows[0]["createTime"].ToString();

                string thumbBig= dtNews.Rows[0]["thumb"].ToString();

                if(thumbBig.Length>0)
                {
                    string fileType = thumbBig.Substring(thumbBig.Length-4);
                    thumbBig = dmInfo.GUID +"b"+ fileType;
                }

                dmInfo.thumb = thumbBig;

                //文章浏览计数加1
                AddViewCounter(ID);
            }


            return dmInfo;
        }


        /// <summary>
        /// 删除指定ID的资讯记录
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveNews(int ID)
        {
            string strSQL = "delete from Lesson_News where ID="+ID;
            SQLHelper.ExecuteSql(strSQL);
        }




        /// <summary>
        /// 增加浏览计数
        /// </summary>
        /// <returns>The view counter.</returns>
        /// <param name="ID">I.</param>
        public void AddViewCounter(int ID)
        {
            string strSQL = "update Lesson_News set counterView=counterView+1 where ID="+ID;
            SQLHelper.ExecuteSql(strSQL);
        }



        /// <summary>
        /// 将资讯加入收藏
        /// </summary>
        /// <returns>The to fav.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="newsID">News I.</param>
        public int AddToFav(int userID, int newsID)
        {
            int result = 0;

            try
            {
                //首先检查是否已经收藏过
                string strSQL = "select * from Fav_News where userID="+userID+" and newsID="+newsID;
                DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

                if(dtInfo.Rows.Count>0)
                {
                    //如果已经存在则删除记录
                    strSQL = "delete from Fav_News where userID="+userID+" and newsID="+newsID;
                    result= SQLHelper.ExecuteSql(strSQL);
                }
                else
                {
                    
                    strSQL = "insert into Fav_News(userID, newsID) values("+userID+","+newsID+")";
                    result= SQLHelper.ExecuteSql(strSQL);
                }


            }
            catch(Exception ex)
            {
                result = -1;
            }

            return result;
        }





        /// <summary>
        /// 检查该用户是否收藏了指定的资讯
        /// </summary>
        /// <returns>返回0表示没有收藏，返回1表示已经收藏</returns>
        /// <param name="userID">User I.</param>
        /// <param name="newsID">News I.</param>
        public int IsMyFav(int userID, int newsID)
        {
            int result = 0;

            try
            {
                //首先检查是否已经收藏过
                string strSQL = "select * from Fav_News where userID="+userID+" and newsID="+newsID;
                DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

                if(dtInfo.Rows.Count>0)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }


            }
            catch(Exception ex)
            {
                result = -1;
            }

            return result;
        }




    




    }
}
