namespace ToFit_WebAPI
{
    using Nancy;

	using System.Collections.Generic;

	using System.Data;
	using System.Data.SqlClient;


	using Suwen.ToFit.BLL;
	using Suwen.ToFit.DM;

    using Nancy.ModelBinding;
    using Nancy.Extensions;

    //using Suwen.ToFit.Push;


    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            /*
			BLL_LessonNews oNews = new BLL_LessonNews ();

			List<DM_News> listNews = oNews.GetNewsList (1);

			DM_News dmNews = new DM_News();
			dmNews.classID = 1;
			dmNews.title = "ttttt";
			dmNews.mainBody = "ccccc";
			dmNews.createTime = "2015-01-25";
            */         



            Get["/"] = parameters =>
            {


                string GUID = System.Guid.NewGuid().ToString();
                return GUID;//Response.AsJson(dmNews); //View["Lesson/index"]; //Response.AsJson(listNews);
            };


			Get["/Doc"] = _ =>
			{
				return View["Doc/index"]; 
			};




            Post["/PushMessage/"] = _ =>
                {
                    BLL_PushMessage oP = new BLL_PushMessage();

                    DM_PushMessage dmMessage = this.Bind<DM_PushMessage>();

                    string result = oP.apnPush(dmMessage);

                    return "{\"result\":"+result+"}"; 
                };

           

        }
    }
}