using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    class KeywordTable
    {
        public Guid keywordGUID { get; set; }
        public Guid userGUID { get; set; }
        public String keywordString { get; set; }

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

    }
}
