using System;

namespace Suwen.ToFit.DM
{
    public class DM_MyPlanList
    {
        public DM_MyPlanList()
        {
        }

        public string ID { get; set; }

        public int userID { get; set; }

        public string planID { get; set; }

        public string planName { get; set; }

        public int state { get; set; }

        //已经执行了几天
        public int counterDays { get; set; }

        //该方案总共有几天
        public int planDays { get; set; }

        public string createTime { get; set; }

    }
}

