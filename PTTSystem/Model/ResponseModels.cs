using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class ResponseModels
    {
        public ImageModels data { get; set; } = null;
        public bool success { get; set; }
        public string status { get; set; }
    }
}
