using PTTSystem.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace PTTSystem.utility
{
    class ContentViewer
    {
        public ContentViewer(String Context, FlowLayoutPanel flp, int width, bool isContent=true)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            string[] text_list = Context.Split(stringSeparators, StringSplitOptions.None);
            Regex regex = new Regex(@"^https*://\S*.(png|jpg|jpeg|gif)");
            foreach (var line in text_list)
            {

                if (regex.IsMatch(line))
                {
                    PictureBox pbox = new PictureBox();
                    
                    //Label label = new Label();
                    //label.Width = flp.Width;
                    //label.Height = 20;
                    flp.Controls.Add(pbox);
                    //flp.Controls.Add(label);

                    WebClient client = new WebClient();
                    client.DownloadDataCompleted += DownloadDataCompleted;
                    client.DownloadDataAsync(new Uri(line), pbox);
                }
                else if (Regex.Match(line, @"https*://youtu.be\S*").Success)
                {
                    //   case 0:
                    //   case 1:
                    //   .....
                    //   break;

                    ChromiumWebBrowser youtubePlayer = new ChromiumWebBrowser(line.Replace("youtu.be/", @"www.youtube.com/embed/"))
                    {
                        Name = "webBrowser1",
                        Dock = DockStyle.Fill
                    };

                    GroupBox gbox = new GroupBox();
                    gbox.Width = 800;
                    gbox.Height = 450;
                    gbox.Controls.Add(youtubePlayer);
                    flp.Controls.Add(gbox);

                    Label label = new Label();
                    label.Width = flp.Width;
                    label.Height = 20;
                    flp.Controls.Add(label);
                }
                else if (Regex.Match(line, @"https://www.youtube.com/\S*").Success)
                {
                    ChromiumWebBrowser youtubePlayer = new ChromiumWebBrowser(line.Replace("watch?v=", @"/embed/"))
                    {
                        Name = "webBrowser1",
                        Dock = DockStyle.Fill
                    };

                    GroupBox gbox = new GroupBox();
                    gbox.Width = 800;
                    gbox.Height = 450;
                    gbox.Controls.Add(youtubePlayer);
                    flp.Controls.Add(gbox);

                    Label label = new Label();
                    label.Width = flp.Width;
                    label.Height = 20;
                    flp.Controls.Add(label);
                }
                else
                {
                    if (isContent)
                    {
                        LabelModulation label = new LabelModulation(line, 20, 0, 0, flp.Width, 40, false);
                        label.Text = line;
                        label.Width = width;
                        flp.Controls.Add(label);
                    }
                    else
                    {
                        MessageLabelModulation label = new MessageLabelModulation(line, 20, 0, 0, flp.Width, 40, false);
                        label.Text = line;
                        label.Width = width;
                        flp.Controls.Add(label);
                    }
                }
            }
        }

        public void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            byte[] raw = e.Result;
            MemoryStream imgStream = new MemoryStream(raw);
            Image img = Image.FromStream(imgStream);

            int wSize = img.Width;
            int hSize = img.Height;

            int pboxWidth = wSize;
            int pboxHeight = hSize;
            if (img.Width > 840)
            {
                pboxWidth = 840;
                pboxHeight = (int)(hSize / wSize * 840); 

            }

            PictureBox pbox = (PictureBox)e.UserState;
            pbox.Image = img;
            pbox.Width = pboxWidth;
            pbox.Height = pboxHeight;
            //pbox.Width = wSize > 250 ? (int)(wSize * 0.8) : wSize;
            //pbox.Height = wSize > 250 ? (int)(hSize * 0.8) : hSize;
            pbox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
