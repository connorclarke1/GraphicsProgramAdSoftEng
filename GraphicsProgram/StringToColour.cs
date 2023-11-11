using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class StringToColour
    {
        public static Color Convert(String colourStr) 
        {
            if (colourStr.ToLower() == "black") { return Color.Black; }
            if (colourStr.ToLower() == "blue") { return Color.Blue; }
            if (colourStr.ToLower() == "green") { return Color.Green; }
            if (colourStr.ToLower() == "red") { return Color.Red; }
            if (colourStr.ToLower() == "white") { return Color.White; }
            if (colourStr.ToLower() == "yellow") { return Color.Yellow; }
            return Color.Black; //add exception here, should be checked already
        }
    }
}
