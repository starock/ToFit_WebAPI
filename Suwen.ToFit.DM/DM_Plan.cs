using System;

namespace Suwen.ToFit.DM
{
    public class DM_Plan
    {
        public DM_Plan()
        {
        }

        /// <summary>
        /// ID
        /// </summary>      
        private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }    

        /// <summary>
        /// GUID
        /// </summary>      
        public string GUID { get; set; }


        /// <summary>
        /// 方案名称
        /// </summary>      
        private string _planname;
        public string planName
        {
            get{ return _planname; }
            set{ _planname = value; }
        }        
        /// <summary>
        /// 适用人群- 男、女、男女均可
        /// </summary>      
        private string _type_match;
        public string type_match
        {
            get{ return _type_match; }
            set{ _type_match = value; }
        }        
        /// <summary>
        /// 健身场所-家庭，健身房，户外
        /// </summary>      
        private string _type_place;
        public string type_place
        {
            get{ return _type_place; }
            set{ _type_place = value; }
        }        
        /// <summary>
        /// 计划训练天数
        /// </summary>      
        private int _days;
        public int days
        {
            get{ return _days; }
            set{ _days = value; }
        }        
        /// <summary>
        /// 备注信息
        /// </summary>      
        private string _remark;
        public string remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
        /// <summary>
        /// createTime
        /// </summary>      
        private string _createtime;
        public string createTime
        {
            get{ return _createtime; }
            set{ _createtime = value; }
        }        
        /// <summary>
        /// 排序ID
        /// </summary>      
        private int _sortid;
        public int sortID
        {
            get{ return _sortid; }
            set{ _sortid = value; }
        }        
        /// <summary>
        /// 是否为推荐计划-1是，0否
        /// </summary>      
        private int _isrecommend;
        public int isRecommend
        {
            get{ return _isrecommend; }
            set{ _isrecommend = value; }
        }        
        /// <summary>
        /// 计数器-喜欢
        /// </summary>      
        private int _counterlike;
        public int counterLike
        {
            get{ return _counterlike; }
            set{ _counterlike = value; }
        }        
        /// <summary>
        /// 缩略图
        /// </summary>      
        private string _thumb;
        public string thumb
        {
            get{ return _thumb; }
            set{ _thumb = value; }
        }        
        /// <summary>
        /// 健身部位-整体、局部
        /// </summary>      
        private string _type_part;
        public string type_part
        {
            get{ return _type_part; }
            set{ _type_part = value; }
        }        
        /// <summary>
        /// 运动强度-大，小
        /// </summary>      
        private string _type_intensity;
        public string type_intensity
        {
            get{ return _type_intensity; }
            set{ _type_intensity = value; }
        }      
    }
}

