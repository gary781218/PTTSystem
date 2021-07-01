using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class Message
    {
        public String message_ID { get; set; }
        public String user_GUID { get; set; }
        public String user_ID { get; set; }
        public String article_ID { get; set; }
        public String message { get; set; }
        public String push { get; set; }
        public String pushFeedback { get; set; }
        public string ipdatetime { get; set; }
        public DateTime datetime { get; set; }
        public bool isdelete { get; set; }

        public List<Model.Message> getMessage(string article_ID)
        {
            //驗證取得
            Database db = new Database();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("article_ID", article_ID.ToString());
            List<Model.Message> result = db.query<Model.Message>("Select * from Message where article_ID = @article_ID", dict);
            db.Close();
            return result;
        }
    }
}
