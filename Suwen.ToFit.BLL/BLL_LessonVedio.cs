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
    public class BLL_LessonVedio
    {
        DAL_LessonVedio oVedio = new DAL_LessonVedio();

        public BLL_LessonVedio()
        {
        }

        /// <summary>
        /// 获取健身教程视频的专辑列表
        /// </summary>
        /// <returns>The album list.</returns>
        public List<DM_LessonVedioAlbum> GetAlbumList()
        {
            return oVedio.GetAlbumList();
        }



        /// <summary>
        /// 获取专辑的视频列表
        /// </summary>
        /// <returns>The vedio list.</returns>
        /// <param name="albumID">Album I.</param>
        public List<DM_LessonVedio> GetVedioList(int albumID)
        {
            return oVedio.GetVedioList(albumID);
        }


        /// <summary>
        /// 增加浏览计数
        /// </summary>
        /// <returns>The view counter.</returns>
        /// <param name="ID">I.</param>
        public void AddViewCounter(int ID)
        {
            oVedio.AddViewCounter(ID);
        }
    }
}

