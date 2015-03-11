using Nancy;

using System.Collections.Generic;

using System.Text;


using System.Data;
using System.Data.SqlClient;


using Suwen.ToFit.BLL;
using Suwen.ToFit.DM;

using Starock.Tools;

using Newtonsoft.Json;

using Nancy.ModelBinding;



namespace ToFit_WebAPI
{
    public class FitModule :NancyModule
    {
        public FitModule()
        {
            BLL_Fit oFit = new BLL_Fit();

            //列表只返回方案名称和ID
            Get["Fit/GetNameList/"] = param => 
                {
                    DataTable dtList = oFit.GetFitNameList();

                    if(dtList.Rows.Count>0)
                    {
                        return JsonConvert.SerializeObject(dtList);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };


            //根据ID获取运动信息
            Get["Fit/GetInfo/{ID}"] = param => 
                {
                    DM_FitInfo dmResult = oFit.GetInfo(param.ID);

                    if(dmResult!=null)
                    {
                        return JsonConvert.SerializeObject(dmResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };

        }
    }
}

