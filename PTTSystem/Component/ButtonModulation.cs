using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTSystem.Component
{
    class ButtonModulation : Button
    {
        public ButtonModulation(String text, int fontsize, int loctionX = 0, int locationY = 0, int width = 30, int height = 30, bool autosize = false, bool enabled = true)
        {
            this.Font = new Font("新細明體", fontsize, FontStyle.Regular);
            this.AutoSize = false;
            this.Enabled = enabled;
            this.Size = new Size(width, height);
            this.Text = text;
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
    }
}
