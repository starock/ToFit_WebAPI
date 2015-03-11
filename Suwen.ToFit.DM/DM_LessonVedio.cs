using System;

namespace Suwen.ToFit.DM
{
    public class DM_LessonVedio
    {
        public DM_LessonVedio()
        {
        }

        public int ID { get; set; }

        public string title { get; set; }

        public string fileName { get; set; }

        public int albumID { get; set; }

        public int sortID { get; set; }

        public int counterWatch { get; set; }
    }
}

