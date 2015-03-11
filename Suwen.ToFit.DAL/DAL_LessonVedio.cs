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
    public class DAL_LessonVedio
    {
        public DAL_LessonVedio()
        {
        }


        /// <summary>
        /// 获取健身教程视频的专辑列表
        /// </summary>
        /// <returns>The album list.</returns>
        public List<DM_LessonVedioAlbum> GetAlbumList()
        {
            List<DM_LessonVedioAlbum> listResult = new List<DM_LessonVedioAlbum>();


            string strSQL = "select * from Vedio_LessonAlbum order by sortID DESC";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_LessonVedioAlbum dmInfo = new DM_LessonVedioAlbum();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.albumName = dtList.Rows[i]["albumName"].ToString();
                    dmInfo.intro = dtList.Rows[i]["intro"].ToString();
                    dmInfo.sortID = Convert.ToInt32(dtList.Rows[i]["sortID"].ToString());
                    dmInfo.forderName = dtList.Rows[i]["forderName"].ToString();
                    dmInfo.thumb = dtList.Rows[i]["thumb"].ToString();
                    dmInfo.sumCounter = Convert.ToInt32(dtList.Rows[i]["sumCounter"].ToString());

                    listResult.Add(dmInfo);

                }
            }

            return listResult;
        }



        /// <summary>
        /// 获取专辑的视频列表
        /// </summary>
        /// <returns>The vedio list.</returns>
        /// <param name="albumID">Album I.</param>
        public List<DM_LessonVedio> GetVedioList(int albumID)
        {
            List<DM_LessonVedio> listResult = new List<DM_LessonVedio>();


            string strSQL = "select * from Vedio_Lesson where albumID="+albumID+" order by sortID DESC ";
            DataTable dtList = SQLHelper.ExecuteDt(strSQL);

            if(dtList.Rows.Count>0)
            {
                for(int i=0; i<dtList.Rows.Count; i++)
                {
                    DM_LessonVedio dmInfo = new DM_LessonVedio();

                    dmInfo.ID = Convert.ToInt32(dtList.Rows[i]["ID"].ToString());
                    dmInfo.title = dtList.Rows[i]["title"].ToString();
                    dmInfo.fileName = dtList.Rows[i]["fileName"].ToString();
                    dmInfo.albumID = Convert.ToInt32(dtList.Rows[i]["albumID"].ToString());
                    dmInfo.sortID = Convert.ToInt32(dtList.Rows[i]["sortID"].ToString());
                    dmInfo.counterWatch = Convert.ToInt32(dtList.Rows[i]["counterWatch"].ToString());

                    listResult.Add(dmInfo);


                }
            }

            return listResult;
        }




        /// <summary>
        /// 增加浏览计数
        /// </summary>
        /// <returns>The view counter.</returns>
        /// <param name="ID">I.</param>
        public void AddViewCounter(int ID)
        {
            string strSQL = "update Vedio_Lesson set counterWatch=counterWatch+1 where ID="+ID;
            SQLHelper.ExecuteSql(strSQL);

            strSQL = "select * from Vedio_Lesson where ID=" + ID;
            DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

            if(dtInfo.Rows.Count>0)
            {
                int albumID = Convert.ToInt32(dtInfo.Rows[0]["albumID"].ToString());

                strSQL = "update Vedio_LessonAlbum set sumCounter=sumCounter+1 where ID="+albumID;
                SQLHelper.ExecuteSql(strSQL);
            }


        }




    }
}

