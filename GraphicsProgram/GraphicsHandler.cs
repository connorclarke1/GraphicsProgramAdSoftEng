using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace GraphicsProgram
{
    public class GraphicsHandler
    {
        internal Graphics graphics;
        internal PictureBox pictureBox;
        public Pointer pointer;
        internal Pen pen;
        SolidBrush brush;

        //Graphics Handler initialisation - creates bitmap, clears it to be reset
        public GraphicsHandler(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }
            pictureBox.Image =  new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.White);
            graphics.SetClip(new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            pointer = new Pointer();
            brush = new SolidBrush(Color.Black);
            pen = new Pen(Color.Black);
            //graphics.FillEllipse(Brushes.Cyan, 10, 10, 100, 100);
        }

        public void CircleTest() 
        {
            int xpos = pointer.GetPointerXPos();
            int ypos = pointer.GetPointerYPos();

            //graphics.FillEllipse(Brushes.Cyan, xpos - 50, ypos - 50, 100, 100);
            //graphics.DrawLine(pen, xpos, ypos, 100, 100);
        }

        public void ClearTest()
        {
            //graphics.Clear(Color.White);
            //pictureBox.Invalidate();
            Clear.ClearMethod(graphics, pictureBox);
        }
        public void SetColour(Color colour) 
        {
            pen.Color = colour;
            brush.Color = colour;
        }

        
    }
}
