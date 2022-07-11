using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using hua_bbs.Model;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using hua_bbs.DBUtility;

namespace hua_bbs.BLL
{
    /// <summary>
    /// UserService
    /// </summary>
    public partial class UserService
    {
        private readonly hua_bbs.DAL.UserDao dal = new hua_bbs.DAL.UserDao();
        public UserService()
        { 
		}
        #region  BasicMethod



        //检验登录，账号和密码是或否存在
        public bool CheckLogin(int user_id, string user_password)
        {
            User user = dal.GetModel(user_id);
            if (user.password == user_password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public bool Modifyuser(User user)
        //    {
        //    string sql = "UPDATE t_user SET email = @email ,face = @face ,mobile = @mobile ,nickname = @nickname,password = @password ,sex = @sex ,truename = @truename WHERE id = @id";
        //                  //UPDATE t_user SET email = 'abcedegh2', face = 'abcedegh2', mobile = 'abcedegh2', nickname = 'abcedegh2', password = 'abcedegh2', sex = '男', truename = 'abcedegh2' WHERE id = 21
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@email",SqlDbType.VarChar),
        //            new SqlParameter("@face", SqlDbType.VarChar),
        //            new SqlParameter("@mobile",SqlDbType.VarChar),
        //            new SqlParameter("@nickname", SqlDbType.VarChar),
        //            new SqlParameter("@password", SqlDbType.VarChar),
        //            new SqlParameter("@sex", SqlDbType.VarChar),
        //            new SqlParameter("@truename",SqlDbType.VarChar)};

        //    parameters[0].Value = "@email";
        //    parameters[1].Value = "@face";
        //    parameters[2].Value = "@mobile";
        //    parameters[3].Value = "@nickname";
        //    parameters[4].Value = "@password";
        //    parameters[5].Value = "@sex";
        //    parameters[6].Value = "@truename";

        //    object obj = SqlHelper.ExecuteScalar(sql, CommandType.Text, null);

        //    int tablecount = Int32.Parse(obj.ToString());
        //    if (tablecount > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string nickname,int id)
		{
			return dal.Exists(nickname,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(hua_bbs.Model.User model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.User model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string nickname,int id)
		{
			
			return dal.Delete(nickname,id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hua_bbs.Model.User GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public hua_bbs.Model.User GetModelByCache(int id)
		{
			
			string CacheKey = "UserModel-" + id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (hua_bbs.Model.User)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.User> DataTableToList(DataTable dt)
		{
			List<hua_bbs.Model.User> modelList = new List<hua_bbs.Model.User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				hua_bbs.Model.User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}

        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

