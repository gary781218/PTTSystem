using PTTSystem.utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Model
{
    public class Account
    {
        public Guid userGUID { get; set; }
        public String userName { get; set; }
        public String passWord { get; set; }
        public String userAKA { get; set; }
        public String familyName { get; set; }
        public String lastName { get; set; }
        public String mail { get; set; }
        public String salt { get; set; }
        public DateTime birthday { get; set; }
        public int errorTimes { get; set; }
        public DateTime errorDate { get; set; }
        public bool isBanned { get; set; }
        public int onlineDays { get; set; }
        public DateTime lastLoginDate { get; set; }
        public DateTime createTime { get; set; }
        public bool isDelete { get; set; }

        public bool userValid(string account)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("userName", account);

            Database db = new Database();
            List<Account> result = db.query<Account>("Select * from Account Where userName = @userName", dict);
            if (result.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool pwdValid(string account, string password)
        {
            Database db = new Database();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("userName", account);
            List<Account> result = db.query<Account>("Select * from Account Where userName = @userName", dict);

            bool isMatchpwdAndHashpwd = false;
            if (result.Count == 1)
            {
                string pwd = password + result[0].salt;
                isMatchpwdAndHashpwd = BCrypt.Net.BCrypt.Verify(pwd, result[0].passWord);
            }
            db.Close();

            return isMatchpwdAndHashpwd;
        }

        public List<Account> getAccount(string account)
        {
            Database db = new Database();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("userName", account);
            List<Account> result = db.query<Account>("Select * from Account Where userName = @userName", dict);
            db.Close();

            if (result.Count == 1)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void validSafeChange(Account account)
        {
            Database db = new Database();
            int onlineDays = 0;

            if (account.onlineDays == 0)
            {
                onlineDays=1;
            }
            else
            {
                DateTime date1 = new DateTime(account.lastLoginDate.Year, account.lastLoginDate.Month, account.lastLoginDate.Day, 0, 0, 0);
                DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                onlineDays = DateTime.Compare(date1, date2) == 0 ? account.onlineDays : account.onlineDays + 1;
            }

            account.onlineDays = onlineDays;
            account.errorTimes = 0;
            account.lastLoginDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            db.update(account);
            db.Close();
        }


        public int validNotSafeChange(Account account)
        {
            Database db = new Database();

            int errorTimes = account.errorTimes;

            if (errorTimes <= 1)
            {
                account.errorTimes = errorTimes + 1;
                account.errorDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (errorTimes == 2)
            {
                account.isBanned = true;
                account.errorTimes = errorTimes + 1;
                account.errorDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            db.update(account);
            db.Close();
            return account.errorTimes;
        }
    }
}
