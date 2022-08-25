using System.Data.SqlClient;
using System.Data;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace MyLib
{
    public static class Ado
    {
        public static SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbGODCH"].ConnectionString);
    }
    public class AdoQuery
    {
        SqlDataAdapter adp = new SqlDataAdapter("", Ado.Conn);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public DataTable QuerySql(string sql)
        {
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.CommandText = sql;
            adp.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
        public DataTable QuerySql(string sql, params SqlParameter[] parameter)
        {
            foreach (SqlParameter p in parameter)
            {
                adp.SelectCommand.Parameters.Add(p);
            }
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.CommandText = sql;
            adp.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
    }
    public class AdoExcel
    {
        SqlCommand cmd = new SqlCommand("", Ado.Conn);
        public void ExcelSql(string sql)
        {
            cmd.CommandText = sql;
            Ado.Conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Ado.Conn.Close();
        }
        public void ExcelSql(string sql, params SqlParameter[] parameters)
        {
            cmd.CommandText = sql;
            foreach (SqlParameter p in parameters)
            {
                cmd.Parameters.Add(p);
            }
            Ado.Conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Ado.Conn.Close();
        }
    }
}
