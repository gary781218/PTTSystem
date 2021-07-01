using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class Article
    {
        public String article_ID { get; set; }
        public String article_title { get; set; }
        public String article_content { get; set; }
        public String  popularity { get; set; }
        public String href { get; set; }
        public string user_GUID { get; set; }
        public String user_ID { get; set; }
        public string block_ID { get; set; }
        public bool isannouncemet { get; set; }
        public string date { get; set; }
        public string createdate { get; set; }
        public bool isdelete { get; set; }

        public List<Article> getArticles(string block_ID)
        {
            //驗證取得
            String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
            Database db = new Database(DBConnectCommand);
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("block_ID", block_ID.ToString());
            List<Article> result = db.query<Article>("Select * from Article where block_ID = @block_ID and article_title not like '%公告%' order by dateTime", dict);
            db.Close();
            return result;
        }
        public List<Article> getArticles(string block_ID, int offset, int fetch = 20)
        {
            //驗證取得
            Database db = new Database();
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("block_ID", block_ID.ToString());
            dict.Add("start", offset);
            dict.Add("end", fetch);
            List<Article> result = db.query<Article>(@"Select * from Article
                                                     where block_ID = @block_ID and article_title not like '%公告%' 
                                                     order by dateTime
                                                     offset @start rows fetch next @end rows only", dict);
            db.Close();
            return result;
        }

        public int getArticleCount(string block_ID)
        {
            //驗證取得
            String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
            Database db = new Database(DBConnectCommand);
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("block_ID", block_ID.ToString());
            int result = db.queryBaseType<int>(@"Select count(*) as count from Article
                                                     where block_ID = @block_ID and article_title not like '%公告%'", dict)[0];
            db.Close();
            return result;
        }
        public List<Article> getContent(string article_ID)
        {
            //驗證取得
            Database db = new Database();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("article_ID", article_ID.ToString());
            List<Article> result = db.query<Article>("Select * from Article where article_ID = @article_ID", dict);
            db.Close();
            return result;
        }
    }
}
