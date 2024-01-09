using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsProgram
{
    internal class Clear
    {
        /// <summary>
        /// Clears the screen
        /// <br/>Example:<br/> Call this method to clear the bitmap image
        ///     <code>
        ///     Clear.ClearMethod(graphicsHandler);
        ///     </code>
        /// This will clear the bitmap image on the display
        /// </summary>
        /// <param name="graphicsHandler">GraphicsHandler to be used.</param>
        /// <returns>void</returns>
        public static void ClearMethod(GraphicsHandler graphicsHandler)
        {
            Graphics g = graphicsHandler.graphics;
            PictureBox p = graphicsHandler.pictureBox; 
            g.Clear(Color.White);
            p.Invalidate();
            p.Refresh();
        }
    }
}
