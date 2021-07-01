using PTTSystem.Function;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using CCWin.Win32.Struct;
using PTTSystem.Component;

namespace PTTSystem
{
    public partial class CreateArticle : Skin_Color
    {
        Block block;
        Account account;
        Rectangle screen;
        DateConvert dataConvert;

        public CreateArticle(Block block, Account account)
        {
            InitializeComponent();
            screen = Screen.FromControl(this).Bounds;
            dataConvert = new DateConvert();
            this.block = block;
            this.account = account;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }
        private void CreateArticle_Load(object sender, EventArgs e)
        {
            //Form視窗長寬高設定
            Width = dataConvert.SizeZoom(screen.Width, 0.9);
            Height = dataConvert.SizeZoom(screen.Height, 0.95);
            Location = new Point(25, 25);

            ////textbox1設定
            textBox1.Width = Width - 250;
            textBox1.Height = 45;
            textBox1.BackColor = Color.FromArgb(34, 34, 34);
            textBox1.ForeColor = Color.Silver;
            //textBox1.Font = Font = new Font("新細明體", 14, FontStyle.Regular);

            //textbox2設定
            textBox2.Width = Width - 250;
            textBox2.BackColor = Color.FromArgb(34, 34, 34);
            textBox1.ForeColor = Color.Silver;

            //建立按鈕
            Button check = new ButtonModulation("確定", 10, 0, 0, 50, 30, false, true);

        }

        private void 上傳圖檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(dialog.FileName);
                APIMethod apimethod = new APIMethod();

                //確認相簿是否已建立
                string uri = "https://api.imgur.com/3/account/gary781218/albums";
                Object headers = new { Authorization = "Bearer 5d14027e993fac88b5e46b0658ec947113979daa" };
                ResponseArrayModels albumsOfGary = apimethod.Get<ResponseArrayModels>(uri, headers: headers).Result;

                //建立相簿
                string albumName = account.userGUID.ToString();
                bool hasAlbum = false;
                ImageModels tempModel = new ImageModels();
                foreach (var x in albumsOfGary.data)
                {
                    if (x.title.Equals(albumName))
                    {
                        tempModel = x;
                        hasAlbum = true;
                    }
                }

                ResponseModels AlbumCreateResult = new ResponseModels();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                BodyModels body = new BodyModels();
                body.filemodels.filepath = $"{dialog.FileName}";
                body.filemodels.filetype = "image";
                body.filemodels.filename = $@"{dialog.FileName.Split('\\').Last() }";
                body.parameters = dict;
                if (!hasAlbum)
                {
                    uri = $"https://api.imgur.com/3/album?title={albumName}";
                    AlbumCreateResult = apimethod.Post<ResponseModels>(uri, headers: headers).Result;
                    Console.WriteLine($"已新建相簿, title={AlbumCreateResult.data.title}");

                    dict.Add("album", $"{AlbumCreateResult.data.deletehash}");
                    dict.Add("type", "file");
                    dict.Add("title", $"{dialog.FileName.Split('\\').Last()}");
                }
                else
                {
                    dict.Add("album", $"{tempModel.deletehash}");
                    dict.Add("type", "file");
                    dict.Add("title", $"{dialog.FileName.Split('\\').Last()}");
                    Console.WriteLine($"已將{body.filemodels.filename} 放入{tempModel.title}相簿中");
                }

                uri = $"https://api.imgur.com/3/image";
                ResponseModels UpdateImagesResult = UpdateImagesResult = apimethod.Post<ResponseModels>(uri, body, headers).Result;
                textBox2.Text = textBox2.Text + "\r\n" + UpdateImagesResult.data.link;

            }
        }

        
    }
}
