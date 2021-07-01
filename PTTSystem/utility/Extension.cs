using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.utility
{
    public static class Extension
    {
        public static Object ConvertToType(this object objvalue , object modeltype)
        {
            if (objvalue != null)
            {
                Object result;

                if (modeltype.ToString().Equals("System.DateTime"))
                {
                    try
                    {
                        result = DateTime.Parse(((DateTime)objvalue).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch
                    {
                        result = DateTime.Parse(objvalue.ToString());

                    }
                }
                else if (modeltype.ToString().Equals("Nullable`1"))
                {
                    result = Guid.Parse(objvalue.ToString());
                }
                else if (modeltype.ToString().Equals("System.Guid"))
                {
                    result = Guid.Parse(objvalue.ToString());
                }
                else
                {
                    result = objvalue.ToString();
                }
                return result;
            }
            else
            {
                return null;
            }
            //Object result;
            //if (obj != null)
            //{
            //    if (obj.ToString().Contains("System.DateTime"))
            //    {
            //        //result = DateTime.Parse(((DateTime)str).ToString("yyyy-MM-dd"));
            //        result = DateTime.Parse(((DateTime)obj).ToString("yyyy-MM-dd"));
            //    }
            //    else if (obj.ToString().Equals("System.Guid"))
            //    {
            //        result = (Guid)obj;
            //    }
            //    else
            //    {
            //        result = obj.ToString();
            //    }
            //    return result;
            //}
            //else
            //{
            //    return null;
            //}
        }

    }
}
