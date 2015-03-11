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
	public class LessonModule : NancyModule
	{
		public LessonModule ()
		{
			BLL_LessonNews oNews = new BLL_LessonNews ();
            BLL_LessonVedio oVedio = new BLL_LessonVedio();


			//根据classID获取资讯列表
			Get["/Lesson/GetNewsList/{classID}"] = param =>
			{
				List<DM_News> listNews = oNews.GetNewsList (param.classID);


				return Response.AsJson(listNews);
			};


            //根据classID获取资讯列表 - 用户
            Get["/Lesson/GetNewsList_User/{classID},{userID}"] = param =>
                {
                    List<DM_News> listNews = oNews.GetNewsList_User(param.classID,param.userID);


                    return Response.AsJson(listNews);
                };


            //根据classID获取资讯列表 - 用户
            Get["/Lesson/GetFavList/{userID}"] = param =>
                {
                    List<DM_News> listNews = oNews.GetFavList(param.userID);


                    return Response.AsJson(listNews);
                };
            


           
            


            Get["/Lesson/Vedio/GetAlbumList/"] = param =>
                {
                    List<DM_LessonVedioAlbum> listResult = oVedio.GetAlbumList();


                    if(listResult.Count>0)
                    {
                        return Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }

                };


            Get["/Lesson/Vedio/GetVedioList/{albumID}"] = param =>
                {
                    List<DM_LessonVedio> listResult = oVedio.GetVedioList(param.albumID);


                    if(listResult.Count>0)
                    {
                        return Response.AsJson(listResult);
                    }
                    else
                    {
                        return "{\"result\":0}"; 
                    }


                };



            //增加视频浏览计数
            Get["/Lesson/Vedio/AddViewCounter/{ID}"] = param =>
                {
                    oVedio.AddViewCounter(param.ID);

                    return "{\"result\":1}"; 

                };


            //将资讯加入收藏
            Get["/Lesson/News/AddToFav/{userID},{newsID}"] = param =>
                {
                    int result = oNews.AddToFav(param.userID, param.newsID);

                    return "{\"result\":"+result+"}"; 

                };


          
            //检查该用户是否收藏了指定的资讯
            Get["/Lesson/IsMyFav/{userID},{newsID}"] = param =>
                {
                    int result = oNews.IsMyFav(param.userID,param.newsID);


                    return "{\"result\":"+result+"}"; 
                };
            




            //网页-------------------------------------------------------

            //添加方案
            Get["Lesson/Pages/ShowNews/{ID}"] = param => 
                {
                    DM_News dmNews = oNews.GetNewsDetail(param.ID);

                    string newContent = dmNews.mainBody;
                    newContent = newContent.Replace("/attached/image/","http://www.2fit.cn:8100/attached/image/");

                    dmNews.mainBody = newContent;

                    string strThumb = "";

                    if(dmNews.thumb.Length>0)
                    {
                        dmNews.thumb = "<div id='thumb'><img src='http://2fit.cn:8100/img_NewsThumb/"+dmNews.thumb+"' width='100%' ></div>";
                    }


                 

                    return View["Views/Lesson/ShowNews.html",dmNews];
                };



		}
	}
}

