using System;

namespace Suwen.ToFit.DM
{
    public class DM_Feedback
    {
        public DM_Feedback()
        {
        }

        public int ID { get; set; }

        public int userID { get; set; }

        public string title { get; set; }

        public string feedback { get; set; }

        public string createTime { get; set; }

        public string reply { get; set; }


        //服务满意度，取值范围-2~2，默认为0 非常不满意，不满意，一般，满意，很满意
        public int score { get; set; }


        //0尚未处理的反馈，1已经回复处理
        public int state { get; set; }

    }
}

