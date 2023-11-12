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
            Pointer pointer = new Pointer();   
            int startX = pointer.GetPointerXPos();
            int startY = pointer.GetPointerYPos();

            Graphics g = graphicsHandler.graphics;
            Pen p = graphicsHandler.pen;
            PictureBox pictureBox = graphicsHandler.pictureBox;

            g.DrawLine(p, startX, startY, endX, endY);
            pictureBox.Invalidate();

            //Set pointer x,y to new location
            //int[] posArray = new int[2];
            //posArray[0] = endX;
            //posArray[1] = endY;
            //pointer.setPointerPos(posArray);
            pointer.SetPointerXPos(endX);
            pointer.SetPointerYPos(endY);
            
        }
    }
}
