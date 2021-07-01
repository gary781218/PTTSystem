using CCWin;
using CCWin.SkinClass;
using PTTSystem.Component;
using PTTSystem.Function;
using PTTSystem.Model;
using PTTSystem.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PTTSystem.Function.NumberCheck;

namespace PTTSystem
{
    public partial class ContextForm : Skin_Color
    {
        //共用參數區
        Rectangle screen;
        DataTable dt;
        DateConvert dataConvert;
        List<object> list;                //外部傳入的Block、Article資料，為了顯示在UI上
        FlowLayoutPannelModulation div;   ////內文與留言顯示區最外層容器

        public ContextForm(List<object> list)
        {
            InitializeComponent();

            screen = Screen.FromControl(this).Bounds;
            dataConvert = new DateConvert();
            this.list = list;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void ContextForm_Load(object sender, EventArgs e)
        {
            //取得該篇文章對應屬性
            Block block = (Block)list[0];
            Article article = (Article)list[1];
            label6.Text = block.name;
            
            //Form視窗長寬高設定
            Width = dataConvert.SizeZoom(screen.Width, 0.9);
            Height = dataConvert.SizeZoom(screen.Height, 0.95);
            Location = new Point(25, 25);

            //bar長寬設定
            skinPanel1.Width = Width;
            skinPanel1.Height = 40;

            //內文與留言顯示區最外層容器
            div = new FlowLayoutPannelModulation(
                skinPanel2.Location.X,
                skinPanel2.Location.Y + 150,
                Width - 90,
                Height - 220,
                false
                );
            div.HorizontalScroll.Visible = false;

            //上方模板內容設定
            List<Article> result = new Article().getContent(article.article_ID);
            Article context = result[0];
            label14.Text = block.name;
            label7.Text = context.user_ID;
            label8.Text = context.article_title;
            label9.Text = context.createdate;

            //內文，判斷是否有網址或圖片，再逐行顯示
            ContentViewer contentview = new ContentViewer(context.article_content, div, div.Width);

            //留言
            int pushWidth = dataConvert.SizeZoom(div.Width, 0.02);
            int userWidth = dataConvert.SizeZoom(div.Width, 0.1);
            int messageWidth = dataConvert.SizeZoom(div.Width, 0.73);
            int ipdatetimeWidth = dataConvert.SizeZoom(div.Width, 0.12);

            //取得所有留言
            List<Model.Message> messages = new Model.Message().getMessage(context.article_ID);

            foreach (var message in messages)
            {
                MessageFLPModulation tr2 = new MessageFLPModulation(0, 0, div.Width, 40, false);

                MessageLabelModulation push = new MessageLabelModulation(message.push, 20, 0, 0, pushWidth, 40, false);
                PushTagCheck check = new PushTagCheck();
                List<int> pushtagColor = check.PushTagColor(message.push.ToString());
                push.ForeColor = Color.FromArgb(pushtagColor[0], pushtagColor[1], pushtagColor[2]);

                MessageLabelModulation user = new MessageLabelModulation(message.user_ID, 20, 0, 0, userWidth, 40, false);
                user.ForeColor = Color.FromArgb(255, 255, 102);

                MessageLabelModulation messageContent = new MessageLabelModulation(message.message, 20, 0, 0, messageWidth, 40, false);
                //ContentViewer messageContent = new ContentViewer(message.message, tr2, messageWidth, false);

                MessageLabelModulation ipdatetime = new MessageLabelModulation(message.ipdatetime, 20, 0, 0, ipdatetimeWidth, 40, false);

                tr2.Controls.Add(push);
                tr2.Controls.Add(user);
                tr2.Controls.Add(messageContent);
                tr2.Controls.Add(ipdatetime);
                div.Controls.Add(tr2);

                string newline = message.message.Replace(": ", "").Trim();
                if (Regex.Match(newline, @"https://\S*.(png|jpg|jpeg|gif)").Success)
                {
                    MessageFLPModulation tr3 = new MessageFLPModulation(0, 0, div.Width, 450, true);
                    ContentViewer messageContent_picture = new ContentViewer(newline, tr3, div.Width, false);
                    div.Controls.Add(tr3);
                }

            }
            this.Controls.Add(div);
        }
    }
}
