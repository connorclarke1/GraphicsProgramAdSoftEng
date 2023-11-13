using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    public class DrawTo
    {
        public static void Draw(GraphicsHandler graphicsHandler, int endX, int endY)
        {
            Pointer pointer = graphicsHandler.pointer;   
            int startX = pointer.GetPointerXPos();
            int startY = pointer.GetPointerYPos();


            Graphics g = graphicsHandler.graphics;
            Pen p = graphicsHandler.pen;
            PictureBox pictureBox = graphicsHandler.pictureBox;

            g.DrawLine(p, startX, startY, endX, endY);
            pictureBox.Invalidate();

            pointer.SetPointerXPos(endX);
            pointer.SetPointerYPos(endY);


        }
    }
}
