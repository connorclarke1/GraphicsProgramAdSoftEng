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
        /// <summary>
        /// Draws a line onto GraphicsHandler.Graphics using GraphicsHandler pointer, colour and param radius
        /// <br/>Example:<br/> Call this method to draw a line onto the bitmap image
        ///     <code>
        ///     DrawTo.Draw(50, 50);
        ///     </code>
        /// This will draw a line from the pointer position to endX, endY in the colour set in the graphicsHandler
        /// </summary>
        /// <param name="endX">X position to draw to</param>
        /// <param name="endY">Y position to draw to</param>
        /// <returns>void</returns>
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
            pictureBox.Refresh();

            pointer.SetPointerXPos(endX);
            pointer.SetPointerYPos(endY);


        }
    }
}
