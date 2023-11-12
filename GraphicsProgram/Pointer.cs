using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    public class Pointer
    {
        public int ypos, xpos;

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

        public int GetPointerXPos() 
        //added x and y pos getters for faster processing if x is used only
        {
            return xpos;
        }

        public int GetPointerYPos()
        //added x and y pos getters for faster processing if y is used only
        {
            return ypos;
        }

        //maybe change to two integers see in actual implementation
        public void SetPointerPos(int[] posArray)
        {
            this.xpos = posArray[0];
            this.ypos = posArray[1];
        }

        public void SetPointerXPos(int xPos)
        {
            this.xpos =xPos;
        }
        public void SetPointerYPos(int yPos)
        {
            this.ypos = yPos;
        }

    }
}
