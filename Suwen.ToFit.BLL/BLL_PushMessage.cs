using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.ProtocolBuffers;
using com.gexin.rp.sdk.dto;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using System.Net;

using Suwen.ToFit.DM;

namespace Suwen.ToFit.BLL
{
    public class BLL_PushMessage
    {
        //参数设置 <-----参数需要重新设置----->
        private static String APPID = "EcOGpQ3OUX7ewf3YXyEui5";                     //您应用的AppId
        private static String APPKEY = "CtDmOz6Xke86RgjA8GK1w4";                    //您应用的AppKey
        private static String MASTERSECRET = "6QxjVKwl3z53ZyDX3TFDr9";              //您应用的MasterSecret 

        private static String CLIENTID = "838138138802548603";        //您获取的clientID 838138138802548603
        //private static String CLIENTID1 = "";
        private static String ALIAS = "请输入别名";
        private static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";    //HOST：OpenService接口地址

        private static String DeivceToken = "";//"e19bcf4f22ba55fbd2b58f051440bd0fe6715f73c446bd57f483a03f7d61297e";  

       
        //"68869b7b3dbb2e08001b68236afa70817b0c8d1c885a2c7ce6e308b450f33012"; //fan
        //"e19bcf4f22ba55fbd2b58f051440bd0fe6715f73c446bd57f483a03f7d61297e"; //stormer
        //"e373e20de85f28a146b98e3a9d299c15"//stormer android

        public BLL_PushMessage()
        {
        }


        public void SendToDeviceID()
        {

        }






        public static void bindAlias() {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.bindAlias(APPID, ALIAS, CLIENTID);
            //System.Console.WriteLine(ret);
        }

        public static void queryAlias()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.queryAlias(APPID, CLIENTID);
            //System.Console.WriteLine(ret);
        }

