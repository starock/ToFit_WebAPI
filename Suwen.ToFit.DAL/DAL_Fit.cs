using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;



using Suwen.ToFit.DBConn;
using Suwen.ToFit.DM;



namespace Suwen.ToFit.DAL
{
    public class DAL_Fit
    {

       
        public DAL_Fit()
        {
        }


        /// <summary>
        /// 列表只返回ID和运动名称
        /// </summary>
        /// <returns>The fit name list.</returns>
        public DataTable GetFitNameList()
        {
            string strSQL = "select ID,name from Fit_Info order by name";
            return SQLHelper.ExecuteDt(strSQL);
        }


        /// <summary>
        /// 根据ID获取运动信息
        /// </summary>
        /// <returns>The info.</returns>
        /// <param name="ID">I.</param>
        public DM_FitInfo GetInfo(int ID)
        {
            DM_FitInfo dmResult = null;

            string strSQL = "select * from Fit_Info where ID="+ID;
            DataTable dtInfo = SQLHelper.ExecuteDt(strSQL);

            if(dtInfo.Rows.Count>0)
            {
                dmResult = new DM_FitInfo();

                dmResult.ID = Convert.ToInt32(dtInfo.Rows[0]["ID"].ToString());
                dmResult.name = dtInfo.Rows[0]["name"].ToString();
                dmResult.place = dtInfo.Rows[0]["place"].ToString();
                dmResult.effect = dtInfo.Rows[0]["effect"].ToString();
                dmResult.point = dtInfo.Rows[0]["point"].ToString();
                dmResult.breathe = dtInfo.Rows[0]["breathe"].ToString();
                dmResult.cal = float.Parse(dtInfo.Rows[0]["cal"].ToString());
                dmResult.theUnit = dtInfo.Rows[0]["theUnit"].ToString();

                int iMediaType = GetMediaType(dmResult.name);

                if (iMediaType > 0)
                {
                    switch(iMediaType)
                    {
                        case 1:
                            dmResult.mediaType = "mp4";
                            break;

                        case 2:
                            dmResult.mediaType = "jpg";
                            break;
                    }

                    dmResult.mediaFile = dmResult.name + "." + dmResult.mediaType;
                }
                else
                {
                    dmResult.mediaType = "jpg";
                    dmResult.mediaFile = "default.jpg";
                }
            }

            return dmResult;
        }



        /// <summary>
        /// 获取相应名称的健身视频/图片
        /// </summary>
        /// <returns>0-文件不存在，1-MP4,2-JPG</returns>
        /// <param name="fitName">Fit name.</param>
        public int GetMediaType(string fitName)
        {
            //首先检查在Fit_Vedio表中是否有这个fitName名称的视频记录
            string strSQL = "select * from Fit_Vedio where vedioName='"+fitName+"'";
            DataTable dtResultHas = SQLHelper.ExecuteDt(strSQL);

            if(dtResultHas.Rows.Count>0)
            {
                if (dtResultHas.Rows[0]["fileType"].ToString() == "mp4")
                {
                    return 1;
                }
                else
                {
                    return 2;
                }

            }
            else
            {
                //视频不存在
                return 0;
            }
        }






    }
}

