using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsProgram
{
    internal class GraphicsHandler
    {
        Graphics graphics;
        PictureBox pictureBox;

        //Graphics Handler initialisation - creates bitmap, clears it to be reset
        public GraphicsHandler(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.White);
            //graphics.FillEllipse(Brushes.Cyan, 10, 10, 100, 100);
        }

        public void CircleTest() 
        {
            graphics.FillEllipse(Brushes.Cyan, 10, 10, 100, 100);
        }

        public void ClearTest()
        {
            graphics.Clear(Color.White);
            pictureBox.Invalidate();
        }
    }
}
