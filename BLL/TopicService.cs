using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using hua_bbs.Model;
using System.Collections;
using hua_bbs.DBUtility;
using hua_bbs.DAL;

namespace hua_bbs.BLL
{
	/// <summary>
	/// TopicService
	/// </summary>
	public partial class TopicService
	{
		private readonly hua_bbs.DAL.TopicDao dal=new hua_bbs.DAL.TopicDao();
        private readonly hua_bbs.DAL.ReplyDao replyDao = new hua_bbs.DAL.ReplyDao();
        private readonly hua_bbs.DAL.UserDao userDao = new hua_bbs.DAL.UserDao();
        private readonly hua_bbs.DAL.SectionDao sectionDao = new hua_bbs.DAL.SectionDao();
        private readonly hua_bbs.DAL.ZoneDao zoneDao = new hua_bbs.DAL.ZoneDao();

        #region  BasicMethod


        //查询没有回复的贴子数量
        public int replyTopic(int sectionId)
        {
            return dal.reachReplyTopic(sectionId);

        }



        //***********************************************
        //置顶帖子
        //通过板块id(外键id)进行数据查询
        //获得回帖数， 最后回帖人和回帖时间， 普通帖子的发帖人
        //***********************************************
        public ArrayList FindStickTopic(int sectionId)
        {
            DataSet dataSet = dal.GetList("t_s_id='" + sectionId + "' and [top] = '1'");

            //将ds对象 转换 成List集合
            List<Topic> ZdTopicList = DataTableToList(dataSet.Tables[0]);
            //保存每个贴子的回复作者与回复时间
            Dictionary<int, Reply> topicLastReply = new Dictionary<int, Reply>();
            //保存每个贴子的回复数
            Dictionary<int, int> topicReplyCount = new Dictionary<int, int>();
            foreach (Topic topic in ZdTopicList)
            {
                //获取(封装)发贴人的用户信息到贴子对象中去
                topic._topicUser = userDao.GetModel(topic.t_u_id);

                //通过主帖id进行查询回复的贴子对象.但是我们只需要最后回复的那一条
                dataSet = replyDao.GetList(1, "t_t_id='" + topic.id + "'", "publishtime desc");
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    Reply reply = replyDao.DataRowToModel(dataSet.Tables[0].Rows[0]);//将查询出来的回贴DataSet转换成Reply对象
                                                                                ////获取(封装)回帖人的用户信息到回贴对象中去
                    reply._replyuser = userDao.GetModel(reply.t_u_id);

                    topicLastReply.Add(topic.id, reply);//主贴ID为key , 回贴对象为value

                    //得到此主贴下的回贴数
                    int count = replyDao.GetRecordCount("t_t_id='" + topic.id + "'");
                    topicReplyCount.Add(topic.id, count);//主贴ID为key , 回贴数为value
                }
            }
            ArrayList mylist = new ArrayList();
            mylist.Add(topicReplyCount);//0下标:回帖数
            mylist.Add(topicLastReply);//1下标:回帖作者与回帖时间
            mylist.Add(ZdTopicList);//2下标:保存的置顶的主贴
            return mylist;

        }






        //***********************************************
        //普通帖子
        //通过板块id(外键id)进行数据查询并且分页,
        //获得回帖数， 最后回帖人和回帖时间， 普通帖子的发帖人
        //***********************************************
        public ArrayList FindTopic(int sectionId, int pageNumber)
        {
            int pageCount = 4;  //一页显示的帖子（普通帖）个数

            //通过帖子所属于模块，的主键ID， 查询 发帖者和 所属于 大板块的 信息
            Section section = sectionDao.GetModel(sectionId);//通过主键ID查询板块信息
            section._user = userDao.GetModel(section.t_u_id);
            section._zone = zoneDao.GetModel(section.t_z_id);

            //查询主贴的记录数
            int recordCount = dal.GetRecordCount("t_s_id='" + sectionId + "'and [top]='0'");

            int maxPage = 0;
            if (recordCount % pageCount == 0)
            {
                maxPage = recordCount / pageCount;
            }
            else
            {
                maxPage = recordCount / pageCount + 1;
            }

            if (pageNumber > maxPage)
            {
                pageNumber = maxPage;
            }

            //主贴分页链接
            string pageCode  = PageUtil.GenPagination("/topic/TopicList.aspx", recordCount, pageNumber, pageCount, "sectionId=" + sectionId);
            //分页查询数据.返回dataset
            DataSet dataSet =  GetListByPage("t_s_id='" + sectionId + "'and [top]='0'", "", (pageNumber-1)*pageCount, pageNumber* pageCount);  //分页获得所有符合条件的数据列表
            List<Topic> topicList = DataTableToList(dataSet.Tables[0]);  //将里面数据进行转化为List<Topic>类型    //只收了第一页的数据

            //创建一个，使用键值对的方式
            //获取每一个帖子的 回复数
            Dictionary<int, int> topicReplyCount = new Dictionary<int, int>();
            //获取每一个帖子的 最后回复（人和时间）
            Dictionary<int, Reply> topiclastReply = new Dictionary<int, Reply>();
            
            //查询普通贴子
            foreach(Topic topic in topicList)
            {
                //获取(封装)发贴人的用户信息到贴子对象中去
                topic._topicUser = userDao.GetModel(topic.t_u_id);

                //通过主帖id进行查询回复的贴子对象.但是我们只需要最后回复的那一条
                dataSet = replyDao.GetList(1, "t_t_id = '" + topic.id + "'", "publishtime desc");  //
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    
                    Reply reply = replyDao.DataRowToModel(dataSet.Tables[0].Rows[0]);//将查询出来的回贴DataSet转换成Reply对象
                    //获取(封装)回帖人的用户信息到回贴对象中去
                    reply._replyuser = userDao.GetModel(reply.t_u_id);  //*******************reply.t_u_id  回帖人 ???
                    topiclastReply.Add(topic.id, reply);//主贴ID为key , 回贴对象为value

                    //得到此主贴下的回贴数
                    int count = replyDao.GetRecordCount("t_t_id='" + topic.id + "'");
                    topicReplyCount.Add(topic.id, count);//主贴ID为key , 回贴数为value
                }

            }

            ArrayList mylist = new ArrayList();
            mylist.Add(topicReplyCount);//0下标:回帖数
            mylist.Add(topiclastReply);//1下标:回帖作者与回帖时间
            mylist.Add(topicList);//2下标:普通的主贴对象
            mylist.Add(pageCode);//3下标:保存分页的连接
            mylist.Add(section);//4下标:保存的是板块对象
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
		public bool Exists(int t_u_id,int t_s_id,int id)
		{
			return dal.Exists(t_u_id,t_s_id,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(hua_bbs.Model.Topic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.Topic model)
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
		public bool Delete(int t_u_id,int t_s_id,int id)
		{
			
			return dal.Delete(t_u_id,t_s_id,id);
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
		public hua_bbs.Model.Topic GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public hua_bbs.Model.Topic GetModelByCache(int id)
		{
			
			string CacheKey = "TopicModel-" + id;
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
			return (hua_bbs.Model.Topic)objModel;
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
		public List<hua_bbs.Model.Topic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<hua_bbs.Model.Topic> DataTableToList(DataTable dt)
		{
			List<hua_bbs.Model.Topic> modelList = new List<hua_bbs.Model.Topic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				hua_bbs.Model.Topic model;
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

