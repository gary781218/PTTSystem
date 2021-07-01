using CCWin.SkinControl;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Function
{
    class DateConvert
    {
        public int SizeZoom(int unit, double zoomPercent)
        {
            return (int)(unit * zoomPercent);
        }
    }

    class NumberCheck
    {
        public List<int> BlockPopulartyColor(String popularty)
        {
            List<int> list = new List<int>();
            if (!popularty.Equals("爆") && !popularty.Equals("") && popularty!=null )
            {
                int num = int.Parse(popularty);
                if (num >= 10000)
                {
                    list.Add(102);
                    list.Add(255);
                    list.Add(255);
                }
                else if (num >= 5000 && num < 10000)
                {
                    list.Add(102);
                    list.Add(102);
                    list.Add(255);
                }
                else if (num >= 2000 && num < 5000)
                {
                    list.Add(255);
                    list.Add(102);
                    list.Add(102);
                }
                else
                {
                    list.Add(255);
                    list.Add(255);
                    list.Add(255);
                }
            }
            else
            {
                list.Add(255);
                list.Add(0);
                list.Add(0);
            }
            return list;
        }
        public List<int> ArticlePopulartyColor(String popularty)
        {
            List<int> list = new List<int>();
            if (!popularty.Equals("爆") && !popularty.Equals("") && !popularty.Contains("X"))
            {
                int num = int.Parse(popularty);
                if (num > 9 && num < 100)
                {
                    list.Add(255);
                    list.Add(255);
                    list.Add(102);
                }
                else
                {
                    list.Add(102);
                    list.Add(255);
                    list.Add(102);
                }
            }
            else if (popularty.Contains("X"))
            {
                list.Add(102);
                list.Add(102);
                list.Add(102);
            }
            else
            {
                list.Add(255);
                list.Add(0);
                list.Add(0);
            }

            return list;
        }
        public Dictionary<string, int> ArticlePagination(int totalArticleCount, int showOnPaginationCount)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (totalArticleCount <= showOnPaginationCount)
            {
                dict.Add("pageCount", 1);
                dict.Add("lastCount", totalArticleCount);
            }
            else
            {
                if (totalArticleCount % showOnPaginationCount == 0)
                {
                    dict.Add("pageCount", totalArticleCount / showOnPaginationCount);
                    dict.Add("lastCount", showOnPaginationCount);
                }
                else
                {
                    dict.Add("pageCount", totalArticleCount / showOnPaginationCount + 1);
                    dict.Add("lastCount", totalArticleCount % showOnPaginationCount);
                }
            }
            return dict;
        }
        public (int, int) GetArticleIndexFromPage(int page, int initialArticleCount, int maxArticeOnPage, int totalPage)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (page == 1)
            {
                return ((totalPage - page) * maxArticeOnPage, initialArticleCount);
            }
            else
            {
                return ((totalPage - page) * maxArticeOnPage, maxArticeOnPage);
            }
        }
        public class PushTagCheck
        {
            public List<int> PushTagColor(String popularty)
            {
                List<int> list = new List<int>();
                if (popularty.Trim().Equals("推"))
                {
                    list.Add(255);
                    list.Add(255);
                    list.Add(255);
                }
                else
                {
                    list.Add(255);
                    list.Add(102);
                    list.Add(102);
                }

                return list;
            }
        }

    }

    class ModelCheck
    {
        public bool hasPK(Object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();

            Dictionary<string, object> dict_temp = new Dictionary<string, object>();
            dict_temp.Add("TABLE_NAME", obj.GetType().ToString().Split('.').Last()); ;
            String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
            Database db = new Database(DBConnectCommand);

            string PK = db.queryBaseType<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = @TABLE_NAME", dict_temp)[0];
            bool hasPK = props.Where(x => x.Name.Equals(PK)).IsNull() ? false : true;
            db.Close();

            return hasPK;
        }

        public bool checkCantNullColumn(Object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();

            Dictionary<string, object> dict_temp = new Dictionary<string, object>();
            dict_temp.Add("TABLE_NAME", obj.GetType().ToString().Split('.').Last()); ;
            String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
            Database db = new Database(DBConnectCommand);

            bool hasNull = false;
            List<String> notNullColumns = db.queryBaseType<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns Where Table_Name = @TABLE_NAME And IS_NULLABLE = 'NO'", dict_temp);
            foreach (var prop in props)
            {
                if (notNullColumns.Contains(prop.Name) && prop.GetValue(obj).IsNull())
                {
                    hasNull = true;
                }
            }
            db.Close();

            return hasNull;
        }
    }

    class CreateUdrtCheck
    {
        public (String, bool) accountCheckStatus(String str)
        {
            (string,bool) result = ("",false);
            if (Regex.IsMatch(str, @"^[a-zA-Z]+[a-zA-Z0-9]{7,19}$"))
            {
                //驗證資料庫有沒有同樣的
                String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
                Database db = new Database(DBConnectCommand);

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("userName", str.Trim());
                string sqlcommand = "select count(userName) from [Account] where userName = @userName";
                List<int> list = db.queryBaseType<int>(sqlcommand, dict);
                int resultCount = list[0];
                db.Close();
                if (resultCount == 1)
                {
                    result = ("此帳號已有人使用", false);
                }
                else if (resultCount == 0)
                {
                    result = ("此帳號可以使用", true); 
                }
            }
            else
            {
                result = ("帳號不符合格式，請重新輸入", false);
            }
            return result;
        }
        public (String, bool) pwdCheckStatus(String str)
        {
            (string, bool) result = ("", false);
            if (Regex.IsMatch(str, @"^[a-zA-Z0-9]{8,20}$"))
            {
                result = ("密碼符合格式", true);
            }
            else
            {
                result = ("密碼不符合格式，請重新輸入",false);
            }
            return result;
        }
        public (String, bool) pwdDoubleCheckStatus(String pwdDouble, String pwdOrigin)
        {
            (string, bool) result = ("", false);
            if (pwdDouble.Equals(pwdOrigin))
            {
                result = ("密碼相同", true);
            }
            else
            {
                result = ("密碼不同相同",false);
            }
            return result;
        }
        public (String, bool) mailCheckStatus(String str)
        {
            (string, bool) result = ("", false);
            //^([A - Za - z0 - 9_\-\.]) +\@(163.com | qq.com | 42du.cn)$   限制哪個網域
            //^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$"))
            if (Regex.IsMatch(str, @"^([A-Za-z0-9_\-\.])+\@gmail.com"))
            {
                //驗證資料庫有沒有同樣的
                String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
                Database db = new Database(DBConnectCommand);

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("mail", str.Trim());
                string sqlcommand = "select count(userName) from [Account] where userName = @mail";
                List<int> list = db.queryBaseType<int>(sqlcommand, dict);
                int resultCount = list[0];
                db.Close();
                if (resultCount == 1)
                {
                    result = ("此mail已有人註冊", false);
                }
                else if (resultCount == 0)
                {
                    result = ("此mail可以使用", true);
                }
            }
            else
            {
                result = ("請確認信箱格式", false);
            }
            return result;
        }
    }
}