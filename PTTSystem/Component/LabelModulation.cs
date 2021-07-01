using PTTSystem.Function;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Component
{
    class LabelModulation : Label
    {
        public LabelModulation(String text, int fontsize, int x = 0, int y = 0, int width=0, int height = 40, bool autosize = true )
        {
            //this.Margin = new Padding(0, 0, 0, 0);
            this.Text = text;
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.ForeColor = Color.FromArgb(136,136,136);
            this.BackColor = Color.Black;
            this.AutoSize = autosize;
            if (autosize == false)
            {
                this.Size = new Size(width, height);
            }
        }
    }
    class ArticleLabelModulation : Label
    {
        public ArticleLabelModulation(String text, int fontsize, int x = 0, int y = 0, int width = 0, int height = 40, bool autosize = true)
        {
            this.Text = text;
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.BackColor = Color.FromArgb(17, 17, 17);
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.AutoSize = autosize;
            if (autosize == false)
            {
                this.Size = new Size(width, height);
            }
        }
    }

    class MessageLabelModulation : Label
    {
        public MessageLabelModulation(String text, int fontsize, int x = 0, int y = 0, int width = 0, int height = 40, bool autosize = true)
        {
            this.Text = text;
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.BackColor = Color.Black;
            this.ForeColor = Color.FromArgb(153, 153, 0);
            this.AutoSize = autosize;
            if (autosize == false)
            {
                this.Size = new Size(width, height);
            }
        }
    }

    class PaginationLabelModulation : Label
    {
        public PaginationLabelModulation(String text, int fontsize, int x = 0, int y = 0, int width = 0, int height = 30, bool autosize = true)
        {
            this.Text = text;
            this.Margin = new Padding(0, 12, 0, 0);
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.AutoSize = autosize;
            if (autosize == false)
            {
                this.Size = new Size(width, height);
            }
        }
    }
}
