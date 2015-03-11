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
    public class BLL_Trainer
    {
        DAL_Trainer oTrainer = new DAL_Trainer();

        public BLL_Trainer()
        {
        }


        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <returns>The trainer list.</returns>
        public DataTable GetTrainerList()
        {
            return oTrainer.GetTrainerList();
        }


        /// <summary>
        /// 获取教练详细介绍
        /// </summary>
        /// <returns>The trainer info.</returns>
        /// <param name="ID">I.</param>
        public DM_Trainer GetTrainerInfo(int ID)
        {
            return oTrainer.GetTrainerInfo(ID);
        }


        /// <summary>
        /// 根据ID获取教练名字
        /// </summary>
        /// <returns>The trainer name.</returns>
        /// <param name="ID">I.</param>
        public string GetTrainerName(int ID)
        {
            return oTrainer.GetTrainerName(ID);
        }


        /// <summary>
        /// 添加问教练的记录
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="dmInfo">dmInfo.type值为0表示留言来自用户，值为1表示来自教练</param>
        public string Ask_Add(DM_Ask dmInfo)
        {
            return oTrainer.Ask_Add(dmInfo);
        }



        /// <summary>
        /// 获取等待回复的问题列表
        /// </summary>
        /// <returns>The ask waitting.</returns>
        public List<DM_AskState> GetAskWaiting()
        {
            return oTrainer.GetAskWaiting();
        }


        /// <summary>
        /// 获取用户的提问列表
        /// </summary>
        /// <returns>The my ask list.</returns>
        public List<DM_AskLast> GetMyAskList(int userID)
        {
            return oTrainer.GetMyAskList(userID);
        }

        /// <summary>
        /// 获取用户与教练的对话列表
        /// </summary>
        /// <returns>The ask record.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="trainerID">Trainer I.</param>
        public List<DM_Ask> GetAskRecord(int userID, int trainerID)
        {
            return oTrainer.GetAskRecord(userID, trainerID);
        }


        /// <summary>
        /// 根据GUID来获取对话列表
        /// </summary>
        /// <returns>The ask record.</returns>
        /// <param name="GUID">GUI.</param>
        public List<DM_Ask> GetAskRecord_ByGUID(string GUID)
        {
            return oTrainer.GetAskRecord_ByGUID(GUID);
        }



        /// <summary>
        /// 教练回复
        /// </summary>
        /// <param name="GUID">GUI.</param>
        /// <param name="words">Words.</param>
        public string Reply(string GUID, string words)
        {
            return oTrainer.Reply(GUID, words);
        }
    }
}

