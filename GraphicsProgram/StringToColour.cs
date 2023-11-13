using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class StringToColour
    {
        /// <summary>
        /// Converts String to Colour 
        /// <br/>Example:<br/> Call this method to draw a triangle onto the bitmap image
        ///     <code>
        ///     StringToColour("blue");
        ///     </code>
        /// This will return Color.Blue, if colour not in library will default to Color.Black
        /// </summary>
        /// <param name="colourStr">Colour string</param>
        /// <returns>Color</returns>
        public static Color Convert(String colourStr) 
        {
            if (colourStr.ToLower() == "black") { return Color.Black; }
            if (colourStr.ToLower() == "blue") { return Color.Blue; }
            if (colourStr.ToLower() == "green") { return Color.Green; }
            if (colourStr.ToLower() == "red") { return Color.Red; }
            if (colourStr.ToLower() == "white") { return Color.White; }
            if (colourStr.ToLower() == "yellow") { return Color.Yellow; }
            return Color.Black;
        }
    }
}
