using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class BodyModels
    {
        public FileModels filemodels { get; set; } = new FileModels();
        public Dictionary<string, string> parameters { get; set; }
    }

    public class FileModels
    {
        public string filepath { get; set; }
        public string filetype { get; set; }
        public string filename { get; set; }
    }
}
