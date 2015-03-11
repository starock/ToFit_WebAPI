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
    public class BLL_Plan
    {
        DAL_Plan oPlan = new DAL_Plan();

        public BLL_Plan()
        {
        }


        /// <summary>
        /// 添加健身方案信息
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public int Plan_Add(DM_Plan dmInfo)
        {
            return oPlan.Plan_Add(dmInfo);
        }



        /// <summary>
        /// 添加健身方案-项目内容
        /// </summary>
        /// <returns>The add item.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string Plan_AddItem(DM_PlanItem dmInfo)
        {
            return oPlan.Plan_AddItem(dmInfo);
        }


        /// <summary>
        /// 获取健康方案列表
        /// </summary>
        /// <returns>The plan list.</returns>
        public List<DM_Plan> GetPlanList()
        {
            return oPlan.GetPlanList();
        }


        /// <summary>
        /// 列表只返回方案名称和ID
        /// </summary>
        /// <returns>The plan name list.</returns>
        public DataTable GetPlanNameList()
        {
            return oPlan.GetPlanNameList();
        }


        /// <summary>
        /// 根据方案ID返回属于它的方案具体内容项
        /// </summary>
        /// <returns>The plan items.</returns>
        /// <param name="planID">Plan I.</param>
        public List<DM_PlanItem>GetPlanItems(int planID)
        {
            return oPlan.GetPlanItems(planID);
        }


       
        /// <summary>
        /// 将方案添加到我的计划中
        /// </summary>
        /// <returns>The to my plan.</returns>
        /// <param name="userID">User I.</param>
        /// <param name="planID">Plan I.</param>
        public string AddToMyPlan(int userID, string planID)
        {
            return oPlan.AddToMyPlan(userID, planID);
        }




        /// <summary>
        /// 获取用户添加的健身方案列表
        /// </summary>
        /// <returns>The my plan list.</returns>
        /// <param name="state">值为1表示当前正在进行中的方案，值为0表示之前已经完成了的方案，值为-1表示获取所有方案</param>
        public List<DM_MyPlanList> GetMyPlanList(int userID, int state)
        {
            return oPlan.GetMyPlanList(userID,state);
        }











    }
}

