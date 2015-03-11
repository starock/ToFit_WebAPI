using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Suwen.ToFit.DAL;
using Suwen.ToFit.DM;

using Newtonsoft.Json;



namespace Suwen.ToFit.BLL
{
    public class BLL_LessonNews
    {
        DAL_LessonNews oDAL = new DAL_LessonNews();


         /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <param name="dmNews"></param>
        /// <returns>成功返回1，失败0</returns>
        public int NewsAdd(DM_News dmInfo)
        {
            return oDAL.NewsAdd(dmInfo);
        }





		/// <summary>
		/// 获取健康资讯列表
		/// </summary>
		/// <returns></returns>
		public List<DM_News> GetNewsList(int classID)
		{
			return oDAL.GetNewsList (classID);
		}


        /// <summary>
        /// 获取健康资讯列表 - 用户（加了isMyFav字段，不过处于性能因素不建议使用这个方法）
        /// </summary>
        /// <returns></returns>
        public List<DM_News> GetNewsList_User(int classID, int userID)
        {
            return oDAL.GetNewsList_User(classID, userID);
        }



         /// <summary>
        /// 获取健康课堂的文章内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DM_News GetNewsDetail(int ID)
        {
            return oDAL.GetNewsDetail(ID);
        }


        /// <summary>
        /// 删除指定ID的资讯记录
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveNews(int ID)
        {
            oDAL.RemoveNews(ID);
        }


        /// <summary>
        /// 将资讯加入收藏
        /// </summary>
        /// <returns>The to fav.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="newsID">News I.</param>
        public int AddToFav(int userID, int newsID)
        {
            return oDAL.AddToFav(userID, newsID);
        }



        /// <summary>
        /// 检查该用户是否收藏了指定的资讯
        /// </summary>
        /// <returns>返回0表示没有收藏，返回1表示已经收藏</returns>
        /// <param name="userID">User I.</param>
        /// <param name="newsID">News I.</param>
        public int IsMyFav(int userID, int newsID)
        {
            return oDAL.IsMyFav(userID, newsID);
        }



        /// <summary>
        /// 获取用户收藏的资讯列表
        /// </summary>
        /// <returns>The fav list.</returns>
        /// <param name="userID">User I.</param>
        public List<DM_News> GetFavList(int userID)
        {
            return oDAL.GetFavList(userID);
        }

    }
}
