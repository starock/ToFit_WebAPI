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
    public class TrainerModule : NancyModule
    {
        public TrainerModule()
        {
            BLL_Trainer oTrainer = new BLL_Trainer();

            //获取教练列表
            Get["Trainer/GetTrainerList/"] = param => 
                {

                    DataTable dtResult = oTrainer.GetTrainerList();


                    return JsonConvert.SerializeObject(dtResult); //Response.AsJson(dtResult);
                };


            //添加问教练的记录
            Post["Trainer/AddAskInfo/"] = param => 
                {

                    DM_Ask dmInfo = this.Bind<DM_Ask>();

                    string result = oTrainer.Ask_Add(dmInfo);

                    return "{\"result\":"+result+"}"; 
                };


            //获取等待回复的聊天列表
            Get["Trainer/GetAskWaiting/"] = param => 
                {

                    List<DM_AskState> listResult = oTrainer.GetAskWaiting();


                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult); //Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };


            //获取聊天列表
            Get["Trainer/GetAskRecord/{userID},{trainerID}"] = param => 
                {

                    List<DM_Ask> listResult = oTrainer.GetAskRecord(param.userid, param.trainerID);


                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult); //Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };



            //根据GUID获取聊天列表
            Get["Trainer/GetAskRecordByGUID/{GUID}"] = param => 
                {

                    List<DM_Ask> listResult = oTrainer.GetAskRecord_ByGUID(param.GUID);


                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult); //Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }
                };



            //教练回复
            Post["Trainer/Reply/"] = param => 
                {
                    DM_Ask dmInfo = this.Bind<DM_Ask>();


                    string result = oTrainer.Reply(dmInfo.GUID, dmInfo.words);

                    if(result=="1")
                    {
                        BLL_User oUser = new BLL_User();
                        BLL_PushMessage oPush = new BLL_PushMessage();
                       
                        string trainerName = "";

                        DM_PhoneInfo dmPhone = oUser.GetUserPhoneInfo(dmInfo.userID);

                        trainerName = oTrainer.GetTrainerName( dmInfo.trainerID);

                        DM_PushMessage dmMessage = new DM_PushMessage();

                        dmMessage.deviceID = dmPhone.deviceID;
                        dmMessage.badge = 1;

                       
                        if(trainerName.Length>0)
                        {
                            dmMessage.message = "教练-"+trainerName+" 刚刚对您的问题做出了答复.";
                        }
                        else
                        {
                            dmMessage.message = "有教练刚刚对您的问题做出了答复.";
                        }


                        if(dmPhone.platform=="iOS")
                        {
                            oPush.apnPush(dmMessage);
                        }
                        else
                        {
                            oPush.PushMessageToSingle(dmMessage);
                        }

                    }

                    return "{\"result\":"+result+"}"; 
                };


            Get["Trainer/GetMyAskList/{userID}"] = param => 
                {

                    List<DM_AskLast> listResult  = oTrainer.GetMyAskList(param.userID);

                    if(listResult.Count>0)
                    {
                        return JsonConvert.SerializeObject(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }

                     
                };




            ///////////////////////////////////////////////////////////////////////////////////////////

            Get["Trainer/Pages/GetAskWaiting/"] = param => 
               {
                    return View["ACM/Ask_Waiting.html"]; 
               };


            Get["Trainer/Pages/GetAskRecordByGUID/{GUID}"] = param => 
                {
                    return View["ACM/Ask_Chat.html", param.GUID]; 
                };

        }
    }
}

