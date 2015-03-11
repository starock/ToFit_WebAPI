using System;

namespace Suwen.ToFit.DM
{
    public class DM_PushMessage
    {
        public DM_PushMessage()
        {
        }

        //DeviceID
        public string deviceID { get; set; }

        //角标数字
        public int badge { get; set; }

        //弹框内容
        public string message { get; set; }

        //透传内容
        public string messageInner { get; set; }

        //声音
        public string sound { get; set; }

        //locKey
        public string locKey { get; set; }

        //loArgs
        public string locArgs { get; set; }

        //launch image
        public string launchImage { get; set; }

        //按钮名称
        public string buttonName { get; set; }

    }
}

