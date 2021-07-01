using PTTSystem.Function;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CCWin.Win32.NativeMethods;

namespace PTTSystem.Component
{
    class PictureBoxModulation : PictureBox
    {
        public PictureBoxModulation(String path, int width, int height, int x, int y)
        {
            this.Image = Image.FromFile(path);
            this.Size = new System.Drawing.Size(width, height);
            this.Location = new System.Drawing.Point(x, y);
        }
    }
}
