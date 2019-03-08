using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static EISv3.Model.EmpInfo;


namespace EISv3.Utils
{
    static class Connection
    {

        #region Select DB

        // Local MySQL 
        private static string USER_ID = "";
        private static string PSWD = "";
        private static string DATA_SOURCE = "(localdb)\\MSSQLLocalDB";
        private static string CATALOG = "Employee_Information_System";

        // Local SQL Server 
        //private static string USER_ID = "";
        //private static string PSWD = "";
        //private static string DATA_SOURCE = "(localdb)\\ProjectsV13";
        //private static string CATALOG = "Employee_Information_System";

        // Microsift Azure Online
        //private static string USER_ID = "sumit";
        //private static string PSWD = "Mahajan123@";
        //private static string DATA_SOURCE = "sumit-mahajan.database.windows.net";
        //private static string CATALOG = "Employee_Information_System";

        #endregion

        //Common connection string for whole project
        private static SqlConnection connection = new SqlConnection(getConnectionString());

        //Connection string building
        private static string getConnectionString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Data Source=");
            builder.Append(DATA_SOURCE);
            builder.Append(";Initial Catalog=");
            builder.Append(CATALOG);
            builder.Append(";User ID=");
            builder.Append(USER_ID);
            builder.Append(";Password=");
            builder.Append(PSWD);

            return builder.ToString();
        }

        #region Execution and Close Connetion

        //Status of Query action & Close connection 
        private static bool returnAndClose(SqlCommand command)
        {
            int cnt = command.ExecuteNonQuery();
            connection.Close();
            return cnt > 0 ? true : false;
        }

        #endregion

        #region Callers to build Queries as per the requirement

        //Retrive data (Select Query)
        public static List<T> getData<T>(string query)
        {
            connection.Open();
            SqlDataReader dataReader = new SqlCommand(query, connection).ExecuteReader();
            return processResponse<T>(dataReader);
        }

        //Insert data (Insert Query)
        public static bool setData<T>(T obj)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(getInsertQuery(obj), connection);
            return returnAndClose(command);
        }

        //Update data (Update Query)
        public static bool updateData<T>(T obj, string key)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(getUpdateQuery(obj, key), connection);
            return returnAndClose(command);
        }

        //Delete data (Delete Query)
        public static bool deleteData<T>(string key, string value)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(getDeleteQuery<T>(key, value), connection);
            return returnAndClose(command);
        }

        #endregion

        #region Generic Processes to Perform a Query

        //Getting data after select command
        private static List<T> processResponse<T>(SqlDataReader dataReader)
        {
            Type type = typeof(T);

            List<T> dataList = new List<T>();
            while (dataReader.Read())
            {
                T obj = (T)Activator.CreateInstance(type);
                foreach (FieldInfo info in type.GetFields())
                {
                    string data = dataReader.GetValue(dataReader.GetOrdinal(info.Name)).ToString();
                    string datatype = info.FieldType.Name;

                    switch (datatype)
                    {
                        case "String":
                            info.SetValue(obj, data);
                            break;
                        case "Int32":
                            info.SetValue(obj, Int32.Parse(data));
                            break;
                        case "DateTime":
                        case "Nullable`1":
                            info.SetValue(obj, DateTime.Parse(data));
                            break;
                        default:
                            throw new Exception("Unhanded Data Type Found: " + datatype);
                    }
                }

                dataList.Add(obj);
            }

            connection.Close();
            return dataList;
        }

        //Inserting data to table
        private static string getInsertQuery<T>(T obj)
        {
            Type type = typeof(T);

            StringBuilder builder = new StringBuilder();
            builder.Append("Insert Into ");
            builder.Append(type.Name);

            builder.Append(" (");
            Array.ForEach(type.GetFields(), info => builder.Append(info.Name + ","));

            builder.Remove(builder.Length - 1, 1);
            builder.Append(") ");

            builder.Append(" Values(");
            foreach (FieldInfo info in type.GetFields())
            {
                string datatype = info.FieldType.Name;
                switch (datatype)
                {
                    case "String":
                        builder.Append("'" + info.GetValue(obj) + "',");
                        break;
                    case "DateTime":
                    case "Nullable`1":
                        string date = Convert.ToDateTime(info.GetValue(obj)).ToString("yyyy-MM-dd");
                        if (Convert.ToDateTime(date) == DateTime.MinValue) date = "";
                        builder.Append("'" + date + "',");
                        break;
                    case "Int32":
                        builder.Append(info.GetValue(obj) + ",");
                        break;
                    default:
                        throw new Exception("Unhanded Data Type Found: " + datatype);
                }
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(");");

            return builder.ToString();
        }

        //Update specific data in table 
        private static string getUpdateQuery<T>(T obj, string key)
        {
            Type type = typeof(T);
            string keyValue = "";

            StringBuilder builder = new StringBuilder();
            builder.Append("Update ");
            builder.Append(type.Name);
            builder.Append(" Set ");

            foreach (FieldInfo info in type.GetFields())
            {
                string datatype = info.FieldType.Name.ToString();
                builder.Append(info.Name);
                builder.Append(" = ");
                switch (datatype)
                {
                    case "String":
                        builder.Append("'" + info.GetValue(obj) + "',");
                        if (info.Name.Equals(key)) keyValue = "'" + info.GetValue(obj) + "'";
                        break;
                    case "DateTime":
                    case "Nullable`1":
                        //string date = GetFormatedDate((DateTime)info.GetValue(obj));
                        string date = Convert.ToDateTime(info.GetValue(obj)).ToString("yyyy-MM-dd");
                        if (Convert.ToDateTime(date) == DateTime.MinValue) date = "";
                        builder.Append("'" + date + "',");
                        if (info.Name.Equals(key)) keyValue = "'" + date + "'";
                        break;
                    case "Int32":
                        builder.Append(info.GetValue(obj) + ",");
                        if (info.Name.Equals(key)) keyValue = info.GetValue(obj).ToString();
                        break;
                    default:
                        throw new Exception("Unhanded Data Type Found: " + datatype);
                }
            }
            builder.Remove(builder.Length - 1, 1);

            builder.Append(" Where ");
            builder.Append(key);
            builder.Append(" = ");
            builder.Append(keyValue);
            builder.Append(";");

            return builder.ToString();
        }

        //Delete a record from table
        private static string getDeleteQuery<T>(string key, string value)
        {
            Type type = typeof(T);

            StringBuilder builder = new StringBuilder();
            builder.Append("Delete From ");
            builder.Append(type.Name);
            builder.Append(" Where ");
            builder.Append(key);
            builder.Append(" = ");
            builder.Append("'" + value + "'");
            builder.Append(";");

            return builder.ToString();
        }

        #endregion

        #region Test DataBase Connection
        public static void TestConnection()
        {
            try
            {
                connection.Open();
                connection.Close();
            }catch(Exception e)
            {
                MessageBox.Show("Error occured while connecting to database" + e.Message);
            }
        }
        #endregion
    }
}
