using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class Block
    {
        public Guid block_ID { get; set; }
        public String name { get; set; }
        public String popularity { get; set; }
        public String cName { get; set; }
        public String description { get; set; }
        public String url { get; set; }
        public DateTime createTime 
        {
            get 
            {
                return DateTime.Now;
            }
            set
            { 
            }
        }
        public bool isDelete { get; set; }

        public List<Block> getBlocks()
        {
            //驗證取得
            Database db = new Database();
            List<Block> result = db.query<Block>("Select * from Block order by popularity DESC");
            db.Close();
            return result;
        }
    }
}
