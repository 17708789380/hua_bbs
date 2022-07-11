using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace hua_bbs.DAL
{
	/// <summary>
	/// 数据访问类:TopicDao
	/// </summary>
	public partial class TopicDao
	{
		public TopicDao()
		{}
        #region  BasicMethod



        //根据板块的id(外键t_s_id)进行删除
        public bool DeleteByTsid(int tsid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_topic ");
            strSql.Append(" where t_s_id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = tsid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //查询没有回复的贴子数量
        public int reachReplyTopic(int sid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from t_topic t where t.t_s_id = '" + sid + "'  and t.id not in (select t_t_id  from t_reply)");

            //select count(*) from(select id  from t_topic t where t_s_id = '1' and t.id not in (select t_t_id from t_reply )) tt;
            ///*需要给查询出来的临时表 命一个别名*/
            ///或者这一句SQL语句
            ///
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("t_u_id", "t_topic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int t_u_id,int t_s_id,int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_topic");
			strSql.Append(" where t_u_id=@SQL2012t_u_id and t_s_id=@SQL2012t_s_id and id=@SQL2012id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_s_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)			};
			parameters[0].Value = t_u_id;
			parameters[1].Value = t_s_id;
			parameters[2].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(hua_bbs.Model.Topic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_topic(");
			strSql.Append("t_u_id,t_s_id,content,modifytime,publishtime,title,good,[top])");
			strSql.Append(" values (");
			strSql.Append("@SQL2012t_u_id,@SQL2012t_s_id,@SQL2012content,@SQL2012modifytime,@SQL2012publishtime,@SQL2012title,@SQL2012good,@SQL2012top)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_s_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012content", SqlDbType.VarChar,1000),
					new SqlParameter("@SQL2012modifytime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012publishtime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012title", SqlDbType.VarChar,200),
					new SqlParameter("@SQL2012good", SqlDbType.VarChar,10),
					new SqlParameter("@SQL2012top", SqlDbType.VarChar,10)};
			parameters[0].Value = model.t_u_id;
			parameters[1].Value = model.t_s_id;
			parameters[2].Value = model.content;
			parameters[3].Value = model.modifytime;
			parameters[4].Value = model.publishtime;
			parameters[5].Value = model.title;
			parameters[6].Value = model.good;
			parameters[7].Value = model.top;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hua_bbs.Model.Topic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_topic set ");
			strSql.Append("content=@SQL2012content,");
			strSql.Append("modifytime=@SQL2012modifytime,");
			strSql.Append("publishtime=@SQL2012publishtime,");
			strSql.Append("title=@SQL2012title,");
			strSql.Append("good=@SQL2012good,");
			strSql.Append("[top]=@SQL2012top");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012content", SqlDbType.VarChar,1000),
					new SqlParameter("@SQL2012modifytime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012publishtime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012title", SqlDbType.VarChar,200),
					new SqlParameter("@SQL2012good", SqlDbType.VarChar,10),
					new SqlParameter("@SQL2012top", SqlDbType.VarChar,10),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_s_id", SqlDbType.Int,4)};
			parameters[0].Value = model.content;
			parameters[1].Value = model.modifytime;
			parameters[2].Value = model.publishtime;
			parameters[3].Value = model.title;
			parameters[4].Value = model.good;
			parameters[5].Value = model.top;
			parameters[6].Value = model.id;
			parameters[7].Value = model.t_u_id;
			parameters[8].Value = model.t_s_id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int t_u_id,int t_s_id,int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where t_u_id=@SQL2012t_u_id and t_s_id=@SQL2012t_s_id and id=@SQL2012id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_s_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)			};
			parameters[0].Value = t_u_id;
			parameters[1].Value = t_s_id;
			parameters[2].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hua_bbs.Model.Topic GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,[top] from t_topic ");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			hua_bbs.Model.Topic model=new hua_bbs.Model.Topic();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hua_bbs.Model.Topic DataRowToModel(DataRow row)
		{
			hua_bbs.Model.Topic model=new hua_bbs.Model.Topic();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["t_u_id"]!=null && row["t_u_id"].ToString()!="")
				{
					model.t_u_id=int.Parse(row["t_u_id"].ToString());
				}
				if(row["t_s_id"]!=null && row["t_s_id"].ToString()!="")
				{
					model.t_s_id=int.Parse(row["t_s_id"].ToString());
				}
				if(row["content"]!=null)
				{
					model.content=row["content"].ToString();
				}
				if(row["modifytime"]!=null && row["modifytime"].ToString()!="")
				{
					model.modifytime=DateTime.Parse(row["modifytime"].ToString());
				}
				if(row["publishtime"]!=null && row["publishtime"].ToString()!="")
				{
					model.publishtime=DateTime.Parse(row["publishtime"].ToString());
				}
				if(row["title"]!=null)
				{
					model.title=row["title"].ToString();
				}
				if(row["good"]!=null)
				{
					model.good=row["good"].ToString();
				}
				if(row["top"]!=null)
				{
					model.top=row["top"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,[top] ");
			strSql.Append(" FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,top ");
			strSql.Append(" FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from t_topic T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@SQL2012fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@SQL2012PageSize", SqlDbType.Int),
					new SqlParameter("@SQL2012PageIndex", SqlDbType.Int),
					new SqlParameter("@SQL2012IsReCount", SqlDbType.Bit),
					new SqlParameter("@SQL2012OrderType", SqlDbType.Bit),
					new SqlParameter("@SQL2012strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "t_topic";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

