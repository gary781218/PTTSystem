namespace PTTSystem
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.account_label = new System.Windows.Forms.Label();
            this.account_textBox = new System.Windows.Forms.TextBox();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.pwdDoubleCheck_textBox = new System.Windows.Forms.TextBox();
            this.pwdCheck_label = new System.Windows.Forms.Label();
            this.userFName_textBox = new System.Windows.Forms.TextBox();
            this.userFName_label = new System.Windows.Forms.Label();
            this.userLName_textBox = new System.Windows.Forms.TextBox();
            this.userLName_label = new System.Windows.Forms.Label();
            this.aka_textBox = new System.Windows.Forms.TextBox();
            this.aka_label = new System.Windows.Forms.Label();
            this.birthday_label = new System.Windows.Forms.Label();
            this.mail_textBox = new System.Windows.Forms.TextBox();
            this.mail_label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pwdDoubleValid_label = new System.Windows.Forms.Label();
            this.pwdValid_label = new System.Windows.Forms.Label();
            this.accountValid_label = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.mailValid_label = new System.Windows.Forms.Label();
            this.mailCheck_label = new System.Windows.Forms.Label();
            this.send_button = new System.Windows.Forms.Button();
            this.password_label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // account_label
            // 
            this.account_label.AutoSize = true;
            this.account_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.account_label.Location = new System.Drawing.Point(28, 35);
            this.account_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.account_label.Name = "account_label";
            this.account_label.Size = new System.Drawing.Size(94, 32);
            this.account_label.TabIndex = 0;
            this.account_label.Text = "*帳號";
            // 
            // account_textBox
            // 
            this.account_textBox.Location = new System.Drawing.Point(88, 34);
            this.account_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.account_textBox.Name = "account_textBox";
            this.account_textBox.Size = new System.Drawing.Size(133, 22);
            this.account_textBox.TabIndex = 1;
            this.account_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.account_textBox.Leave += new System.EventHandler(this.textBox_LeaveFocus);
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(88, 75);
            this.password_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.PasswordChar = '*';
            this.password_textBox.Size = new System.Drawing.Size(133, 22);
            this.password_textBox.TabIndex = 3;
            this.password_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.password_textBox.Leave += new System.EventHandler(this.textBox_LeaveFocus);
            // 
            // pwdDoubleCheck_textBox
            // 
            this.pwdDoubleCheck_textBox.Location = new System.Drawing.Point(102, 117);
            this.pwdDoubleCheck_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.pwdDoubleCheck_textBox.Name = "pwdDoubleCheck_textBox";
            this.pwdDoubleCheck_textBox.PasswordChar = '*';
            this.pwdDoubleCheck_textBox.Size = new System.Drawing.Size(133, 22);
            this.pwdDoubleCheck_textBox.TabIndex = 5;
            this.pwdDoubleCheck_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.pwdDoubleCheck_textBox.Leave += new System.EventHandler(this.textBox_LeaveFocus);
            // 
            // pwdCheck_label
            // 
            this.pwdCheck_label.AutoSize = true;
            this.pwdCheck_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pwdCheck_label.Location = new System.Drawing.Point(-3, 118);
            this.pwdCheck_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pwdCheck_label.Name = "pwdCheck_label";
            this.pwdCheck_label.Size = new System.Drawing.Size(158, 32);
            this.pwdCheck_label.TabIndex = 4;
            this.pwdCheck_label.Text = "*密碼確認";
            // 
            // userFName_textBox
            // 
            this.userFName_textBox.Location = new System.Drawing.Point(83, 37);
            this.userFName_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.userFName_textBox.Name = "userFName_textBox";
            this.userFName_textBox.Size = new System.Drawing.Size(133, 22);
            this.userFName_textBox.TabIndex = 7;
            this.userFName_textBox.TextChanged += new System.EventHandler(this.userFName_textBox_TextChanged);
            // 
            // userFName_label
            // 
            this.userFName_label.AutoSize = true;
            this.userFName_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.userFName_label.Location = new System.Drawing.Point(23, 39);
            this.userFName_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userFName_label.Name = "userFName_label";
            this.userFName_label.Size = new System.Drawing.Size(94, 32);
            this.userFName_label.TabIndex = 6;
            this.userFName_label.Text = "*姓氏";
            // 
            // userLName_textBox
            // 
            this.userLName_textBox.Location = new System.Drawing.Point(83, 86);
            this.userLName_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.userLName_textBox.Name = "userLName_textBox";
            this.userLName_textBox.Size = new System.Drawing.Size(133, 22);
            this.userLName_textBox.TabIndex = 9;
            this.userLName_textBox.TextChanged += new System.EventHandler(this.userLName_textBox_TextChanged);
            // 
            // userLName_label
            // 
            this.userLName_label.AutoSize = true;
            this.userLName_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.userLName_label.Location = new System.Drawing.Point(23, 87);
            this.userLName_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userLName_label.Name = "userLName_label";
            this.userLName_label.Size = new System.Drawing.Size(94, 32);
            this.userLName_label.TabIndex = 8;
            this.userLName_label.Text = "*名字";
            // 
            // aka_textBox
            // 
            this.aka_textBox.Location = new System.Drawing.Point(83, 126);
            this.aka_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.aka_textBox.Name = "aka_textBox";
            this.aka_textBox.Size = new System.Drawing.Size(133, 22);
            this.aka_textBox.TabIndex = 11;
            this.aka_textBox.TextChanged += new System.EventHandler(this.aka_textBox_TextChanged);
            // 
            // aka_label
            // 
            this.aka_label.AutoSize = true;
            this.aka_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.aka_label.Location = new System.Drawing.Point(34, 127);
            this.aka_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.aka_label.Name = "aka_label";
            this.aka_label.Size = new System.Drawing.Size(79, 32);
            this.aka_label.TabIndex = 10;
            this.aka_label.Text = "暱稱";
            // 
            // birthday_label
            // 
            this.birthday_label.AutoSize = true;
            this.birthday_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.birthday_label.Location = new System.Drawing.Point(23, 169);
            this.birthday_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.birthday_label.Name = "birthday_label";
            this.birthday_label.Size = new System.Drawing.Size(94, 32);
            this.birthday_label.TabIndex = 12;
            this.birthday_label.Text = "*生日";
            // 
            // mail_textBox
            // 
            this.mail_textBox.Location = new System.Drawing.Point(83, 210);
            this.mail_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.mail_textBox.Name = "mail_textBox";
            this.mail_textBox.Size = new System.Drawing.Size(152, 22);
            this.mail_textBox.TabIndex = 15;
            this.mail_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.mail_textBox.Leave += new System.EventHandler(this.textBox_LeaveFocus);
            // 
            // mail_label
            // 
            this.mail_label.AutoSize = true;
            this.mail_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.mail_label.Location = new System.Drawing.Point(23, 211);
            this.mail_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mail_label.Name = "mail_label";
            this.mail_label.Size = new System.Drawing.Size(94, 32);
            this.mail_label.TabIndex = 14;
            this.mail_label.Text = "*信箱";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pwdDoubleValid_label);
            this.groupBox1.Controls.Add(this.pwdValid_label);
            this.groupBox1.Controls.Add(this.accountValid_label);
            this.groupBox1.Controls.Add(this.account_textBox);
            this.groupBox1.Controls.Add(this.account_label);
            this.groupBox1.Controls.Add(this.password_textBox);
            this.groupBox1.Controls.Add(this.password_label);
            this.groupBox1.Controls.Add(this.pwdDoubleCheck_textBox);
            this.groupBox1.Controls.Add(this.pwdCheck_label);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(415, 158);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帳密專區";
            // 
            // pwdDoubleValid_label
            // 
            this.pwdDoubleValid_label.AutoSize = true;
            this.pwdDoubleValid_label.Location = new System.Drawing.Point(259, 122);
            this.pwdDoubleValid_label.Name = "pwdDoubleValid_label";
            this.pwdDoubleValid_label.Size = new System.Drawing.Size(0, 12);
            this.pwdDoubleValid_label.TabIndex = 8;
            // 
            // pwdValid_label
            // 
            this.pwdValid_label.AutoSize = true;
            this.pwdValid_label.Location = new System.Drawing.Point(244, 79);
            this.pwdValid_label.Name = "pwdValid_label";
            this.pwdValid_label.Size = new System.Drawing.Size(0, 12);
            this.pwdValid_label.TabIndex = 7;
            // 
            // accountValid_label
            // 
            this.accountValid_label.AutoSize = true;
            this.accountValid_label.Location = new System.Drawing.Point(243, 38);
            this.accountValid_label.Name = "accountValid_label";
            this.accountValid_label.Size = new System.Drawing.Size(0, 12);
            this.accountValid_label.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.birthday);
            this.groupBox2.Controls.Add(this.mailValid_label);
            this.groupBox2.Controls.Add(this.mailCheck_label);
            this.groupBox2.Controls.Add(this.userFName_textBox);
            this.groupBox2.Controls.Add(this.userFName_label);
            this.groupBox2.Controls.Add(this.mail_textBox);
            this.groupBox2.Controls.Add(this.mail_label);
            this.groupBox2.Controls.Add(this.userLName_textBox);
            this.groupBox2.Controls.Add(this.userLName_label);
            this.groupBox2.Controls.Add(this.birthday_label);
            this.groupBox2.Controls.Add(this.aka_textBox);
            this.groupBox2.Controls.Add(this.aka_label);
            this.groupBox2.Location = new System.Drawing.Point(8, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(415, 253);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "會員資訊";
            // 
            // birthday
            // 
            this.birthday.Location = new System.Drawing.Point(83, 169);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(200, 22);
            this.birthday.TabIndex = 18;
            this.birthday.ValueChanged += new System.EventHandler(this.birthday_ValueChanged);
            // 
            // mailValid_label
            // 
            this.mailValid_label.AutoSize = true;
            this.mailValid_label.Location = new System.Drawing.Point(242, 216);
            this.mailValid_label.Name = "mailValid_label";
            this.mailValid_label.Size = new System.Drawing.Size(0, 12);
            this.mailValid_label.TabIndex = 17;
            // 
            // mailCheck_label
            // 
            this.mailCheck_label.AutoSize = true;
            this.mailCheck_label.Location = new System.Drawing.Point(247, 214);
            this.mailCheck_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mailCheck_label.Name = "mailCheck_label";
            this.mailCheck_label.Size = new System.Drawing.Size(0, 12);
            this.mailCheck_label.TabIndex = 16;
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(332, 497);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(91, 29);
            this.send_button.TabIndex = 18;
            this.send_button.Text = "button1";
            this.send_button.UseVisualStyleBackColor = true;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.password_label.Location = new System.Drawing.Point(28, 76);
            this.password_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(94, 32);
            this.password_label.TabIndex = 2;
            this.password_label.Text = "*密碼";
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 537);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AccountForm";
            this.Text = "帳號申請";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label account_label;
        private System.Windows.Forms.TextBox account_textBox;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.TextBox pwdDoubleCheck_textBox;
        private System.Windows.Forms.Label pwdCheck_label;
        private System.Windows.Forms.TextBox userFName_textBox;
        private System.Windows.Forms.Label userFName_label;
        private System.Windows.Forms.TextBox userLName_textBox;
        private System.Windows.Forms.Label userLName_label;
        private System.Windows.Forms.TextBox aka_textBox;
        private System.Windows.Forms.Label aka_label;
        private System.Windows.Forms.Label birthday_label;
        private System.Windows.Forms.TextBox mail_textBox;
        private System.Windows.Forms.Label mail_label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label accountValid_label;
        private System.Windows.Forms.Label mailCheck_label;
        private System.Windows.Forms.Label pwdValid_label;
        private System.Windows.Forms.Label pwdDoubleValid_label;
        private System.Windows.Forms.Label mailValid_label;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.DateTimePicker birthday;
        private System.Windows.Forms.Label password_label;
    }
}