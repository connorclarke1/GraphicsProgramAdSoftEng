using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    internal class Pointer
    {
        int ypos, xpos;

        public Pointer()
        {
            ypos = 0;
            xpos = 0;
        }
        
        public int[] getPointerPos()
        {
            int[] posArray =  {xpos , ypos};
            return posArray;
        }

        public int getPointerXPos() 
        //added x and y pos getters for faster processing if x is used only
        {
            return xpos;
        }

        public int getPointerYPos()
        //added x and y pos getters for faster processing if y is used only
        {
            return ypos;
        }

        //maybe change to two integers see in actual implementation
        public void setPointerPos(int[] posArray)
        {
            this.xpos = posArray[0];
            this.ypos = posArray[1];
        }
    }
}
