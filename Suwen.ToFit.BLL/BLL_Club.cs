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
    public class BLL_Club
    {
        DAL_Club oClub = new DAL_Club();


        public BLL_Club()
        {
        }


        /// <summary>
        /// 添加会所信息
        /// </summary>
        /// <returns>The club info.</returns>
        /// <param name="dmInfo">Dm info.</param>
        public string AddClubInfo(DM_Club dmInfo)
        {
            return oClub.AddClubInfo(dmInfo);
        }



        /// <summary>
        /// 获取健身会所列表
        /// </summary>
        /// <returns>The club list.</returns>
        public List<DM_Club> GetClubList()
        {
            return oClub.GetClubList();
        }



        /// <summary>
        /// 根据ID获取会所信息
        /// </summary>
        /// <returns>The club info.</returns>
        /// <param name="ID">I.</param>
        public DM_Club GetClubInfo(int ID)
        {
            return oClub.GetClubInfo(ID);
        }
    }
}

