using Nancy;

using System.Collections.Generic;

using System.Text;
using System.IO;

using System.Data;
using System.Data.SqlClient;


using Suwen.ToFit.BLL;
using Suwen.ToFit.DM;

using Starock.Tools;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nancy.ModelBinding;
using Nancy.Extensions;



namespace ToFit_WebAPI
{
    public class OnlineServiceModule :NancyModule
    {
        public OnlineServiceModule()
        {
            BLL_OnlineService oService = new BLL_OnlineService();


            //添加用户反馈
            Post["OnlineService/Feedback/"] = param => 
                {
                    DM_Feedback dmInfo = this.Bind<DM_Feedback>();

                    string result = oService.AddFeedback(dmInfo);

                    if(result=="1")
                    {
                        return "{\"result\":1}"; 
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }

                };


            //获取用户反馈
            Get["OnlineService/Feedback/{userID},{state}"] = param => 
                {

                    List<DM_Club> listResult = oService.GetFeedbackList(param.userID, param.state);

                    if(listResult.Count>0)
                    {
                        return Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }


                };
        }
    }
}

