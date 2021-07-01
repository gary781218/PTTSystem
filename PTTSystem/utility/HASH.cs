using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTSystem.utility
{
    class Hash
    {
        public Dictionary<string, string> getResult(string password, int count)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            //Console.WriteLine($"salt:{salt}");
            string pwd_salt = password + salt;
            //Console.WriteLine($"加鹽的密碼:{salt}");
            string pwd_salt_hash = BCrypt.Net.BCrypt.HashPassword(pwd_salt, count);
            Console.WriteLine($"雜湊的密碼:{pwd_salt_hash}");

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("salt", salt);
            dict.Add("pwd_salt_hash", pwd_salt_hash);

            return dict;
        }
    }
}
