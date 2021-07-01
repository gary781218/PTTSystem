using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Component
{
    class FlowLayoutPannelModulation : FlowLayoutPanel
    {
        public FlowLayoutPannelModulation(int locationX, int locationY, int width, int height, bool autosize=true)
        {
            //this.Margin = new Padding(0, 0, 0, 0);
            this.Location = new Point(locationX, locationY);
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = Color.Black;
            this.ForeColor = Color.Silver;
            this.AutoSize = autosize;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.AutoScroll = true;
            this.Margin = new Padding(0, 0, 0, 0);
        }
    }

    class ArticleFLPModulation : FlowLayoutPanel
    {
        public ArticleFLPModulation(int locationX, int locationY, int width, int height, bool autosize = true)
        {
            this.Location = new Point(locationX, locationY);
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = Color.FromArgb(17, 17, 17);
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.AutoSize = AutoSize;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.AutoScroll = true;
            this.Margin = new Padding(0, 5, 0, 5);
        }
    }

    class ArticleTrFLPModulation : FlowLayoutPanel
    {
        public ArticleTrFLPModulation(int locationX, int locationY, int width, int height, bool autosize = true)
        {
            this.Location = new Point(locationX, locationY);
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = Color.FromArgb(17, 17, 17);
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.AutoSize = autosize;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.AutoScroll = true;
            this.Margin = new Padding(0, 0, 0, 0);
        }
    }

    class MessageFLPModulation : FlowLayoutPanel
    {
        public MessageFLPModulation(int locationX, int locationY, int width, int height, bool autosize = true)
        {
            this.Location = new Point(locationX, locationY);
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = Color.Black;
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.AutoSize = autosize;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.AutoScroll = true;
            this.Margin = new Padding(0, 5, 0, 5);
        }
    }

}
