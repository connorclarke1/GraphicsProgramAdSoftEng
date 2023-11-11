using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Circle : Shapes
    {
        Graphics graphics;
        Color colour;
        public void setColour(Color colour)
        {
            this.colour = colour;
        }
        public Circle(Pointer pointer, Color colour, bool fillBool, int r, Graphics g, Pen pen, Brush brush)
        {
            int[] pointerPos = pointer.getPointerPos();
            graphics = g;
            if (fillBool) { g.FillEllipse(brush, pointerPos[0] - r, pointerPos[1] - r, pointerPos[0] + r, pointerPos[0] + r); }
            if (!fillBool) { g.DrawEllipse(pen, pointerPos[0] - r, pointerPos[1] - r, pointerPos[0] + r, pointerPos[0] + r); }
            
        }
    }
}
