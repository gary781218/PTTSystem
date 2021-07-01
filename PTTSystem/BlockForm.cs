using CCWin;
using CCWin.Win32.Const;
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
    public partial class BlockForm : Skin_Color
    {
        //共用參數區
        Rectangle screen;
        DataTable dt;
        DateConvert dataConvert;
        Account account = new Account();

        public BlockForm()
        {
            InitializeComponent();
            screen = Screen.FromControl(this).Bounds;
            dataConvert = new DateConvert();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        public BlockForm(Account account)
        {
            InitializeComponent();
            screen = Screen.FromControl(this).Bounds;
            dataConvert = new DateConvert();
            this.account = account;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form視窗長寬高設定
            Width = dataConvert.SizeZoom(screen.Width, 0.9);
            Height = dataConvert.SizeZoom(screen.Height, 0.95);
            Location = new Point(25, 25);

            //bar長寬設定
            skinPanel1.Width = Width - 5;
            skinPanel1.Height = 40;

            //討論版顯示區最外層容器
            FlowLayoutPannelModulation div = new FlowLayoutPannelModulation(
                button1.Location.X,
                button1.Location.Y + 80,
                Width - 90,
                Height - 200,
                false
                );
            div.HorizontalScroll.Visible = false;

            //取得討論版資料
            List<Block> blocks = new Block().getBlocks();

            //討論版各欄位顯示比例
            int nameWidth = dataConvert.SizeZoom(div.Width, 0.17);
            int popWidth = dataConvert.SizeZoom(div.Width, 0.055);
            int cNameWidth = dataConvert.SizeZoom(div.Width, 0.07);
            int despWidth = dataConvert.SizeZoom(div.Width, 0.55);

            //Console.WriteLine(div.Width);
            //Console.WriteLine(nameWidth + popWidth+ cNameWidth + despWidth);

            //顏色更改器
            NumberCheck numberCheck = new NumberCheck();
            
            foreach (var block in blocks)
            {
                //元件顯示
                LabelModulation name = new LabelModulation(block.name, 20, 0, 0, nameWidth, 40, false);
                string popularity_str = block.popularity == null ? "" : block.popularity.ToString();
                LabelModulation popularity = new LabelModulation(popularity_str, 20, 0, 0, popWidth, 40, false);
                List<int> colorList = numberCheck.BlockPopulartyColor(popularity_str);          //依照人氣數字範圍回傳不同顏色
                popularity.ForeColor = Color.FromArgb(colorList[0], colorList[1], colorList[2]);  //更換字體顏色 
                LabelModulation cName = new LabelModulation(block.cName, 20, 0, 0, cNameWidth, 40, false);
                LabelModulation description = new LabelModulation(block.description, 20, 0, 1, despWidth, 40, false);

                //帶入參數供後續使用
                name.Tag = block;
                popularity.Tag = block;
                cName.Tag = block;
                description.Tag = block;

                //設定click事件
                name.MouseHover += new EventHandler(CellMouseHover);
                name.MouseLeave += new EventHandler(CellMouseLeave);
                name.Click += new EventHandler(CellClick);
                popularity.MouseHover += new EventHandler(CellMouseHover);
                popularity.MouseLeave += new EventHandler(CellMouseLeave);
                popularity.Click += new EventHandler(CellClick);
                cName.MouseHover += new EventHandler(CellMouseHover);
                cName.MouseLeave += new EventHandler(CellMouseLeave);
                cName.Click += new EventHandler(CellClick);
                description.MouseHover += new EventHandler(CellMouseHover);
                description.MouseLeave += new EventHandler(CellMouseLeave);
                description.Click += new EventHandler(CellClick);

                //行容器
                FlowLayoutPannelModulation tr = new FlowLayoutPannelModulation(0, 0, div.Width-50, name.Height, false);
                //Console.WriteLine(tr.Width);
                //加入行容器controls
                tr.Controls.Add(name);
                tr.Controls.Add(popularity);
                tr.Controls.Add(cName);
                tr.Controls.Add(description);
                //tr.Click += new EventHandler(test);
                tr.MouseHover += new EventHandler(CellMouseHover);
                tr.MouseLeave += new EventHandler(CellMouseLeave);
                tr.Click += new EventHandler(CellClick);

                //加入討論版顯示區最外層容器
                div.Controls.Add(tr);
                this.Controls.Add(div);
            }
        }

        //事件
        private void CellMouseHover(object sender, EventArgs e)
        {
            try
            {
                Label label = (Label)sender;
                label.Parent.BackColor = Color.FromArgb(170, 170, 170);
                Cursor = Cursors.Hand;
                foreach (Control Ctl in label.Parent.Controls)
                {
                    Ctl.BackColor = Color.FromArgb(170, 170, 170);
                }
            }
            catch
            {
                FlowLayoutPanel flp = (FlowLayoutPanel)sender;
                flp.BackColor = Color.FromArgb(170, 170, 170);
                Cursor = Cursors.Hand;
                foreach (Control Ctl in flp.Controls)
                {
                    Ctl.BackColor = Color.FromArgb(170, 170, 170);
                }
            }
        }
        private void CellMouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label label = (Label)sender;
                label.Parent.BackColor = Color.Black;
                Cursor = Cursors.Default;
                foreach (Control Ctl in label.Parent.Controls)
                {
                    Ctl.BackColor = Color.Black;

                }
            }
            catch
            {
                FlowLayoutPanel flp = (FlowLayoutPanel)sender;
                flp.BackColor = Color.Black;
                Cursor = Cursors.Default;
                foreach (Control Ctl in flp.Controls)
                {
                    Ctl.BackColor = Color.Black;
                }
            }
        }
        private void CellClick(object sender, EventArgs e)
        {
            Block block;
            try
            {
                Label label = (Label)sender;
                block = (Block)label.Tag;
            }
            catch
            {
                FlowLayoutPanel flp = (FlowLayoutPanel)sender;
                block = (Block)flp.Tag;
            }
            ArticleForm form = new ArticleForm(block, account);
            form.ShowDialog(this);
        }
    }
}
