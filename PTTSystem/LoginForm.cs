using CCWin;
using PTTSystem.Component;
using PTTSystem.Function;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem
{
    public partial class LoginForm : Skin_Color
    {
        //公用參數區
        Rectangle screen;
        DateConvert dataConver;
        int locationX_AccountLabel, locationY_AccountLabel;
        TextBoxModulation tBoxMod;
        TextBoxModulation password_tbox;

        public LoginForm()
        {
            InitializeComponent();
            screen = Screen.FromControl(this).Bounds;
            dataConver = new DateConvert();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }
        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //Form視窗長寬高設定
            Width = dataConver.SizeZoom(screen.Width,0.9);
            Height = dataConver.SizeZoom(screen.Height, 0.95);
            Location = new Point(25, 25);

            //登入首頁圖片顯示
            int width_Pbox = dataConver.SizeZoom(Width, 0.7);
            int height_Pbox = dataConver.SizeZoom(Height, 0.6);
            int locationX_Pbox = Width / 2 - dataConver.SizeZoom(Width, 0.7) / 2;
            int locationY_Pbox = 55;
            string indexPicturePath = ConfigurationManager.AppSettings["indexPicture"];
            PictureBoxModulation pictureBoxMod = new PictureBoxModulation(
                indexPicturePath,
                width_Pbox,
                height_Pbox,
                locationX_Pbox,
                locationY_Pbox
                );

            //帳號密碼輸入元件設定與顯示
            locationX_AccountLabel = locationX_Pbox;
            locationY_AccountLabel = locationY_Pbox + height_Pbox + 10;
            LabelModulation labelMod = new LabelModulation(
                "請輸入代號，或以guest參觀，或以new註冊:",
                20,
                locationX_AccountLabel,
                locationY_AccountLabel
                );

            tBoxMod = new TextBoxModulation(
                "",
                16,
                locationX_Pbox + labelMod.Size.Width + 430,
                locationY_Pbox + height_Pbox + 8,
                220,
                30,
                false
                );
            tBoxMod.KeyDown += Account_Enter_KeyDown;

            this.Controls.Add(pictureBoxMod);
            this.Controls.Add(labelMod);
            this.Controls.Add(tBoxMod);
        }

        //事件
        private void Account_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter && !tbox.Text.Trim().Equals(""))
            {
                if (tbox.Text.ToLower().Equals("new"))
                {
                    AccountForm form = new AccountForm();
                    form.ShowDialog(this);
                }
                else if (tbox.Text.ToLower().Equals("guest"))
                {
                    BlockForm form = new BlockForm();
                    form.ShowDialog(this);
                }
                else
                {
                    bool accountIsAlive = new Account().userValid(tBoxMod.Text);
                    if (accountIsAlive)
                    {
                        LabelModulation password_label = new LabelModulation("密碼:", 20, locationX_AccountLabel, locationY_AccountLabel + 40);
                        password_tbox = new TextBoxModulation("", 20, password_label.Location.X + 70, password_label.Location.Y, 250, 30, false);
                        password_tbox.PasswordChar = '*';
                        password_tbox.KeyDown += Password_Enter_KeyDown;

                        this.Controls.Add(password_label);
                        this.Controls.Add(password_tbox);
                        password_tbox.Focus();
                    }
                    else
                    {
                        MessageBox.Show("無此帳號");   
                    }
                }
            }
        }
        private void Password_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxModulation password_tbox = (TextBoxModulation)sender;
            if (e.KeyCode == Keys.Enter && !password_tbox.Text.Trim().Equals(""))
            {
                //至資料庫驗證
                Account account = new Account();
                bool pwdcheck = account.pwdValid(tBoxMod.Text, password_tbox.Text);
                Account result = account.getAccount(tBoxMod.Text).FirstOrDefault();

                //依據驗證結果呈現
                if (result.isBanned == false)
                {
                    if (pwdcheck)
                    {
                        account.validSafeChange(result);
                        BlockForm form = new BlockForm(result);
                        form.ShowDialog(this);
                    }
                    else
                    {
                        int errorTime = account.validNotSafeChange(result);
                        MessageBox.Show($"請重新確認密碼，你已錯誤{errorTime}次，3次將會鎖定帳號");
                    }
                }
                else
                {
                    MessageBox.Show($"該帳戶已被鎖定，請洽管理員");
                }
               
            }
        }
       
    }
}
