using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
using hua_bbs.Model;

namespace hua_bbs.DAL
{
	/// <summary>
	/// 数据访问类:ReplyDao
	/// </summary>
	public partial class ReplyDao
	{

		public ReplyDao()
		{}
        #region  BasicMethod

        /// <summary>
        /// 删除数据,根据外键t_t_id进删除
        /// </summary>
        public bool DeleteByTid(int t_t_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_reply ");
            strSql.Append(" where t_t_id=@t_t_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@t_t_id", SqlDbType.Int,4)
            };
            parameters[0].Value = t_t_id;

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

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("t_t_id", "t_reply"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int t_t_id,int t_u_id,int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_reply");
			strSql.Append(" where t_t_id=@SQL2012t_t_id and t_u_id=@SQL2012t_u_id and id=@SQL2012id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_t_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)			};
			parameters[0].Value = t_t_id;
			parameters[1].Value = t_u_id;
			parameters[2].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(hua_bbs.Model.Reply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_reply(");
			strSql.Append("t_t_id,t_u_id,modifytime,publishtime,content,title)");
			strSql.Append(" values (");
			strSql.Append("@SQL2012t_t_id,@SQL2012t_u_id,@SQL2012modifytime,@SQL2012publishtime,@SQL2012content,@SQL2012title)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_t_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012modifytime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012publishtime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012content", SqlDbType.VarChar,1000),
					new SqlParameter("@SQL2012title", SqlDbType.VarChar,50)};
			parameters[0].Value = model.t_t_id;
			parameters[1].Value = model.t_u_id;
			parameters[2].Value = model.modifytime;
			parameters[3].Value = model.publishtime;
			parameters[4].Value = model.content;
			parameters[5].Value = model.title;

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
		public bool Update(hua_bbs.Model.Reply model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_reply set ");
			strSql.Append("modifytime=@SQL2012modifytime,");
			strSql.Append("publishtime=@SQL2012publishtime,");
			strSql.Append("content=@SQL2012content,");
			strSql.Append("title=@SQL2012title");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012modifytime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012publishtime", SqlDbType.DateTime),
					new SqlParameter("@SQL2012content", SqlDbType.VarChar,1000),
					new SqlParameter("@SQL2012title", SqlDbType.VarChar,50),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_t_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4)};
			parameters[0].Value = model.modifytime;
			parameters[1].Value = model.publishtime;
			parameters[2].Value = model.content;
			parameters[3].Value = model.title;
			parameters[4].Value = model.id;
			parameters[5].Value = model.t_t_id;
			parameters[6].Value = model.t_u_id;

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
		/// 删除数据通过Reply的外键t_t_id删除
		/// </summary>
		public bool DeleteByTopicId(int id) //这是Reply的外键t_t_id删除
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_reply ");
            strSql.Append(" where t_t_id=@SQL2012id");
            SqlParameter[] parameters = {
                    new SqlParameter("@SQL2012id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

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

        /// <summary>
        /// 删除一条数据（Reply的主键id删除）
        /// </summary>
        public bool Delete(int id) //这是Reply的主键id
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_reply ");
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
		public bool Delete(int t_t_id,int t_u_id,int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_reply ");
			strSql.Append(" where t_t_id=@SQL2012t_t_id and t_u_id=@SQL2012t_u_id and id=@SQL2012id ");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012t_t_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012t_u_id", SqlDbType.Int,4),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)			};
			parameters[0].Value = t_t_id;
			parameters[1].Value = t_u_id;
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
			strSql.Append("delete from t_reply ");
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
		public hua_bbs.Model.Reply GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,t_t_id,t_u_id,modifytime,publishtime,content,title from t_reply ");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			hua_bbs.Model.Reply model=new hua_bbs.Model.Reply();
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
		public hua_bbs.Model.Reply DataRowToModel(DataRow row)
		{
			hua_bbs.Model.Reply model=new hua_bbs.Model.Reply();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["t_t_id"]!=null && row["t_t_id"].ToString()!="")
				{
					model.t_t_id=int.Parse(row["t_t_id"].ToString());
				}
				if(row["t_u_id"]!=null && row["t_u_id"].ToString()!="")
				{
					model.t_u_id=int.Parse(row["t_u_id"].ToString());
				}
				if(row["modifytime"]!=null && row["modifytime"].ToString()!="")
				{
					model.modifytime=DateTime.Parse(row["modifytime"].ToString());
				}
				if(row["publishtime"]!=null && row["publishtime"].ToString()!="")
				{
					model.publishtime=DateTime.Parse(row["publishtime"].ToString());
				}
				if(row["content"]!=null)
				{
					model.content=row["content"].ToString();
				}
				if(row["title"]!=null)
				{
					model.title=row["title"].ToString();
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
			strSql.Append("select id,t_t_id,t_u_id,modifytime,publishtime,content,title ");
			strSql.Append(" FROM t_reply ");
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
			strSql.Append(" id,t_t_id,t_u_id,modifytime,publishtime,content,title ");
			strSql.Append(" FROM t_reply ");
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
			strSql.Append("select count(1) FROM t_reply ");
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
			strSql.Append(")AS Row, T.*  from t_reply T ");
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
			parameters[0].Value = "t_reply";
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

