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
    public class Club_Module :NancyModule
    {
        public Club_Module()
        {
            BLL_Club oClub = new BLL_Club();

            Post["Club/AddInfo/"] = param => 
                {
                    DM_Club dmClub = new DM_Club();

                    dmClub.clubName = Request.Form["clubName"];
                    dmClub.address = Request.Form["address"];
                    dmClub.phone = Request.Form["phone"];
                    dmClub.intro = Request.Form["intro"];
                    dmClub.GUID = Request.Form["GUID"];
                    dmClub.province = Request.Form["province"];
                    dmClub.city = Request.Form["city"];
                    dmClub.area = "";

                    if(dmClub.clubName.Length>0)
                    {
                        return oClub.AddClubInfo(dmClub); 
                    }
                    else
                    {
                        return "0"; 
                    }


                };


            //获取会所列表
            Get["Club/GetClubList/"] = param => 
                {

                    List<DM_Club> listResult = oClub.GetClubList();


                    return Response.AsJson(listResult);
                };






            //网页

            //显示会所信息
            Get["Club/Pages/ShowInfo/{ID}"] = param => 
                {
                    DM_Club dmClub = oClub.GetClubInfo(param.ID);

                    return View["Views/Club/ShowClubInfo.html",dmClub];

                };



            Get["Club/Pages/Add/"] = param => 
                {

                    return View["Views/ACM/Club_AddInfo.html"];
                };

        }


    }
}

