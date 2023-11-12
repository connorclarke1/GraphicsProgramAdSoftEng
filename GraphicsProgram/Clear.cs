using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Clear
    {
        public static void ClearMethod(GraphicsHandler graphicsHandler)
        {
            Graphics g = graphicsHandler.graphics;
            PictureBox p = graphicsHandler.pictureBox; 
            g.Clear(Color.White);
            p.Invalidate();
        }
    }
}
