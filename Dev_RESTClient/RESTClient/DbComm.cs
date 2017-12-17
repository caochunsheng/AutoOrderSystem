using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using System.Diagnostics;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace DbControl
{
    public class DbComm
    {
        public static string DATABASE_NAME = "";
        public static string DATABASE_STRING = "";

        //public static string _DB_NAME = "MES_Interface";
        //public static string _DB_SERVER = "sql12.htcxms.com,21434";
        //public static string _DB_USER = "cutting_user";
        //public static string _DB_PASS = "cuttingSa1234";

        private static string _DB_SERVER = "sql12.htcxms.com,21434";
        public static string DbServer 
        { 
            get { return _DB_SERVER; }
            set {_DB_SERVER = value; } 
        }
        private static string _DB_PORT = "";
        public static string DbPort
        {
            get { return _DB_PORT; }
            set { _DB_PORT = value; }
        }
        private static string _DB_NAME = "MES_Interface";
        public static string DbName
        {
            get { return _DB_NAME; }
            set { _DB_NAME = value; }
        }
        private static string _DB_USER = "cutting_user";
        public static string DbUser
        {
            get { return _DB_USER; }
            set { _DB_USER = value; }
        }
        private static string _DB_PASS = "cuttingSa1234";
        public static string DbPass
        {
            get { return _DB_PASS; }
            set { _DB_PASS = value; }
        }

        //恒通赛木数据库资料
        //恒通赛木：EXEC cutting.GetWorkOrder NULL,NULL
        //数据库版本：sql2012 
        //服务器：sql12.htcxms.com,21434
        //用户名：cutting_user
        //密码:cuttingSa1234

        /*
        请求作业可以是定时轮询，也可以是人工触发（与生产线的工作过程有关，比如要人工上料或者换刀具）
        同一个作业批次，只要东杰没有确认产品下线，就是未完成状态，无论请求多少 次，都会返回这个批次，不能越过
        除非MES系统取消该作业，所以不用考虑频率的问题
        裁切线，理论上只是获取作业计划，根据计划调整生产线的工作参数，然后 等待东杰送板上线，分段切完后输送至东杰的对接工位上
        这是作业流程，如果中间有异常，需要通过PLC及时与上下料（即东杰）交互，同时向MES汇报一次报警
        此外，定时，可以是5秒至3分钟之间的任意值，向MES汇报产线各核心设备的工作状态（结构由厂家自定义，但需要交付前告知MES）整合形成一个XML字符串提交
        大体上就是这些，看还有什么问题
         * 
         * 板边有20的凸起,
         * 不同孔数也许影响功率
         * 壁厚基本相同
         * 然后就是裁切线了,两点表示一线
         * 线宽6，以锯为准，后续要调整
         * 定位误差加裁切误差1mm以内
         * 材质e，p都是水泥
         * 批次和数量就不解释了
        */

        public static DataTable GetWorkOrder(String dbServer, String dbUser, String dbPass)
        {
            DataTable dtResult = null;
            try
            {
                _DB_SERVER = dbServer;
                _DB_USER = dbUser;
                _DB_PASS = dbPass;

                String connectString = String.Format("Data Source={0};Initial Catalog={1};User id={2};Password={3};", _DB_SERVER, _DB_NAME, _DB_USER, _DB_PASS);
                dtResult = CallStoredProcedure(connectString, "GetWorkOrder", "@Position", "NULL", "@AdditionalPosition", "NULL");

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    DataRow row = dtResult.Rows[i];
                }
            }
            catch (Exception e)
            {
                dtResult = null;

                Debug.WriteLine(e.Message);
            }
            return dtResult;
        }

        public static string GetTempFileName(string extension)
        {
            int attempt = 0;
            while (true)
            {
                string fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);
                fileName = Path.Combine(Path.GetTempPath(), fileName);

                try
                {
                    if (!File.Exists(fileName))
                        return fileName;
                }
                catch (Exception ex)
                {
                    if (++attempt == 20)
                        return "";
                }
            }
        }

        public static String GetDatabseName()
        {
            return DATABASE_NAME;
        }

        public static string GetConnectString()
        {
            return DATABASE_STRING;// String.Format("Server=localhost;Port=5432;Database={0};User Id=postgres;Password=Admin", DbComm.DATABASE_NAME);
        }

        public delegate void Logger(String log);
        public static Logger _OutputLog = null;

        public static void SetLogger(Logger funcLog)
        {
            _OutputLog = funcLog;
        }

        public static void AppendLog(string log)
        {
            if (_OutputLog != null)
                _OutputLog(log);
        }

        public static void SetDatabseName(string dbName, String dbString)
        {
            DATABASE_NAME = dbName;
            DATABASE_STRING = dbString;

            ExtractFromConnectString(DATABASE_STRING);
        }

        public static String ExtractFromConnectString(String connectString)
        {
            String[] items = connectString.Split(';'); // String.Format("Server=localhost;Port=5432;Database={0};User Id=postgres;Password=Admin", DbComm.DATABASE_NAME);
            foreach (String pairs in items)
            {
                String[] pair = pairs.Split('=');
                if (pair.Length == 2)
                {
                    if (pair[0].Equals("Server", StringComparison.InvariantCultureIgnoreCase))
                        _DB_SERVER = pair[1].Trim();
                    else if (pair[0].Equals("Port", StringComparison.InvariantCultureIgnoreCase))
                        _DB_PORT = pair[1].Trim();
                    else if (pair[0].Equals("Database", StringComparison.InvariantCultureIgnoreCase))
                        _DB_NAME = pair[1].Trim();
                    else if (pair[0].Equals("User Id", StringComparison.InvariantCultureIgnoreCase))
                        _DB_USER = pair[1].Trim();
                    else if (pair[0].Equals("Password", StringComparison.InvariantCultureIgnoreCase))
                        _DB_PASS = pair[1].Trim();
                }
            }
            return _DB_NAME;
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static Boolean IsDatabaseExist(string dbName)
        {
            bool dbExists = false;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(DbComm.GetConnectString()))//"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=Admin"))
                {
                    conn.Open();
                    string cmdText = String.Format("SELECT 1 FROM pg_database WHERE datname='{0}'", dbName);
                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn))
                    {
                        dbExists = (cmd.ExecuteScalar() != null);
                    }
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
            }

            return dbExists;
        }

        public static Boolean IsTableExist(string tblName)
        {
            return IsTableExist(DbComm.GetConnectString(), tblName);
        }

        public static Boolean IsTableExist(String connectString, string tblName)
        {
            bool dbExists = false;
            try
            {
                string dbName = _DB_NAME;

                //string connectString = DbComm.GetConnectString();// String.Format("Server=localhost;Port=5432;Database={0};User Id=postgres;Password=Admin", dbName);
                using (NpgsqlConnection con = new NpgsqlConnection(connectString))
                {
                    con.Open();

                    string cmdText = String.Format("SELECT 1 FROM pg_database WHERE datname='{0}'", dbName);
                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, con))
                    {
                        dbExists = (cmd.ExecuteScalar() != null);
                        if (!dbExists)
                            return false;

                        cmdText = String.Format("SELECT * FROM information_schema.tables WHERE table_name='{0}'", tblName);
                        if (cmd.Connection == null)
                            cmd.Connection = con;
                        if (cmd.Connection.State != ConnectionState.Open)
                            cmd.Connection.Open();

                        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                        {
                            try
                            {
                                if ((rdr != null) && rdr.HasRows)
                                    return true;
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
            }
            return false;
        }

        public static String GetParameter(String keyName, String defaultValue)
        {
            NpgsqlConnection pgConnection = null;
            NpgsqlTransaction pgTransaction = null;
            NpgsqlCommand pgCommand = null;

            String resultString = defaultValue;
            try
            {
                pgConnection = new NpgsqlConnection(DbComm.GetConnectString());
                if (pgConnection != null)
                {
                    pgConnection.Open();

                    pgTransaction = pgConnection.BeginTransaction();
                    if (pgTransaction != null)
                    {
                        try
                        {
                            pgCommand = new NpgsqlCommand();
                            pgCommand.Connection = pgConnection;
                            pgCommand.CommandType = CommandType.Text;

                            pgCommand.CommandText = String.Format("SELECT ValueString FROM SYS_PARAMETER WHERE Parameter_Id=\'{0}\'", keyName);
                            object result = pgCommand.ExecuteScalar();
                            if (result != null)
                            {
                                resultString = Convert.ToString(result).Trim();
                            }
                            else
                            {
                                pgCommand.CommandText = String.Format("INSERT INTO SYS_PARAMETER (Parameter_Id,Type,ValueString) VALUES (\'{0}\', \'{1}\', \'{2}\')", keyName, "String", defaultValue);
                                pgCommand.ExecuteNonQuery();
                            }
                            pgTransaction.Commit();
                            pgConnection.Close();
                        }
                        catch (Exception e)
                        {
                            pgTransaction.Rollback();
                        }
                    }
                    pgConnection.Close();
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
            }
            return resultString;
        }

        public static String SetParameter(String keyName, String setValue)
        {
            NpgsqlConnection pgConnection = null;
            NpgsqlTransaction pgTransaction = null;
            NpgsqlCommand pgCommand = null;

            String resultString = setValue;
            try
            {
                pgConnection = new NpgsqlConnection(DbComm.GetConnectString());
                if (pgConnection != null)
                {
                    pgConnection.Open();

                    pgTransaction = pgConnection.BeginTransaction();
                    if (pgTransaction != null)
                    {
                        try
                        {
                            pgCommand = new NpgsqlCommand();
                            pgCommand.Connection = pgConnection;
                            pgCommand.CommandType = CommandType.Text;

                            pgCommand.CommandText = String.Format("SELECT ValueString FROM SYS_PARAMETER WHERE Parameter_Id=\'{0}\'", keyName);
                            object result = pgCommand.ExecuteScalar();
                            if (result != null)
                            {
                                pgCommand.CommandText = String.Format("UPDATE SYS_PARAMETER SET ValueString=\'{0}\' WHERE Parameter_Id=\'{1}\'", setValue, keyName);
                                pgCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                pgCommand.CommandText = String.Format("INSERT INTO SYS_PARAMETER (Parameter_Id,Type,ValueString) VALUES (\'{0}\', \'{1}\', \'{2}\')", keyName, "String", setValue);
                                pgCommand.ExecuteNonQuery();
                            }
                            pgTransaction.Commit();
                            pgConnection.Close();
                        }
                        catch (Exception e)
                        {
                            pgTransaction.Rollback();
                        }
                    }
                    pgConnection.Close();
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
            }
            return resultString;
        }

        public static int GetNextID(String idName)
        {
            NpgsqlConnection pgConnection = null;
            NpgsqlTransaction pgTransaction = null;
            NpgsqlCommand pgCommand = null;

            int nid = 1000;
            try
            {
                pgConnection = new NpgsqlConnection(DbComm.GetConnectString());
                if (pgConnection != null)
                {
                    pgConnection.Open();

                    pgTransaction = pgConnection.BeginTransaction();
                    if (pgTransaction != null)
                    {
                        try
                        {
                            pgCommand = new NpgsqlCommand();
                            pgCommand.Connection = pgConnection;
                            pgCommand.CommandType = CommandType.Text;

                            pgCommand.CommandText = String.Format("SELECT ValueString FROM SYS_PARAMETER WHERE Parameter_Id=\'{0}\'", idName);
                            object result = pgCommand.ExecuteScalar();
                            if ((result != null) && !String.IsNullOrEmpty(result.ToString()))
                            {
                                nid = Convert.ToInt32(result) + 1;

                                pgCommand.CommandText = String.Format("UPDATE SYS_PARAMETER SET ValueString=\'{0}\' WHERE Parameter_Id=\'{1}\'", nid.ToString(), idName);
                                pgCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                pgCommand.CommandText = String.Format("INSERT INTO SYS_PARAMETER (Parameter_Id,Type,ValueString) VALUES (\'{0}\', \'{1}\', \'{2}\')", idName, "Integer", nid.ToString());
                                pgCommand.ExecuteNonQuery();
                            }

                            pgTransaction.Commit();
                            pgConnection.Close();
                            return nid;
                        }
                        catch (Exception e)
                        {
                            pgTransaction.Rollback();
                        }
                    }
                    pgConnection.Close();
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
                nid = -1;
            }
            return nid;
        }

        public static DataColumnCollection GetTableColumns(string tableName)
        {
            return GetTableColumns(DbComm.GetConnectString(), tableName);
        }

        public static DataColumnCollection GetTableColumns(String connectString, string tableName)
        {
            NpgsqlConnection pgConnection = null;
            DataTable dt = new DataTable();
            DataColumnCollection columns = null;

            try
            {
                pgConnection = new NpgsqlConnection(connectString);
                if (pgConnection != null)
                {
                    pgConnection.Open();
                    using (NpgsqlDataAdapter pgAdapter = new NpgsqlDataAdapter())
                    {
                        string cmdText = String.Format("SELECT * FROM {0}", tableName);
                        pgAdapter.SelectCommand = new NpgsqlCommand(cmdText, pgConnection);
                        pgAdapter.Fill(dt);
                        columns = dt.Columns;
                    }
                    pgConnection.Close();
                    return columns;
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
            }
            return null;
        }

        public static string DataTable2Json(DataTable dt, Boolean bCompact=false)
        {
            StringBuilder JsonString = new StringBuilder();

            JsonString.Append("[\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (bCompact)
                    JsonString.Append("[\r\n");
                else
                    JsonString.Append("{\r\n");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string cell = "\"\"";
                    if (dt.Rows[i][j] != DBNull.Value)
                    {
                        if (dt.Columns[j].DataType.FullName.Equals("System.DateTime"))
                        {
                            cell = Convert.ToDateTime(dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                            cell = cell.Replace(" 00:00:00", "");
                        }
                        else
                        {
                            cell = dt.Rows[i][j].ToString().Trim();
                        }

                        if (dt.Columns[j].DataType.FullName.Equals("System.DateTime") ||
                            dt.Columns[j].DataType.FullName.Equals("System.Date") ||
                            dt.Columns[j].DataType.FullName.Equals("System.Time") ||
                            dt.Columns[j].DataType.FullName.Equals("System.String"))
                        {
                            cell = String.Format("\"{0}\"", cell);
                        }
                    }
                    if (bCompact)
                        JsonString.Append(cell);
                    else
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + cell);

                    if (j < dt.Columns.Count - 1)
                        JsonString.Append(",");
                    JsonString.Append("\r\n");
                }

                if (i == dt.Rows.Count - 1)
                {
                    if (bCompact)
                        JsonString.Append("]\r\n");
                    else
                        JsonString.Append("}\r\n");
                }
                else
                {
                    if (bCompact)
                        JsonString.Append("],\r\n");
                    else
                        JsonString.Append("},\r\n");
                }
            }
            JsonString.Append("]\r\n");

            return JsonString.ToString();
        }

        public static String GetDate(String fullDateTime)
        {
            if (String.IsNullOrEmpty(fullDateTime))
                return String.Empty;

            int idx = fullDateTime.Trim().LastIndexOf(" ");
            if (idx >= 0)
                return fullDateTime.Substring(0, idx);
            return fullDateTime;
        }

        public static String GetTime(String fullDateTime)
        {
            if (String.IsNullOrEmpty(fullDateTime))
                return String.Empty;

            int idx = fullDateTime.Trim().LastIndexOf(" ");
            if (idx >= 0)
                return fullDateTime.Substring(idx+1);
            return fullDateTime;
        }

        public static DataTable SqlQuery(String sql, StringBuilder errMessage)
        {
            DataTable dt = null;

            NpgsqlConnection pgConnection = null;
            NpgsqlDataAdapter pgAdapter = null;

            try
            {
                pgConnection = new NpgsqlConnection(DbComm.GetConnectString());
                pgConnection.Open();

                pgAdapter = new NpgsqlDataAdapter();
                pgAdapter.SelectCommand = new NpgsqlCommand(sql, pgConnection);

                dt = new DataTable();
                pgAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                errMessage.Append(e.Message);
                dt = null;
            }

            finally
            {
                if (pgAdapter != null)
                    pgAdapter.Dispose();
                if (pgConnection != null)
                {
                    pgConnection.Close();
                    pgConnection.Dispose();
                }
            }
            return dt;
        }

        public static Boolean SqlNonQuery(String sql, StringBuilder errMessage)
        {
            Boolean bResult = false;

            NpgsqlConnection pgConnection = null;
            NpgsqlTransaction pgTransaction = null;
            NpgsqlCommand pgCommand = null;

            try
            {
                pgConnection = new NpgsqlConnection(DbComm.GetConnectString());
                pgConnection.Open();

                pgTransaction = pgConnection.BeginTransaction();
                try
                {
                    pgCommand = new NpgsqlCommand();
                    pgCommand.Connection = pgConnection;
                    pgCommand.CommandType = CommandType.Text;

                    pgCommand.CommandText = sql;
                    pgCommand.ExecuteNonQuery();

                    pgTransaction.Commit();

                    bResult = true;
                }
                catch (Exception e)
                {
                    pgTransaction.Rollback();
                }
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
                errMessage.Append(e.Message);
                bResult = false;
            }

            finally
            {
                if (pgCommand != null)
                    pgCommand.Dispose();
                if (pgTransaction != null)
                    pgTransaction.Dispose();
                if (pgConnection != null)
                {
                    pgConnection.Close();
                    pgConnection.Dispose();
                }
            }
            return bResult;
        }

        public static Boolean Serialize(DataSet ds, Stream stream)
        {
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, ds);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public static DataSet Deserialize(Stream stream)
        {
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                return (DataSet)serializer.Deserialize(stream);
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static byte[] GzipCompress(byte[] input)
        {
            try
            {
                byte[] compressed = null;
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (GZipStream tinyStream = new GZipStream(outStream, CompressionMode.Compress))
                    using (MemoryStream mStream = new MemoryStream(input))//Encoding.UTF8.GetBytes(inputString)))
                        mStream.CopyTo(tinyStream);

                    compressed = outStream.ToArray();
                }
                return compressed;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static byte[] GzipDecompress(byte[] input)
        {
            try
            {
                byte[] decompressed = null;
                using (MemoryStream inStream = new MemoryStream(input))
                using (GZipStream bigStream = new GZipStream(inStream, CompressionMode.Decompress))
                using (MemoryStream bigStreamOut = new MemoryStream())
                {
                    bigStream.CopyTo(bigStreamOut);
                    decompressed = bigStreamOut.ToArray();
                }
                return decompressed;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static String byte2hex(byte[] bytes)
        { // byte array to hex string with "0x" prefix 
            string tbl = "0123456789ABCDEF";
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += tbl[(bytes[i] >> 4)];
                hexString += tbl[(bytes[i] & 0xF)];
            }
            //string hexString = Fast.ToHexString(bytes);//, true); //true for "0x" prefix
            return hexString;
        }

        public static byte[] hex2byte(String hex)
        { // hex string to byte array
            String tbl = "00112233445566778899aAbBcCdDeEfF";
            byte[] bytes = new byte[hex.Length / 2];

            String chr;
            for (int i = 0; i < hex.Length / 2; i++)
            {
                chr = hex.Substring(2 * i, 1);
                bytes[i] = (byte)((tbl.IndexOf(chr) / 2) << 4);

                chr = hex.Substring(2 * i + 1, 1);
                bytes[i] += (byte)((tbl.IndexOf(chr) / 2));
            }
            //byte[] bytes = Fast.FromHexString(hex);
            return bytes;
        }

        public static byte[] CompressDataTable2HexString(DataTable dt)
        {
            try
            {
                String hexString = null;
                byte[] compressed = null;

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                MemoryStream stream = new MemoryStream();
                if (DbComm.Serialize(ds, stream))
                {
                    compressed = DbComm.GzipCompress(stream.ToArray());
                    if (compressed != null)
                    {
                        //hexString = DbComm.byte2hex(compressed);
                    }
                }
                return compressed;
            }
            catch(Exception e)
            {
            }
            return null;
        }

        public static DataTable DecompressHex2DataTable(byte[] bytes)//String hexString)
        {
            try
            {
                //byte[] bytes = DbComm.hex2byte(hexString);
                if (bytes != null)
                {
                    byte[] decompressed = DbComm.GzipDecompress(bytes);
                    if (decompressed != null)
                    {
                        MemoryStream stream = new MemoryStream(decompressed);
                        DataSet ds = DbComm.Deserialize(stream);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                                return ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static DataTable CallStoredProcedure(String storedName, params String[] Params)
        {
            return CallStoredProcedure(GetConnectString(), storedName, Params);
        }

        public static DataTable CallStoredProcedure(String connectString, String storedName, params String[] Params)
        {
            DataTable dtResult = null;

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectString;
                sqlConnection.Open();

                sqlCommand = new SqlCommand(storedName, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Params.Length / 2; i++)
                {
                    if (Params[2 * i].StartsWith("@"))
                        sqlCommand.Parameters.Add(new SqlParameter(Params[2 * i], Params[2 * i + 1]));
                    else
                        sqlCommand.Parameters.Add(new SqlParameter("@" + Params[2 * i], Params[2 * i + 1]));
                }

                sqlAdapter = new SqlDataAdapter(sqlCommand);

                dtResult = new DataTable();
                sqlAdapter.Fill(dtResult);
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
                dtResult = null;
            }

            finally
            {
                if (sqlCommand != null)
                    sqlCommand.Dispose();
                if (sqlAdapter != null)
                    sqlAdapter.Dispose();
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            return dtResult;
        }

        public static DataTable CallStoredProcedure(String storedName, List<String> paramNames, List<String> paramValues)
        {
            return CallStoredProcedure(GetConnectString(), storedName, paramNames, paramValues);
        }

        public static DataTable CallStoredProcedure(String connectString, String storedName, List<String> paramNames, List<String> paramValues)
        {
            DataTable dtResult = null;

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlAdapter = null;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectString;
                sqlConnection.Open();

                sqlCommand = new SqlCommand(storedName, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramNames.Count; i++)
                {
                    if (paramNames[i].StartsWith("@"))
                        sqlCommand.Parameters.Add(new SqlParameter(paramNames[i], paramValues[i]));
                    else
                        sqlCommand.Parameters.Add(new SqlParameter("@" + paramNames[i], paramValues[i]));
                }
                sqlAdapter = new SqlDataAdapter(sqlCommand);

                dtResult = new DataTable();
                sqlAdapter.Fill(dtResult);
            }
            catch (Exception e)
            {
                AppendLog(e.Message);
                dtResult = null;
            }

            finally
            {
                if (sqlCommand != null)
                    sqlCommand.Dispose();
                if (sqlAdapter != null)
                    sqlAdapter.Dispose();
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            return dtResult;
        }

        public static Boolean CreateHistoryTable(String connectString, String tblName)
        {
            NpgsqlConnection pgConnection = null;
            NpgsqlTransaction pgTransaction = null;
            NpgsqlCommand pgCommand = null;

            try
            {
                String sPrefix = "history_";
                String sSuffix = "";
                String newTableName = sPrefix + tblName.Trim() + sSuffix;

                DataColumnCollection columns = DbComm.GetTableColumns(connectString, tblName);
                DataColumnCollection columnsHistory = null;

                Boolean bExist = IsTableExist(connectString, newTableName);
                if (bExist)
                    columnsHistory = DbComm.GetTableColumns(connectString, newTableName);

                pgConnection = new NpgsqlConnection(connectString);
                if (pgConnection != null)
                {
                    pgConnection.Open();

                    pgTransaction = pgConnection.BeginTransaction();
                    if (pgTransaction != null)
                    {
                        try
                        {
                            pgCommand = new NpgsqlCommand();
                            pgCommand.Connection = pgConnection;
                            pgCommand.CommandType = CommandType.Text;

                            if (columnsHistory == null)
                            {
                                String tblCreate = String.Format("CREATE TABLE {0} AS TABLE {1} WITH NO DATA", newTableName, tblName);
                                pgCommand.CommandText = tblCreate;
                                pgCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                for (int i = 0; i < columns.Count; i++)
                                {
                                    Boolean absent = true;
                                    for (int j = 0; j < columnsHistory.Count; j++)
                                    {
                                        if (columnsHistory[j].ColumnName.Equals(columns[i].ColumnName, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            absent = false;
                                            break;
                                        }
                                    }
                                    if (absent)
                                    {
                                        String alter = "";
                                        if (columns[i].DataType == typeof(DateTime))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} date NULL", newTableName, columns[i].ColumnName);
                                        else if (columns[i].DataType == typeof(String))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} varchar(256) NULL", newTableName, columns[i].ColumnName);
                                        else if (columns[i].DataType == typeof(int))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} int NULL", newTableName, columns[i].ColumnName);
                                        else if (columns[i].DataType == typeof(Int32))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} int NULL", newTableName, columns[i].ColumnName);
                                        else if (columns[i].DataType == typeof(Int64))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} int NULL", newTableName, columns[i].ColumnName);
                                        else if (columns[i].DataType == typeof(Double))
                                            alter = String.Format("ALTER TABLE {0} ADD COLUMN {1} double precision NULL", newTableName, columns[i].ColumnName);
                                        if (!String.IsNullOrEmpty(alter))
                                        {
                                            pgCommand.CommandText = alter;
                                            pgCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            pgTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            pgTransaction.Rollback();
                        }
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            finally
            {
                if (pgCommand != null)
                    pgCommand.Dispose();
                if (pgTransaction != null)
                    pgTransaction.Dispose();
                if (pgConnection != null)
                {
                    pgConnection.Close();
                    pgConnection.Dispose();
                }
            }

            return false;
        }
    }
}
