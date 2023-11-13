﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Circle : Shapes
    {
        public static void Draw(GraphicsHandler graphicsHandler, int radius)
        {
            Pointer pointer = graphicsHandler.pointer;
            int pointerX = pointer.GetPointerXPos();
            int pointerY = pointer.GetPointerYPos();
            Graphics g = graphicsHandler.graphics;
            bool fill = graphicsHandler.fill;
            PictureBox pictureBox = graphicsHandler.pictureBox;

            if (fill) { g.FillEllipse(graphicsHandler.brush, pointerX - (radius), pointerY - (radius), (2 * radius), (2 * radius)); }
            if (!fill) { g.DrawEllipse(graphicsHandler.pen , pointerX - (radius), pointerY - (radius), (2 * radius), (2 * radius)); }
            pictureBox.Invalidate();
        }
    }
}
