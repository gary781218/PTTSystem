using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.Model
{
    public class ResponseArrayModels
    {
        public ImageModels[] data { get; set; } = null;
        public bool success { get; set; }
        public string status { get; set; }
    }

    public class ImageModels
    {
        public string id { get; set; }
        public string deletehash { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public int bandwidth { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public object nsfw { get; set; }
        public object section { get; set; }
        public string account_url { get; set; }
        public string account_id { get; set; }
        public bool is_ad { get; set; }
        public bool in_most_viral { get; set; }
        public bool has_sound { get; set; }
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public int edited { get; set; }
        public bool in_gallery { get; set; }
        public string link { get; set; }
    }
}
