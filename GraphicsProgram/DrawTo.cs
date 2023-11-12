using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class DrawTo
    {
        public static void Draw(GraphicsHandler graphicsHandler, int endX, int endY)
        {
            Pointer pointer = graphicsHandler.pointer;
            Graphics g = graphicsHandler.graphics;
            Pen p = graphicsHandler.pen;

            int startX = pointer.getPointerXPos();
            int startY = pointer.getPointerYPos();

            g.DrawLine(p, startX, startY, endX, endY);
            //Set pointer x,y to new location
            int[] posArray = new int[2];
            posArray[0] = endX;
            posArray[1] = endY;
            pointer.setPointerPos(posArray);
        }
    }
}
