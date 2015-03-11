using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Suwen.ToFit.DAL;
using Suwen.ToFit.DM;

namespace Suwen.ToFit.BLL
{
    public class BLL_OnlineService
    {
        DAL_OnlineService oService = new DAL_OnlineService();

        public BLL_OnlineService()
        {
        }


        /// <summary>
        /// 添加用户反馈
        /// </summary>
        /// <returns>The feedback.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string AddFeedback(DM_Feedback dmInfo)
        {
            return oService.AddFeedback(dmInfo);
        }



        /// <summary>
        /// 获取某位用户的反馈信息列表
        /// </summary>
        /// <returns>The feedback list.</returns>
        /// <param name="userID">大于0则选择指定用户，等于0则表示显示所有用户的反馈信息</param>
        /// <param name="state">0表示客服尚未回复的反馈，1表示已经反馈的，2表示获取全部状态的信息</param>
        public List<DM_Feedback> GetFeedbackList(int userID, int state)
        {
            return oService.GetFeedbackList(userID, state);
        }
    }
}

