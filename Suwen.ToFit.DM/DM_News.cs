using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suwen.ToFit.DM
{
    public class DM_News
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// GUID
        /// </summary>		
        public string GUID { get; set; }

        /// <summary>
        /// 文章分类-1健身资讯，2基础知识，3常见问题
        /// </summary>		
        public int classID { get; set; }

        /// <summary>
        /// createTime
        /// </summary>		
        public string createTime { get; set; }

        /// <summary>
        /// title
        /// </summary>		
        public string title { get; set; }

        /// <summary>
        /// 新闻正文
        /// </summary>		
        public string mainBody { get; set; }

        /// <summary>
        /// 缩略图文件名
        /// </summary>		
        public string thumb { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>		
        public int counterReply { get; set; }

        /// <summary>
        /// 浏览计数
        /// </summary>		
        public int counterView { get; set; }

        /// <summary>
        /// state
        /// </summary>		
        public int state { get; set; }

        /// <summary>
        /// state
        /// </summary>      
        public int isMyFav { get; set; }


    }
}