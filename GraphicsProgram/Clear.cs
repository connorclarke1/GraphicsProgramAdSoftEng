using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Clear
    {
        public static void ClearMethod(Graphics g, PictureBox p)
        {
            g.Clear(Color.White);
            p.Invalidate();
        }
    }
}
