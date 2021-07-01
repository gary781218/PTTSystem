using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.utility
{
    class ConvertObjectType
    {
        public Object ChangeObjectType(object modeltype, object objvalue)
        {
            if (objvalue != null)
            {
                Object result;

                if (modeltype.ToString().Equals("System.DateTime"))
                {
                    try
                    {
                        result = DateTime.Parse(((DateTime)objvalue).ToString("yyyy-MM-dd"));
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
        }
    }
}
