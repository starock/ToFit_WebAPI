using System;

namespace Suwen.ToFit.DM
{
    public class DM_PlanItem
    {
        public DM_PlanItem()
        {
        }

        /// <summary>
        /// ID
        /// </summary>      
        private Guid _id;
        public Guid ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
        /// <summary>
        /// 计划任务的ID-对应Plan_BaseInfo表的ID
        /// </summary>      
        private int _planid;
        public int planID
        {
            get{ return _planid; }
            set{ _planid = value; }
        }        
        /// <summary>
        /// 健身任务的内容
        /// </summary>      
        private string _itemContent;
        public string itemContent
        {
            get{ return _itemContent; }
            set{ _itemContent = value; }
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
        /// 运动信息的ID - 对应Fit_Info表
        /// </summary>      
        private int _fitID;
        public int fitID
        {
            get{ return _fitID; }
            set{ _fitID = value; }
        } 

        /// <summary>
        /// 分组-0为不分组
        /// </summary>      
        private int _theGroup;
        public int theGroup
        {
            get{ return _theGroup; }
            set{ _theGroup = value; }
        }   

        /// <summary>
        /// 数量
        /// </summary>      
        private int _theNumber;
        public int theNumber
        {
            get{ return _theNumber; }
            set{ _theNumber = value; }
        }   

        /// <summary>
        /// 单位-个，分钟，RM
        /// </summary>      
        private string _theUnit;
        public string theUnit
        {
            get{ return _theUnit; }
            set{ _theUnit = value; }
        }   
    }
}

