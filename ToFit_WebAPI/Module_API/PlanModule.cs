using Nancy;

using System;

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
    public class PlanModule :NancyModule
    {
        public PlanModule()
        {
            BLL_Plan oPlan = new BLL_Plan();



            //添加健康方案
            Post["Plan/AddInfo/"] = param => 
                {
                    DM_Plan dmPlan = new DM_Plan();

                    dmPlan.planName = Request.Form["planName"];
                    dmPlan.type_match = Request.Form["typeMatch"];
                    dmPlan.type_place = Request.Form["typePlace"];
                    dmPlan.type_part = Request.Form["typePart"];
                    dmPlan.type_intensity = Request.Form["typeIntensity"];
                    dmPlan.days = Request.Form["days"];
                    dmPlan.remark = Request.Form["remark"];

                    int result = oPlan.Plan_Add(dmPlan);

                    return "{\"result\":"+result+"}"; 

                };


            //添加健康方案
            Post["PlanItem/AddInfo/"] = param => 
                {

                    DM_PlanItem dmPlan = new DM_PlanItem();

                    dmPlan.planID = Request.Form["planID"];
                    dmPlan.itemContent = Request.Form["itemContent"];
                    dmPlan.fitID = Request.Form["fitID"];
                    dmPlan.theGroup = Request.Form["theGroup"];
                    dmPlan.theNumber = Request.Form["theNumber"];
                    dmPlan.theUnit = Request.Form["theUnit"];
            

                    string result = oPlan.Plan_AddItem(dmPlan);

                    return "{\"result\":"+result+"}"; 


                };



            //获取健康方案列表
            Get["Plan/GetPlanList/"] = param => 
                {
                    List<DM_Plan> listResult = oPlan.GetPlanList();

                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };


            //列表只返回方案名称和ID
            Get["Plan/GetPlanNameList/"] = param => 
                {
                    DataTable dtList = oPlan.GetPlanNameList();

                    if(dtList.Rows.Count>0)
                    {
                        return JsonConvert.SerializeObject(dtList);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };


            //获取方案具体内容
            Get["Plan/GetPlanItems/{planID}"] = param => 
                {
                    List<DM_PlanItem> listResult = oPlan.GetPlanItems(param.planID);

                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };



            //将方案添加到我的计划中
            Get["Plan/AddToMyPlan/{userID},{planID}"] = param => 
                {
                    string result = oPlan.AddToMyPlan(param.userID, param.planID);

                    if(result == "1")
                    {
                        return "{\"result\":"+result+"}"; 
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };


            //获取用户添加的健身方案列表
            Get["Plan/GetMyPlanList/{userID},{state}"] = param => 
                {
                    List<DM_MyPlanList> listResult = oPlan.GetMyPlanList(param.userID, param.state);

                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };










            //网页-------------------------------------------------------

            //添加方案
            Get["Plan/Pages/AddInfo/"] = param => 
                {
                    return View["Views/ACM/Plan_AddInfo.html"];
                };

            //添加方案详情
            Get["Plan/Pages/AddItem/"] = param => 
                {
                    return View["Views/ACM/PlanItem_AddInfo.html"];
                };


        }


    }
}

