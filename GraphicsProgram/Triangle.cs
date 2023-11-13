using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Triangle
    {
        /// <summary>
        /// Draws a triangle onto GraphicsHandler.Graphics using GraphicsHandler pointer, colour and param radius
        /// <br/>Example:<br/> Call this method to draw a triangle onto the bitmap image
        ///     <code>
        ///     Triangle.Draw(graphicsHandler, x1,y2,x2,y2);
        ///     </code>
        /// This will draw a triangle between the location of the pointer and x1,y1,x2,y2
        /// </summary>
        /// <param name="graphicsHandler">GraphicsHandler to be used.</param>
        /// <param name="xPointA">X pos of first point.</param>
        /// <param name="yPointA">Y pos of first point.</param>
        /// <param name="xPointB">X pos of second point.</param>
        /// <param name="yPointB">Y pos of first point.</param>
        /// <returns>void</returns>
        public static void Draw(GraphicsHandler graphicsHandler, int xPointA, int yPointA, int xPointB, int yPointB)
        {
            Pointer pointer = graphicsHandler.pointer;
            int pointerX = pointer.GetPointerXPos();
            int pointerY = pointer.GetPointerYPos();
            Graphics g = graphicsHandler.graphics;
            bool fill = graphicsHandler.fill;
            PictureBox pictureBox = graphicsHandler.pictureBox;

            Point pointA = new Point(pointerX, pointerY);
            Point pointB = new Point(xPointA, yPointA);
            Point pointC = new Point(xPointB, yPointB);
            Point[] pointArray = new Point[3];

            pointArray[0] = pointA;
            pointArray[1] = pointB;
            pointArray[2] = pointC;

            if (fill) { g.FillPolygon(graphicsHandler.brush, pointArray); }
            if (!fill) { g.DrawPolygon(graphicsHandler.pen, pointArray); }
            pictureBox.Invalidate();
        }
    }
}
