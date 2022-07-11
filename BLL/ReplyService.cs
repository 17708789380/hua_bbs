using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using hua_bbs.Model;
using hua_bbs.DBUtility;
using System.Collections;

namespace hua_bbs.BLL
{
	/// <summary>
	/// ReplyService
	/// </summary>
	public partial class ReplyService
	{
		private readonly hua_bbs.DAL.ReplyDao dal=new hua_bbs.DAL.ReplyDao();
        private readonly hua_bbs.DAL.TopicDao topicDao = new hua_bbs.DAL.TopicDao();
        private readonly hua_bbs.DAL.UserDao userDao = new hua_bbs.DAL.UserDao();
        private readonly hua_bbs.DAL.SectionDao sectionDao = new hua_bbs.DAL.SectionDao();

        //private readonly hua_bbs.Model.Topic topic  = new hua_bbs.Model.Topic();
        public ReplyService()
		{}
        #region  BasicMethod

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByTid(int id)
        {

            return dal.DeleteByTid(id);
        }

        /// <summary>
        /// 通过主帖的id和页码， 查询回复帖子人的相关信息， 和分页(链接)代码
        /// </summary>
        public ArrayList FindReplyInfoByTopicId(int topicId, int pageNumber)
        {
            //1通过主帖id 查询出 回复帖子id 和 发帖者id 和  回帖者id
            //2通过这个id得到回帖者详细信息 和 回帖的

            int pageCount = 4;

            Topic topic = topicDao.GetModel(topicId);
            topic._topicUser = userDao.GetModel(topic.t_u_id);

            //获得分页显示的代码，DataSet dataSet 看做一个集合（游标）==>得到回帖者的List集合
            DataSet dataSet = dal.GetListByPage("t_t_id = '" + topicId + "'", "publishtime asc", (pageNumber - 1) * pageCount + 1, pageNumber * pageCount);
            //将dataSet 集合转换为列表集合
            List<Reply> replyList = DataTableToList(dataSet.Tables[0]);

            //将回帖人的信息全部封装到reply对象中去
            foreach (Reply reply in replyList)
            {
                User user = userDao.GetModel(reply.t_u_id);
                reply._replyuser = user;   
            }

            //得到总的记录数
            int maxRecord = dal.GetRecordCount("t_t_id = '" + topicId + "'");
            //生成分页的连接
            string pageCode = PageUtil.GenPagination("/topic/TopicDetails.aspx", maxRecord, pageNumber, pageCount, "topicID=" + topicId);
            Section section = sectionDao.GetModel(topic.t_s_id);

            ArrayList mylist = new ArrayList();
            mylist.Add(topic);      //将主贴对象设置到0下标
            mylist.Add(replyList);   //将回帖的集合设置到1下标
            mylist.Add(pageCode);   //将分页的连接设置到2下标
            mylist.Add(section);//将板块信息设置到3下标
            return mylist;
        }

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
		public bool Exists(int t_t_id,int t_u_id,int id)
		{
			return dal.Exists(t_t_id,t_u_id,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(hua_bbs.Model.Reply model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.Reply model)
		{
			return dal.Update(model);
		}
        /// <summary>
		/// 删除数据通过Reply的外键t_t_id删除
		/// </summary>
		public bool DeleteByTopicId(int id) //这是Reply的外键t_t_id删除
        {
            return dal.DeleteByTopicId(id);
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
		public bool Delete(int t_t_id,int t_u_id,int id)
		{
			
			return dal.Delete(t_t_id,t_u_id,id);
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
		public hua_bbs.Model.Reply GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public hua_bbs.Model.Reply GetModelByCache(int id)
		{
			
			string CacheKey = "ReplyModel-" + id;
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
			return (hua_bbs.Model.Reply)objModel;
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
		public List<hua_bbs.Model.Reply> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.Reply> DataTableToList(DataTable dt)
		{
			List<hua_bbs.Model.Reply> modelList = new List<hua_bbs.Model.Reply>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				hua_bbs.Model.Reply model;
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

