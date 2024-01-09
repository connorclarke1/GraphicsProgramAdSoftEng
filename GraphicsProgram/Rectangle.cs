using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Rectangle
    {
        /// <summary>
        /// Draws a rectangle onto GraphicsHandler.Graphics using GraphicsHandler pointer, colour and param radius
        /// <br/>Example:<br/> Call this method to draw a rectangle onto the bitmap image
        ///     <code>
        ///     Rectangle.Draw(graphicsHandler, x1, y1);
        ///     </code>
        /// This will draw a rectangle of width xLen, and height yLen, from the pointer location
        /// </summary>
        /// <param name="graphicsHandler">GraphicsHandler to be used.</param>
        /// <param name="xLen">X Width.</param>
        /// <param name="yLen">Y Length.</param>
        /// <returns>void</returns>
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
            pictureBox.Refresh();
        }
    }
}
