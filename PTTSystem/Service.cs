using PTTSystem.Function;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem
{
    class Service
    {
        //NumberCheck check = new NumberCheck();

        //public List<Block> getBlocks()
        //{
        //    //驗證取得
        //    String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
        //    Database db = new Database(DBConnectCommand);
        //    List<Block> result = db.query<Block>("Select * from Block order by popularity DESC");
        //    db.Close();
        //    return result;
        //}

        //public List<Article> getArticles(string block_ID)
        //{
        //    //驗證取得
        //    String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
        //    Database db = new Database(DBConnectCommand);
        //    Dictionary<string, object> dict = new Dictionary<string, object>();

        //    dict.Add("block_ID", block_ID.ToString());
        //    List<Article> result = db.query<Article>("Select * from Article where block_ID = @block_ID and article_title not like '%公告%' order by dateTime", dict);
        //    db.Close();
        //    return result;
        //}

        //public List<Article> getArticles(string block_ID, int offset, int fetch = 20)
        //{
        //    //驗證取得
        //    String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
        //    Database db = new Database(DBConnectCommand);
        //    Dictionary<string, object> dict = new Dictionary<string, object>();

        //    dict.Add("block_ID", block_ID.ToString());
        //    dict.Add("start", offset);
        //    dict.Add("end", fetch);
        //    List<Article> result = db.query<Article>(@"Select * from Article
        //                                             where block_ID = @block_ID and article_title not like '%公告%' 
        //                                             order by dateTime
        //                                             offset @start rows fetch next @end rows only" , dict);
        //    db.Close();
        //    return result;
        //}

        //public List<Article> getContent(string article_ID)
        //{
        //    //驗證取得
        //    String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
        //    Database db = new Database(DBConnectCommand);
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    dict.Add("article_ID", article_ID.ToString());
        //    List<Article> result = db.query<Article>("Select * from Article where article_ID = @article_ID", dict);
        //    db.Close();
        //    return result;
        //}


        //public List<Model.Message> getMessage(string article_ID)
        //{
        //    //驗證取得
        //    String DBConnectCommand = ConfigurationManager.AppSettings["DBConnectCommand_PTTSysytm"];
        //    Database db = new Database(DBConnectCommand);
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    dict.Add("article_ID", article_ID.ToString());
        //    List<Model.Message> result = db.query<Model.Message>("Select * from Message where article_ID = @article_ID", dict);
        //    db.Close();
        //    return result;
        //}
    }
}
