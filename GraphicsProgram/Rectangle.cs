using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Rectangle
    {
        public static void Draw(GraphicsHandler graphicsHandler, int xLen, int yLen)
        {
            Pointer pointer = graphicsHandler.pointer;
            int pointerX = pointer.GetPointerXPos();
            int pointerY = pointer.GetPointerYPos();
            Graphics g = graphicsHandler.graphics;
            bool fill = graphicsHandler.fill;
            PictureBox pictureBox = graphicsHandler.pictureBox;

            if (fill) { g.FillRectangle(graphicsHandler.brush, pointerX, pointerY, xLen, yLen); }
            if (!fill) { g.DrawRectangle(graphicsHandler.pen, pointerX, pointerY, xLen, yLen); }
            pictureBox.Invalidate();
        }
    }
}
