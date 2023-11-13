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
        internal SolidBrush brush;
        public bool fill;

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
            //graphics.SetClip(new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            pointer = new Pointer();
            brush = new SolidBrush(Color.Black);
            pen = new Pen(Color.Black);
            fill = false;
            //graphics.FillEllipse(Brushes.Cyan, 10, 10, 100, 100);
        }

        public void SetColour(Color colour) 
        {
            pen.Color = colour;
            brush.Color = colour;
        }

        public void SetFill(bool fill)
        {
            if (fill) { this.fill = true; }
            if(!fill) { this.fill = false; }  
        }

        
    }
}
