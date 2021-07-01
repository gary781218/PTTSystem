using CCWin.SkinClass;
using CCWin.SkinControl;
using PTTSystem.Function;
using PTTSystem.utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem
{
    class Database
    {
        SqlConnection sqlconnection;

        public Database()
        {
            string connString = "Data Source = localhost; Initial Catalog = PTTSystem; Integrated Security = SSPI";
            sqlconnection = new SqlConnection(connString);
        }

        public Database(String connString)
        {
            sqlconnection = new SqlConnection(connString);
            //sqlconnection.Open();
        }

        public void Close()
        {
            sqlconnection.Close();
        }

        //傳入T取全部
        //不傳入T，透過sql與sql injection組合

        public List<T> queryBaseType<T>(String sqlstring, Dictionary<string, object> dict = null) 
        {
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand(sqlstring, sqlconnection);
            if (dict != null)
            {
                foreach (var parameter in dict)
                {
                    sqlcommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    //Console.WriteLine($"{parameter.Key} {parameter.Value}");
                }
            }

            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<T> list = new List<T>();

            while (reader.Read())
            {
                Console.WriteLine(typeof(T));
                if (typeof(T) == typeof(String))
                {
                    list.Add((T)Convert.ChangeType(reader.GetString(0), typeof(T)));
                }
                else if (typeof(T) == typeof(Int32))
                {
                    list.Add((T)Convert.ChangeType(reader.GetInt32(0), typeof(T)));
                }
                else if (typeof(T) == typeof(Double))
                {
                    list.Add((T)Convert.ChangeType(reader.GetDouble(0), typeof(T)));
                }
                else if (typeof(T) == typeof(Boolean))
                {
                    list.Add((T)Convert.ChangeType(reader.GetBoolean(0), typeof(T)));
                }
                else
                {
                    throw new InvalidOperationException("Type not supported");
                }
            }
            sqlconnection.Close();
            return list;
        }

        public List<T> query<T>(String sqlstring, Dictionary<string, object> dict = null)  where T : new()
        {
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand(sqlstring, sqlconnection);
            if (dict != null)
            {
                foreach (var parameter in dict)
                {
                    sqlcommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            SqlDataReader reader = sqlcommand.ExecuteReader();
            List<T> list = new List<T>();

            while (reader.Read())
            {
                #region 暫時放著
                //if (typeof(T) == typeof(string))
                //{
                //    list.Add((T)Convert.ChangeType(reader.GetString(0), typeof(T)));
                //}
                //else if (typeof(T) == typeof(Int32))
                //{
                //    list.Add((T)Convert.ChangeType(reader.GetInt32(0), typeof(T)));
                //}
                //else if (typeof(T) == typeof(Double))
                //{
                //    list.Add((T)Convert.ChangeType(reader.GetDouble(0), typeof(T)));
                //}
                //else if (typeof(T) == typeof(Boolean))
                //{
                //    list.Add((T)Convert.ChangeType(reader.GetBoolean(0), typeof(T)));
                //}
                //if (t.GetType().IsValueType)
                //{
                //    //switch (t.GetType().Name)
                //    //{
                //    //    case "String":
                //    //        list.Add((T)Convert.ChangeType(reader.GetString(0), typeof(T)));
                //    //        break;
                //    //    case "Int32":
                //    //        list.Add((T)Convert.ChangeType(reader.GetInt32(0), typeof(T)));
                //    //        break;
                //    //    case "Double":
                //    //        list.Add((T)Convert.ChangeType(reader.GetDouble(0), typeof(T)));
                //    //        break;
                //    //    case "Boolean":
                //    //        list.Add((T)Convert.ChangeType(reader.GetBoolean(0), typeof(T)));
                //    //        break;
                //    //}
                //}
                #endregion
                T t = new T();
                if (!t.GetType().IsValueType)
                {
                    PropertyInfo[] props = t.GetType().GetProperties();
                    String columnName = "";
                    //ConvertObjectType convert = new ConvertObjectType();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columnName = reader.GetName(i);
                        foreach (var prop in props)
                        {
                            if (prop.Name.Equals(columnName))
                            {
                                Object data;
                                //Console.WriteLine(prop.PropertyType.ToString());
                                //Console.WriteLine(reader.GetSqlValue(i).ToString());
                                data = reader.GetSqlValue(i).ConvertToType(prop.PropertyType);
             
                                prop.SetValue(t, data.IsNull() ? null : Convert.ChangeType(data, prop.PropertyType));
                            }
                        }
                    }
                    list.Add(t);
                }
            }
            sqlconnection.Close();
            return list;
        }

        public void insert(Object obj)
        {
            sqlconnection.Open();
            ModelCheck modelcheck = new ModelCheck();
            bool hasPK = modelcheck.hasPK(obj);
            bool hasNull = modelcheck.checkCantNullColumn(obj);

            //T t = new T();
            PropertyInfo[] props = obj.GetType().GetProperties();

            if (hasPK && !hasNull)
            {
                StringBuilder tables = new StringBuilder("");
                StringBuilder values = new StringBuilder("");
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < props.Length; i++)
                {
                    tables.AppendFormat($"{props[i].Name}{','}");
                    values.AppendFormat($"{'@'}{props[i].Name}{','}");
                    dict.Add(props[i].Name, props[i].GetValue(obj));
                }

                string table = tables.ToString().TrimEnd(',');
                string value = values.ToString().TrimEnd(',');
                string sqlstring = $"insert into {obj.GetType().ToString().Split('.').Last()} ({table}) values({value})";
                Console.WriteLine($"語法:{sqlstring}");
                SqlCommand sqlcommand = new SqlCommand(sqlstring, sqlconnection);
                foreach (var parameter in dict)
                {
                    sqlcommand.Parameters.AddWithValue(parameter.Key, parameter.Value == null ? DBNull.Value : parameter.Value);
                }
                sqlcommand.ExecuteNonQuery();
                sqlconnection.Close();
            }
        }

        public void update(Object obj)
        {
            sqlconnection.Open();
            PropertyInfo[] props = obj.GetType().GetProperties();

            Dictionary<string, object> dict_temp = new Dictionary<string, object>();
            dict_temp.Add("TABLE_NAME", obj.GetType().ToString().Split('.').Last());

            Database db_update = new Database(ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"]);

            string PK = db_update.queryBaseType<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = @TABLE_NAME", dict_temp)[0];

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("pk_Value", props.Where(x => x.Name.Equals(PK)).FirstOrDefault().GetValue(obj));
            string sqlstring = $"SELECT COUNT(*) FROM {obj.GetType().ToString().Split('.').Last()} WHERE {PK} = @pk_Value";
            int checkIsExist = db_update.queryBaseType<int>(sqlstring, dict)[0];
            db_update.Close();

            if (checkIsExist == 1)
            {
                StringBuilder str = new StringBuilder("");
                dict.Clear();
                for (int i = 0; i < props.Length; i++)
                {
                    //判斷非null
                    if (props[i].GetValue(obj) != null)
                    {
                        if (props[i].Name.Equals(PK))
                        {
                            dict.Add("pk", props.Where(x => x.Name.Equals(PK)).FirstOrDefault().GetValue(obj));
                        }
                        else
                        {
                            str.AppendFormat($"{props[i].Name}={'@'}{props[i].Name}{','}");

                            dict.Add(props[i].Name, props[i].GetValue(obj).ConvertToType(props[i].PropertyType));
                        }
                    }
                }

                string value = str.ToString().TrimEnd(',');
                sqlstring = $@"update {obj.GetType().ToString().Split('.').Last()} 
                                  set {value}
                                  where {PK} = @pk ";
                SqlCommand sqlcommand = new SqlCommand(sqlstring, sqlconnection);
                foreach (var parameter in dict)
                {
                    sqlcommand.Parameters.AddWithValue(parameter.Key, parameter.Value == null ? DBNull.Value : parameter.Value);
                }
                sqlcommand.ExecuteNonQuery();
            }
            sqlconnection.Close();
        }
    }
}
