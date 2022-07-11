using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using hua_bbs.Model;
using hua_bbs.DAL;

namespace hua_bbs.BLL
{
	/// <summary>
	/// ZoneService
	/// </summary>
	public partial class ZoneService
	{
		private readonly hua_bbs.DAL.ZoneDao dal=new hua_bbs.DAL.ZoneDao();
        private readonly hua_bbs.DAL.SectionDao sectionDao = new hua_bbs.DAL.SectionDao();
        private readonly SectionService sectionService = new SectionService();
        private readonly TopicService topicService = new TopicService();
        private readonly ReplyService replyService = new ReplyService();
        private readonly TopicDao topicDao = new TopicDao();
        public int pageCount = 5;

        public ZoneService()
		{}
        #region  BasicMethod
        //此删除方法会删除主题下的所有板块 与 贴子 与 回贴
        public bool mydelete(int zoneId)
        {
            //开启事务
            //查询出此主题下的所有板块对象 
            List<Section> sectionList = sectionService.GetModelList("t_z_id='" + zoneId + "'");
            foreach (Section section in sectionList)
            {
                List<Topic> topicList = topicService.GetModelList("t_s_id='" + section.id + "'");
                foreach (Topic topic in topicList)
                {
                    replyService.DeleteByTid(topic.id);//删除此主贴下的所有回帖
                }
                topicDao.DeleteByTsid(section.id);//删除此板块下的所有主贴
            }
            //删除此主题下的所有板块
            sectionDao.DeleteByTzid(zoneId);

            //删除此主题
            return this.Delete(zoneId);//事务
            //事务提交
            //结束事务

        }

        public List<Zone> findAllZone(int pageNumber)
        {

            DataSet ds = this.GetListByPage("", "", (pageNumber - 1) * 5 + 1, pageNumber * pageCount);
            List<Zone> zoneList = this.DataTableToList(ds.Tables[0]);


            return zoneList; ;
        }

        //查询所以主题信息时也将此主题下相应的板块信息查询出来
        public List<Zone> GetZoneSectionList()
        {
            DataSet ds = dal.GetList("");
            List<Zone> zoneList = this.DataTableToList(ds.Tables[0]);

            //注意:下面代码就是重点
            foreach (Zone zone in zoneList)
            {
                DataSet ds2 = sectionDao.GetList("t_z_id = '" + zone.id + "'");
                List<Section> sectionList = sectionService.DataTableToList(ds2.Tables[0]);
                zone.sectionList = sectionList;
            }
            return zoneList;
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
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(hua_bbs.Model.Zone model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.Zone model)
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
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hua_bbs.Model.Zone GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public hua_bbs.Model.Zone GetModelByCache(int id)
		{
			
			string CacheKey = "ZoneModel-" + id;
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
			return (hua_bbs.Model.Zone)objModel;
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
		public List<hua_bbs.Model.Zone> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.Zone> DataTableToList(DataTable dt)
		{
			List<hua_bbs.Model.Zone> modelList = new List<hua_bbs.Model.Zone>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				hua_bbs.Model.Zone model;
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

