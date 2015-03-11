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
    public class BLL_Fit
    {
        DAL_Fit oFit = new DAL_Fit();

        public BLL_Fit()
        {
        }


        /// <summary>
        /// 列表只返回ID和运动名称
        /// </summary>
        /// <returns>The fit name list.</returns>
        public DataTable GetFitNameList()
        {
            return oFit.GetFitNameList();
        }


        /// <summary>
        /// 根据ID获取运动信息
        /// </summary>
        /// <returns>The info.</returns>
        /// <param name="ID">I.</param>
        public DM_FitInfo GetInfo(int ID)
        {
            return oFit.GetInfo(ID);
        }



        /// <summary>
        /// 获取相应名称的健身视频/图片
        /// </summary>
        /// <returns>The fit media.</returns>
        /// <param name="fitName">Fit name.</param>
        public int GetMediaType(string fitName)
        {
            return oFit.GetMediaType(fitName);
        }
    }
}

