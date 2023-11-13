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
        {
            return xpos;
        }

        public int GetPointerYPos()
        {
            return ypos;
        }

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
