using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Component
{
    class TextBoxModulation : TextBox
    {
        public TextBoxModulation(String text = "", int fontsize = 20, int x = 0, int y = 0, int width = 0, int height = 40, bool autosize = true)
        {
            this.Text = text;
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.ForeColor = Color.Black;
            this.BackColor = Color.Silver;
            this.AutoSize = autosize;
            if (autosize != true)
            {
                this.Size = new Size(width, height);
            }
        }
    }

    class TextBoxArticleCreate : TextBox
    {
        public TextBoxArticleCreate(String text = "", int fontsize = 20, int x = 0, int y = 0, int width = 0, int height = 40, bool autosize = true)
        {
            this.Text = text;
            this.Location = new Point(x, y);
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.ForeColor = Color.FromArgb(34, 34, 34);
            this.BackColor = Color.Silver;
            this.AutoSize = autosize;
            if (autosize != true)
            {
                this.Size = new Size(width, height);
            }
        }
    }
}
