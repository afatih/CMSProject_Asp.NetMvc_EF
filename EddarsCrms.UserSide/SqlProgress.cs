using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCrms.UserSide
{
    public class SqlProgress:IDisposable
    {
        private readonly SqlConnection con = new SqlConnection();
        private readonly GeneralSettings general = new GeneralSettings();
        
        public SqlProgress()
        {
            con.ConnectionString = general.ConString;
        }

        public SqlProgress(string _dataSource, string _initialCatalog, string _userId, string _pass)
        {
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder
            {
                DataSource = _dataSource,
                UserID = _userId,
                Password = _pass,
                InitialCatalog = _initialCatalog
            };
            con.ConnectionString = build.ConnectionString;
        }

        public SqlConnection Open()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                return con;
            }
            catch
            {
                return null;
            }
        }

        public int SetExecuteNonQuery(string _commandText, CommandType _cmdType = CommandType.Text, params SqlParameter[] _params)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = _commandText;
                    cmd.CommandType = _cmdType;
                    cmd.Connection = Open();
                    if (_params != null)
                        cmd.Parameters.AddRange(_params);
                    int result = cmd.ExecuteNonQuery();
                    Close();
                    return result;
                }
            }
            catch
            {
                return 0;
            }
        }
        public object SetExecuteScalar(string _commandText, CommandType _cmdType = CommandType.Text, params SqlParameter[] _params)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = _commandText;
                    cmd.CommandType = _cmdType;
                    cmd.Connection = Open();
                    if (_params != null)
                        cmd.Parameters.AddRange(_params);
                    object result = cmd.ExecuteScalar();
                    Close();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetDataSet(string _commandText, CommandType _cmdType = CommandType.Text, params SqlParameter[] _params)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {





                    cmd.CommandText = _commandText;
                    cmd.CommandType = _cmdType;
                    cmd.Connection = con;
                    if (_params != null)
                        cmd.Parameters.AddRange(_params);
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    using (DataSet ds = new DataSet())
                    {
                        dap.Fill(ds);
                        return ds;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetDataTable(string _commandText, CommandType _cmdType = CommandType.Text, params SqlParameter[] _params)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = _commandText;
                    cmd.CommandType = _cmdType;
                    cmd.Connection = this.con;
                    if (_params != null)
                        cmd.Parameters.AddRange(_params);
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    using (DataTable dt = new DataTable())
                    {
                        dap.Fill(dt);
                        Close();
                        return dt;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public SqlDataReader GetExecuteReader(string _commandText, CommandType _cmdType = CommandType.Text, params SqlParameter[] _params)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = _commandText;
                    cmd.CommandType = _cmdType;
                    cmd.Connection = con;
                    con.Open();
                    if (_params != null)
                        cmd.Parameters.AddRange(_params);
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }

            }
            catch
            {
                return null;
            }
        }


        public void Close()
        {
            con.Close();
        }


        public void Dispose()
        {
            con.Close();
            con.Dispose();
        }
    }
}
