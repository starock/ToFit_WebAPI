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
    public class BLL_Foods
    {
        DAL_Foods oFoods = new DAL_Foods();

        public BLL_Foods()
        {
        }



        /// <summary>
        /// 获取食品名称列表(仅包含ID和名字两个字段)
        /// </summary>
        /// <returns>The name list.</returns>
        public DataTable GetNameList()
        {
            return oFoods.GetNameList();
        }
    }
}