        public static void queryClientId()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.queryClientId(APPID, ALIAS);
            //System.Console.WriteLine(ret);
        }

        public static void aliasUnBind()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.unBindAlias(APPID, ALIAS, CLIENTID);
            //System.Console.WriteLine(ret);
        }

        public static void bindAliasAll()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            List<com.igetui.api.openservice.igetui.Target> Lcids = new List<com.igetui.api.openservice.igetui.Target>();
            com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
            target.clientId = CLIENTID;
            target.alias = ALIAS;

            /*com.igetui.api.openservice.igetui.Target target1 = new com.igetui.api.openservice.igetui.Target();
            target1.clientId = CLIENTID1;
            target1.alias = ALIAS;*/


            Lcids.Add(target);
            //Lcids.Add(target1);

            String ret = push.bindAlias(APPID, Lcids);
            //System.Console.WriteLine(ret);
        }

        public static void aliasUnBindAll()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.unBindAliasAll(APPID, ALIAS);
            //System.Console.WriteLine(ret);
        }

        public static void setTag() {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            List<String>  list=new List<String>();
            list.Add("标签1");
            list.Add("标签2");
            String ret =push.setClientTag(APPID, CLIENTID, list);
            //System.Console.WriteLine(ret);
        }


        public static void taskStop()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            Boolean result = push.stop("OSA-0226_uuGr1x4Yp26TtLqnFq1984");
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine(result);
        }






        public string apnPush(DM_PushMessage dmMessage)
        {
            string result = "";

            try
            {
                dmMessage.buttonName = dmMessage.buttonName == null?"OK":dmMessage.buttonName;
                dmMessage.badge = dmMessage.badge == null?1:dmMessage.badge;
                dmMessage.message = dmMessage.message == null?"您有新消息":dmMessage.message;
                dmMessage.sound = dmMessage.sound == null?"":dmMessage.sound;
                dmMessage.messageInner = dmMessage.messageInner == null?"":dmMessage.messageInner;
                dmMessage.locKey = dmMessage.locKey == null?"":dmMessage.locKey;
                dmMessage.locArgs = dmMessage.locArgs == null?"":dmMessage.locArgs;
                dmMessage.launchImage = dmMessage.locArgs == null?"":dmMessage.launchImage;


                APNTemplate template = new APNTemplate();

                template.setPushInfo(dmMessage.buttonName, dmMessage.badge, dmMessage.message, dmMessage.sound, dmMessage.messageInner, dmMessage.locKey, dmMessage.locArgs, dmMessage.launchImage);

                //template.setPushInfo("OK", 1, "有教练给您做出了答复", "MM", "教练回复", "", "", "");
                //template.setPushInfo("OK", badge, "message", "声音", "教练回复", "loc-key", "loc-args", "launch-image");
                IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

                /*单个用户推送接口*/
                /*SingleMessage Singlemessage = new SingleMessage();
                Singlemessage.Data = template;
                String pushResult = push.pushAPNMessageToSingle(APPID, DeivceToken, Singlemessage);
                Console.Out.WriteLine(pushResult);*/

                /*多个用户推送接口*/
                ListMessage listmessage = new ListMessage();
                listmessage.Data = template;
                String contentId = push.getAPNContentId(APPID, listmessage);
                //Console.Out.WriteLine(contentId);
                List<String> devicetokenlist = new List<string>();

                //添加接收推送的设备ID
                devicetokenlist.Add(dmMessage.deviceID); 

                String ret = push.pushAPNMessageToList(APPID, contentId, devicetokenlist);

                //Console.Out.WriteLine(ret);

                result = "1";

            
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }








        //PushMessageToSingle接口测试代码
        public string PushMessageToSingle(DM_PushMessage dmMessage)
        {
            string result = "";

            try
            {
                // 推送主类
                IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

                /*消息模版：
                    1.TransmissionTemplate:透传模板
                    2.LinkTemplate:通知链接模板
                    3.NotificationTemplate：通知透传模板
                    4.NotyPopLoadTemplate：通知弹框下载模板
                 */


                dmMessage.buttonName = dmMessage.buttonName == null?"OK":dmMessage.buttonName;
                dmMessage.badge = dmMessage.badge == null?1:dmMessage.badge;
                dmMessage.message = dmMessage.message == null?"您有新消息":dmMessage.message;
                dmMessage.sound = dmMessage.sound == null?"":dmMessage.sound;
                dmMessage.messageInner = dmMessage.messageInner == null?"":dmMessage.messageInner;
                dmMessage.locKey = dmMessage.locKey == null?"":dmMessage.locKey;
                dmMessage.locArgs = dmMessage.locArgs == null?"":dmMessage.locArgs;
                dmMessage.launchImage = dmMessage.locArgs == null?"":dmMessage.launchImage;


                NotificationTemplate template = new  NotificationTemplate();

              
                template.AppId = APPID;
                template.AppKey = APPKEY;
                template.Title = "快健身";         //通知栏标题
                template.Text = dmMessage.message;          //通知栏内容
                template.Logo = "";               //通知栏显示本地图片
                template.LogoURL = "";                    //通知栏显示网络图标

                template.TransmissionType = "1";          //应用启动类型，1：强制应用启动  2：等待应用启动
                template.TransmissionContent = "";   //透传内容
                //iOS推送需要的pushInfo字段
                //template.setPushInfo(actionLocKey, badge, message, sound, payload, locKey, locArgs, launchImage);

                template.IsRing = true;                //接收到消息是否响铃，true：响铃 false：不响铃
                template.IsVibrate = true;               //接收到消息是否震动，true：震动 false：不震动
                template.IsClearable = true;             //接收到消息是否可清除，true：可清除 false：不可清除


         
                //template.setPushInfo("", dmMessage.badge, dmMessage.message, dmMessage.sound, dmMessage.messageInner, dmMessage.locKey, dmMessage.locArgs, dmMessage.launchImage);

                //NotificationTemplate template =  NotificationTemplateDemo();
                //LinkTemplate template = LinkTemplateDemo();
                //NotyPopLoadTemplate template = NotyPopLoadTemplateDemo();


                // 单推消息模型
                SingleMessage message = new SingleMessage();
                message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
                message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
                message.Data = template;
                //message.PushNetWorkType = 0;        //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为非WIFI环境

                com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                target.appId = APPID;
                target.clientId = dmMessage.deviceID;
                //target.alias = ALIAS;


                String pushResult = push.pushMessageToSingle(message, target);

                result = "1";

            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine("----------------服务端返回结果：" + pushResult);

            return result;
        }








        //PushMessageToList接口测试代码
        private static void PushMessageToList()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            ListMessage message = new ListMessage();
            /*消息模版：
                 1.TransmissionTemplate:透传功能模板
                 2.LinkTemplate:通知打开链接功能模板
                 3.NotificationTemplate：通知透传功能模板
                 4.NotyPopLoadTemplate：通知弹框下载功能模板
             */

            TransmissionTemplate template =  TransmissionTemplateDemo();
            //NotificationTemplate template =  NotificationTemplateDemo();
            //LinkTemplate template = LinkTemplateDemo();
            //NotyPopLoadTemplate template = NotyPopLoadTemplateDemo();

            message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
            message.Data = template;
            //message.PushNetWorkType = 0;             //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为非WIFI环境

            //设置接收者
            List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
            com.igetui.api.openservice.igetui.Target target1 = new com.igetui.api.openservice.igetui.Target();
            target1.appId = APPID;
            target1.clientId = CLIENTID;

            // 如需要，可以设置多个接收者
            //com.igetui.api.openservice.igetui.Target target2 = new com.igetui.api.openservice.igetui.Target();
            //target2.AppId = APPID;
            //target2.ClientId = "ddf730f6cabfa02ebabf06e0c7fc8da0";

            targetList.Add(target1);
            //targetList.Add(target2);

            String contentId = push.getContentId(message, null);
            String pushResult = push.pushMessageToList(contentId, targetList);
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine("服务端返回结果:" + pushResult);
        }




        //pushMessageToApp接口测试代码
        private static void pushMessageToApp()
        {

            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            AppMessage message = new AppMessage();
            /*消息模版：
                1.TransmissionTemplate:透传模板
                2.LinkTemplate:通知链接模板
                3.NotificationTemplate：通知透传模板
                4.NotyPopLoadTemplate：通知弹框下载模板
             */

            //TransmissionTemplate template =  TransmissionTemplateDemo();
            NotificationTemplate template =  NotificationTemplateDemo();
            //LinkTemplate template = LinkTemplateDemo();
            //NotyPopLoadTemplate template = NotyPopLoadTemplateDemo();

            message.IsOffline = false;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
            message.Data = template;
            //message.PushNetWorkType = 0;            //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为非WIFI环境

            List<String> appIdList = new List<string>();
            appIdList.Add(APPID);

            List<String> phoneTypeList = new List<string>();    //通知接收者的手机操作系统类型
            //phoneTypeList.Add("ANDROID");
            //phoneTypeList.Add("IOS");

            List<String> provinceList = new List<string>();     //通知接收者所在省份
            //provinceList.Add("浙江");
            //provinceList.Add("上海");
            //provinceList.Add("北京");

            List<String> tagList = new List<string>();
            tagList.Add("456");

            message.AppIdList = appIdList;
            message.PhoneTypeList = phoneTypeList;
            message.ProvinceList = provinceList;
            message.TagList = tagList;


            String pushResult = push.pushMessageToApp(message, "toAPP任务别名");
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine("服务端返回结果：" + pushResult);
        }


        /*
         * 
         * 所有推送接口均支持四个消息模板，依次为透传模板，通知透传模板，通知链接模板，通知弹框下载模板
         * 注：IOS离线推送需通过APN进行转发，需填写pushInfo字段，目前仅不支持通知弹框下载功能
         *
         */
        //透传模板动作内容
        public  static TransmissionTemplate TransmissionTemplateDemo()
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            template.TransmissionType = "1";            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionContent = "请输入透传内容";  //透传内容
            //iOS推送需要的pushInfo字段
            //template.setPushInfo(actionLocKey, badge, message, sound, payload, locKey, locArgs, launchImage);
            //template.setPushInfo("1", 4, "2", "", "", "", "", "");
            return template;
        }


        //通知透传模板动作内容
        public static NotificationTemplate NotificationTemplateDemo()
        {
            NotificationTemplate template = new NotificationTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            template.Title = "请填写通知标题";         //通知栏标题
            template.Text = "请填写通知内容";          //通知栏内容
            template.Logo = "";               //通知栏显示本地图片
            template.LogoURL = "";                    //通知栏显示网络图标

            template.TransmissionType = "1";          //应用启动类型，1：强制应用启动  2：等待应用启动
            template.TransmissionContent = "请填写透传内容";   //透传内容
            //iOS推送需要的pushInfo字段
            //template.setPushInfo(actionLocKey, badge, message, sound, payload, locKey, locArgs, launchImage);

            template.IsRing = true;                //接收到消息是否响铃，true：响铃 false：不响铃
            template.IsVibrate = true;               //接收到消息是否震动，true：震动 false：不震动
            template.IsClearable = true;             //接收到消息是否可清除，true：可清除 false：不可清除
            return template;
        }


        //通知链接动作内容
        public static LinkTemplate LinkTemplateDemo()
        {
            LinkTemplate template =new LinkTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            template.Title = "请填写通知标题";         //通知栏标题
            template.Text = "请填写通知内容";          //通知栏内容
            template.Logo = "";               //通知栏显示本地图片
            template.LogoURL = "";  //通知栏显示网络图标，如无法读取，则显示本地默认图标，可为空
            template.Url="http://www.baidu.com";      //打开的链接地址

            //iOS推送需要的pushInfo字段
            //template.setPushInfo(actionLocKey, badge, message, sound, payload, locKey, locArgs, launchImage);

            template.IsRing = true;                 //接收到消息是否响铃，true：响铃 false：不响铃
            template.IsVibrate = true;               //接收到消息是否震动，true：震动 false：不震动
            template.IsClearable = true;             //接收到消息是否可清除，true：可清除 false：不可清除

            return template;
        }

        //通知弹框下载模板动作内容
        public static NotyPopLoadTemplate NotyPopLoadTemplateDemo()
        {
            NotyPopLoadTemplate template = new NotyPopLoadTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            template.NotyTitle = "请填写通知标题";     //通知栏标题
            template.NotyContent = "请填写通知内容";   //通知栏内容
            template.NotyIcon = "icon.png";           //通知栏显示本地图片
            template.LogoURL = "http://www-igexin.qiniudn.com/wp-content/uploads/2013/08/logo_getui1.png";                    //通知栏显示网络图标

            template.PopTitle = "弹框标题";             //弹框显示标题
            template.PopContent = "弹框内容";           //弹框显示内容
            template.PopImage = "";                     //弹框显示图片
            template.PopButton1 = "下载";               //弹框左边按钮显示文本
            template.PopButton2 = "取消";               //弹框右边按钮显示文本

            template.LoadTitle = "下载标题";            //通知栏显示下载标题
            template.LoadIcon = "file://push.png";      //通知栏显示下载图标,可为空
            template.LoadUrl = "http://www.appchina.com/market/d/425201/cop.baidu_0/com.gexin.im.apk";//下载地址，不可为空

            template.IsActived = true;                  //应用安装完成后，是否自动启动
            template.IsAutoInstall = true;              //下载应用完成后，是否弹出安装界面，true：弹出安装界面，false：手动点击弹出安装界面

            template.IsBelled = true;                 //接收到消息是否响铃，true：响铃 false：不响铃
            template.IsVibrationed = true;               //接收到消息是否震动，true：震动 false：不震动
            template.IsCleared = true;             //接收到消息是否可清除，true：可清除 false：不可清除
            return template;
        }

        public static void getUserStatus()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            String ret = push.getClientIdStatus(APPID, CLIENTID);
            //System.Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("用户状态:" + ret);
        }


    }
}

