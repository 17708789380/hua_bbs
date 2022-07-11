using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace hua_bbs.DAL
{
	/// <summary>
	/// 数据访问类:ZoneDao
	/// </summary>
	public partial class ZoneDao
	{
		public ZoneDao()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "t_zone"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_zone");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(hua_bbs.Model.Zone model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_zone(");
			strSql.Append("name,description)");
			strSql.Append(" values (");
			strSql.Append("@SQL2012name,@SQL2012description)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012name", SqlDbType.VarChar,50),
					new SqlParameter("@SQL2012description", SqlDbType.VarChar,100)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.description;

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
		public bool Update(hua_bbs.Model.Zone model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_zone set ");
			strSql.Append("name=@SQL2012name,");
			strSql.Append("description=@SQL2012description");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012name", SqlDbType.VarChar,50),
					new SqlParameter("@SQL2012description", SqlDbType.VarChar,100),
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.description;
			parameters[2].Value = model.id;

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
			strSql.Append("delete from t_zone ");
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_zone ");
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
		public hua_bbs.Model.Zone GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,name,description from t_zone ");
			strSql.Append(" where id=@SQL2012id");
			SqlParameter[] parameters = {
					new SqlParameter("@SQL2012id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			hua_bbs.Model.Zone model=new hua_bbs.Model.Zone();
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
		public hua_bbs.Model.Zone DataRowToModel(DataRow row)
		{
			hua_bbs.Model.Zone model=new hua_bbs.Model.Zone();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["description"]!=null)
				{
					model.description=row["description"].ToString();
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
			strSql.Append("select id,name,description ");
			strSql.Append(" FROM t_zone ");
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
			strSql.Append(" id,name,description ");
			strSql.Append(" FROM t_zone ");
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
			strSql.Append("select count(1) FROM t_zone ");
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
			strSql.Append(")AS Row, T.*  from t_zone T ");
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
			parameters[0].Value = "t_zone";
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

