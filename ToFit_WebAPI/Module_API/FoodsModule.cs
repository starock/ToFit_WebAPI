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
    public class FoodsModule :NancyModule
    {
        BLL_Foods oFoods = new BLL_Foods();

        public FoodsModule()
        {

            //获取食品名称列表
            Get["/Foods/GetNameList/"] = param =>
                {
                    DataTable dtList = oFoods.GetNameList();


                    return JsonConvert.SerializeObject(dtList);
                };




        }


       


    }
}

