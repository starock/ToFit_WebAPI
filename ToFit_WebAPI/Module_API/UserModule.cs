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
    public class UserModule : NancyModule
    {


        public UserModule()
        {
            BLL_User oUser = new BLL_User();

            //注册
            Post["User/Reg/"] = param => 
                {
                    DM_UserInfo dmUser = this.Bind<DM_UserInfo>();

                    string result = oUser.CreateUser(dmUser);

                   

                    return "{\"result\":"+result+"}"; 
                };


          


            //登录
            Get["User/Login/{type},{loginName},{password}"] = param => 
                {

                    int result = oUser.Login(param.type, param.loginName, param.password, "iOS");


                    return "{\"result\":"+result+"}"; 
                };


            Get["User/LoginWithDeviceID/{type},{loginName},{password},{platform},{deviceID}"] = param => 
                {

                    int result = oUser.LoginWithDeviceID(param.type, param.loginName, param.password, param.platform, param.deviceID);


                    return "{\"result\":"+result+"}"; 
                };





            //获取用户信息
            Get["User/GetInfo/{userID},{token}"] = param => 
                {

                    DM_UserInfo dmResult = oUser.GetUserInfo(param.userID, param.token);

                    if(dmResult!=null)
                    {
                       
                        return Response.AsJson(dmResult); //JsonConvert.SerializeObject(dmResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }

                };


            //获取用户信息
            Get["User/GetInfoByGUID/{GUID},{token}"] = param => 
                {

                    DM_UserInfo dmResult = oUser.GetInfoByGUID(param.GUID, param.token);

                    if(dmResult!=null)
                    {
                        return JsonConvert.SerializeObject(dmResult);//Response.AsJson(result);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }

                };










            //JObject jResult = JObject.Parse("{\"result\":\""+result+"\"}");
            //jResult.result;// JsonConvert.SerializeObject(jResult);
            //JsonConvert.SerializeObject(dmUser);//"


            Get["User/Reg2/"] = param => 
                {
                    DM_UserInfo dmUser = new DM_UserInfo();

                    dmUser.loginName = "stormer";
                    dmUser.birthYear = 1999;

                    string result = oUser.CreateUser(dmUser);

                    //JObject jResult = JObject.Parse("{\"result\":\""+result+"\"}");


                    return result;//JsonConvert.SerializeObject(jResult);

                   
                };



        }



    }
}

