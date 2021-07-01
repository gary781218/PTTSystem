using CCWin.Win32;
using PTTSystem.Function;
using PTTSystem.Model;
using PTTSystem.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace PTTSystem
{
    public partial class AccountForm : Form
    {
        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer leaveFocusTimer;
        Control validControlMessage;
        String showtext = "";
        AccountCreateValid valid = new AccountCreateValid();
        Account account = new Account();

        List<Account> result;

        public AccountForm()
        {
            InitializeComponent();
            this.Text = "新增帳號";
            send_button.Text = "送出";
            send_button.Click += new System.EventHandler(this.CreateAccountOK_Click);

            aTimer = new System.Timers.Timer(3000);
            aTimer.Elapsed += new ElapsedEventHandler(Run);
            Console.WriteLine(birthday.Value);
        }

        //修改個資用
        public AccountForm(Guid guid)
        {
            InitializeComponent();
            this.Text = "修改個資";
            send_button.Text = "修改";
            this.send_button.Click += new System.EventHandler(UpdateAccountOK_Click);
            this.Size = new Size(463, 376);
            this.send_button.Location = new Point(320,280);

            aTimer = new System.Timers.Timer(3000);
            aTimer.Elapsed += new ElapsedEventHandler(Run);

            Database db = new Database();
            Dictionary<string, Object> dict = new Dictionary<string, Object>();
            dict.Add("userGUID", guid);
            String sqlString = "select * from Account where userGUID = @userGUID";
            result = db.query<Account>(sqlString, dict);

            if (result.Count == 1)
            {
                account.userGUID = guid;
                this.Controls.Remove(groupBox1);
                groupBox2.Location = new Point(12, 12);
                userFName_textBox.Text = result[0].familyName;
                userLName_textBox.Text = result[0].lastName;
                aka_textBox.Text = result[0].userAKA;
                birthday.Text = result[0].birthday.ToString();
                mail_textBox.Text = result[0].mail;
                userFName_textBox.ReadOnly = true;
                userLName_textBox.ReadOnly = true;
            }
        }

        private void textBox_LeaveFocus(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (!tbox.Text.Trim().Equals(""))
            {
                leaveFocusTimer = new System.Timers.Timer(1);
                leaveFocusTimer.Elapsed += new ElapsedEventHandler(CurrentRun);
                leaveFocusTimer.Enabled = true;
            }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            CreateUdrtCheck check = new CreateUdrtCheck();
            if (!tbox.Text.Equals(""))
            {
                if (tbox.Name.Equals("account_textBox"))
                {
                    accountValid_label.Text = "";
                    validControlMessage = accountValid_label;
                    (string, bool) check_result = check.accountCheckStatus(tbox.Text);
                    //顯示結果在前端

                    showtext = check_result.Item1;
                    //驗證bool寫入物件，後續按送出可直接驗證
                    //textBox內容寫入物件，後續按送出可直接寫到資料庫
                    if (check_result.Item2)
                    {
                        valid.account = true;
                        account.userName = account_textBox.Text;
                    }
                    else
                    {
                        valid.account = false;
                        account.userName = "";
                    }
                }
                else if (tbox.Name.Equals("password_textBox"))
                {
                    pwdValid_label.Text = "";
                    validControlMessage = pwdValid_label;
                    (string, bool) check_result = check.pwdCheckStatus(tbox.Text);
                    showtext = check_result.Item1;
                    if (check_result.Item2)
                    {
                        account.passWord = password_textBox.Text;
                        valid.pwd = true;
                    }
                    else
                    {
                        valid.pwd = false;
                    }
                }
                else if (tbox.Name.Equals("pwdDoubleCheck_textBox"))
                {

                    pwdDoubleValid_label.Text = "";
                    validControlMessage = pwdDoubleValid_label;
                    (string, bool) check_result = check.pwdDoubleCheckStatus(password_textBox.Text, password_textBox.Text);

                    showtext = check_result.Item1;
                    if (check_result.Item2)
                    {
                        valid.pwdDoubleCheck = true;
                    }
                    else
                    {
                        valid.pwdDoubleCheck = false;
                        account.passWord = "";
                    }
                }
                else if (tbox.Name.Equals("mail_textBox"))
                {
                    mailCheck_label.Text = "";
                    validControlMessage = mailCheck_label;
                    (string, bool) check_result = check.mailCheckStatus(tbox.Text);
                    showtext = check_result.Item1;
                    if (check_result.Item2)
                    {
                        valid.mail = true;
                        account.mail = mail_textBox.Text;
                    }
                    else
                    {
                        valid.pwdDoubleCheck = false;
                        account.passWord = "";
                    }
                }
            }
            aTimer.Enabled = true;
        }
        public void Run(object sender, ElapsedEventArgs e)
        {
            validControlMessage.Invoke(new EventHandler(delegate
            {
                validControlMessage.Text = showtext;
                aTimer.Enabled = false;
            }));
        }
        public void CurrentRun(object sender, ElapsedEventArgs e)
        {
            validControlMessage.Invoke(new EventHandler(delegate
            {
                validControlMessage.Text = showtext;
                leaveFocusTimer.Enabled = false;
            }));
        }
        private void birthday_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            DateTimePicker dtp = (DateTimePicker)sender;
            if (dtp.Value.Date != now.Date)
            {
                valid.birthday = true;
                account.birthday = DateTime.Parse(dtp.Value.Date.ToString("yyyy-MM-dd"));
                Console.WriteLine(dtp.Value.Date);
            }
        }
        private void userFName_textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (!tbox.Text.Equals(""))
            {
                valid.familyNameCheck = true;
                account.familyName = tbox.Text;
            }
        }
        private void userLName_textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (!tbox.Text.Equals(""))
            {
                valid.lastNameCheck = true;
                account.lastName = tbox.Text;
            }
        }
        private void aka_textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (!tbox.Text.Equals(""))
            {
                account.userAKA = tbox.Text;
            }
        }
        private void CreateAccountOK_Click(object sender, EventArgs e)
        {
            bool sendOK = true;
            PropertyInfo[] props = valid.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (!(bool)prop.GetValue(valid))
                {
                    sendOK = false;
                }
                Console.WriteLine(prop.GetValue(valid).ToString());    
            }
            Console.WriteLine($"sendOK: {sendOK}");
            if (sendOK)
            {
                Database db = new Database();
                account.userGUID = Guid.NewGuid();
                account.userName = account_textBox.Text;
                Hash hash = new Hash();
                Dictionary<string, string> dict = hash.getResult(password_textBox.Text, 12);
                account.salt = dict["salt"].ToString();
                account.passWord = dict["pwd_salt_hash"].ToString();
                account.familyName = userFName_textBox.Text;
                account.lastName = userLName_textBox.Text;
                account.birthday = (DateTime)birthday.Value.ConvertToType("System.DateTime");
                account.mail = mail_textBox.Text;
                account.errorDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                account.createTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                account.lastLoginDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.insert(account);

                MessageBox.Show("已建立帳號");
                this.Close();
            }
            else 
            {
                MessageBox.Show("請檢查填寫格式是否完全正確");
            }
        }

        private void UpdateAccountOK_Click(object sender, EventArgs e)
        {
            if (result.Count==1)
            {
                account.userAKA = aka_textBox.Text;
                account.birthday = birthday.Value;
                account.mail = mail_textBox.Text;
                account.errorDate = result[0].errorDate;
                account.errorTimes = result[0].errorTimes;
                account.createTime = result[0].createTime;
                account.lastLoginDate = result[0].lastLoginDate;

                Database db = new Database();
                db.update(account);
                MessageBox.Show("已修改完成");
                this.Close();
            }
        }
    }
}
