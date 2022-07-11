using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using hua_bbs.Model;
using System.Collections;
using System.Text;
using hua_bbs.DBUtility;

namespace hua_bbs.BLL
{
	/// <summary>
	/// SectionService
	/// </summary>
	public partial class SectionService
	{
		private readonly hua_bbs.DAL.SectionDao dal=new hua_bbs.DAL.SectionDao();
        private readonly hua_bbs.BLL.TopicService topicService = new TopicService();
        private readonly hua_bbs.BLL.ReplyService replyService = new ReplyService();
        private readonly hua_bbs.DAL.TopicDao topicDao = new hua_bbs.DAL.TopicDao();
        public int pageCount = 5;
        public SectionService()
		{}
        #region  BasicMethod

        public bool MyDelete(int sectionid)
        {
            List<Topic> topicList = topicService.GetModelList("t_s_id='" + sectionid + "'");
            foreach (Topic topic in topicList)
            {
                replyService.DeleteByTid(topic.id);//删除此主贴下的所有回帖
            }
            topicDao.DeleteByTsid(sectionid);//删除此板块下的所有主贴

            return dal.Delete(sectionid);
        }

        public bool saveOrUpdate(Section section)
        {
            //板块id等于0 进行数据添加 , 否则进行修改操作
            if (section.id == 0)
            {
                return dal.Add(section) > 0 ? true : false;
            }
            else
            {
                Section s = dal.GetModel(section.id);
                if (section.logo.Equals("/face/yyy123.gif"))
                {
                    section.logo = s.logo;
                }

                return dal.MyUpdate(section);
            }

        }

        public ArrayList GetSectionZoneUserList(int pageNumber, string sectionname, string uid, string zid)
        {
            StringBuilder strWhere = new StringBuilder();

            if (sectionname != null && !"".Equals(sectionname))
            {
                strWhere.Append(" and s.name = '" + sectionname + "' ");
            }

            if (uid != null && !"".Equals(uid))
            {
                strWhere.Append(" and u.id = '" + uid + "' ");
            }

            if (zid != null && !"".Equals(zid))
            {
                strWhere.Append(" and z.id = '" + zid + "' ");
            }

            int recordCout = dal.GetSectionZoneUserRecordCount(strWhere.ToString());

            DataSet ds = dal.GetSectionZoneUserListByPage(strWhere.ToString(), "", (pageNumber - 1) * pageCount + 1, pageNumber * pageCount);

            List<Section> sectionList = this.DataTableToSectionZoneUserList(ds.Tables[0]);

            string pageCode = PageUtil.GenPagination("/backstage/SectionList.aspx", recordCout, pageNumber, pageCount, "sectionname=" + sectionname + "&uid=" + uid + "&zid=" + zid);

            ArrayList list = new ArrayList();
            list.Add(sectionList);
            list.Add(pageCode);
            return list;
        }


        /// <summary>
        /// 获得数据列表(3表操作)
        /// </summary>
        public List<hua_bbs.Model.Section> DataTableToSectionZoneUserList(DataTable dt)
        {
            List<hua_bbs.Model.Section> modelList = new List<hua_bbs.Model.Section>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                hua_bbs.Model.Section model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToSectionZoneUser(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
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
		public bool Exists(int t_z_id,int t_u_id,int id)
		{
			return dal.Exists(t_z_id,t_u_id,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(hua_bbs.Model.Section model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.Section model)
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
		public bool Delete(int t_z_id,int t_u_id,int id)
		{
			
			return dal.Delete(t_z_id,t_u_id,id);
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
		public hua_bbs.Model.Section GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public hua_bbs.Model.Section GetModelByCache(int id)
		{
			
			string CacheKey = "SectionModel-" + id;
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
			return (hua_bbs.Model.Section)objModel;
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
		public List<hua_bbs.Model.Section> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.Section> DataTableToList(DataTable dt)
		{
			List<hua_bbs.Model.Section> modelList = new List<hua_bbs.Model.Section>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				hua_bbs.Model.Section model;
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

