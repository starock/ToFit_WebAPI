using System;

namespace Suwen.ToFit.DM
{
	public class DM_UserInfo
	{
		public DM_UserInfo ()
		{
		}


		/// <summary>
		/// ID
		/// </summary>		
		private int _id;
		public int ID
		{
			get{ return _id; }
			set{ _id = value; }
		}        
		/// <summary>
		/// 登录名
		/// </summary>		
		private string _loginname;
		public string loginName
		{
			get{ return _loginname; }
			set{ _loginname = value; }
		}        
		/// <summary>
		/// 登录密码
		/// </summary>		
		private string _cipher;
		public string cipher
		{
			get{ return _cipher; }
			set{ _cipher = value; }
		}        
		/// <summary>
		/// realName
		/// </summary>		
		private string _realname;
		public string realName
		{
			get{ return _realname; }
			set{ _realname = value; }
		}        
		/// <summary>
		/// 出生年
		/// </summary>		
		private int _birthyear;
		public int birthYear
		{
			get{ return _birthyear; }
			set{ _birthyear = value; }
		}        
		/// <summary>
		/// sex
		/// </summary>		
		private string _sex;
		public string sex
		{
			get{ return _sex; }
			set{ _sex = value; }
		}        
		/// <summary>
		/// 身高
		/// </summary>		
		private decimal _bodyheight;
		public decimal bodyHeight
		{
			get{ return _bodyheight; }
			set{ _bodyheight = value; }
		}        
		/// <summary>
		/// 体重
		/// </summary>		
		private decimal _bodyweight;
		public decimal bodyWeight
		{
			get{ return _bodyweight; }
			set{ _bodyweight = value; }
		}        
		/// <summary>
		/// 目标体重
		/// </summary>		
		private decimal _targetweight;
		public decimal targetWeight
		{
			get{ return _targetweight; }
			set{ _targetweight = value; }
		}        
		/// <summary>
		/// 日常的锻炼场所（健身房/家里或户外）
		/// </summary>		
		private string _preferenceplace;
		public string preferencePlace
		{
			get{ return _preferenceplace; }
			set{ _preferenceplace = value; }
		}        
		/// <summary>
		/// 健身目的（减肥瘦身/增肌增重/塑形）
		/// </summary>		
		private string _aim;
		public string aim
		{
			get{ return _aim; }
			set{ _aim = value; }
		}        
		/// <summary>
		/// 希望的锻炼强度（效果明显但运动强度大的方案/运动量较小适合大部分人群的方案）-取值：大、小
		/// </summary>		
		private string _intensity;
		public string intensity
		{
			get{ return _intensity; }
			set{ _intensity = value; }
		}        
		/// <summary>
		/// 联系电话
		/// </summary>		
		private string _phone;
		public string phone
		{
			get{ return _phone; }
			set{ _phone = value; }
		}        
		/// <summary>
		/// addressProvince
		/// </summary>		
		private string _addressprovince;
		public string addressProvince
		{
			get{ return _addressprovince; }
			set{ _addressprovince = value; }
		}        
		/// <summary>
		/// addressCity
		/// </summary>		
		private string _addresscity;
		public string addressCity
		{
			get{ return _addresscity; }
			set{ _addresscity = value; }
		}        
		/// <summary>
		/// addressArea
		/// </summary>		
		private string _addressarea;
		public string addressArea
		{
			get{ return _addressarea; }
			set{ _addressarea = value; }
		}        
		/// <summary>
		/// addressDetail
		/// </summary>		
		private string _addressdetail;
		public string addressDetail
		{
			get{ return _addressdetail; }
			set{ _addressdetail = value; }
		}        
		/// <summary>
		/// 备注说明
		/// </summary>		
		private string _remark;
		public string remark
		{
			get{ return _remark; }
			set{ _remark = value; }
		}  
        /// <summary>
        /// GUID
        /// </summary>      
        private string _GUID;
        public string GUID
        {
            get{ return _GUID; }
            set{ _GUID = value; }
        }   


        /// <summary>
        /// 设备ID
        /// </summary>      
        private string _deviceID;
        public string deviceID
        {
            get{ return _deviceID; }
            set{ _deviceID = value; }
        }   



        /// <summary>
        /// 用户使用的手机平台是什么系统
        /// </summary>
        private string _platform;
        public string platform
        {
            get{ return _platform; }
            set{ _platform = value; }
        }  

	}
}

