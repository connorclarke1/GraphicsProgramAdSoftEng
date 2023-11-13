using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Triangle
    {
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
