using System;

namespace Suwen.ToFit.DM
{
    public class DM_AskState
    {
        public DM_AskState()
        {
        }

        public int ID { get; set; }

        public int userID { get; set; }

        public int trainerID { get; set; }

        public string lastTime { get; set; }

        public string lastWords { get; set; }

        public string GUID { get; set; }

    }
}

