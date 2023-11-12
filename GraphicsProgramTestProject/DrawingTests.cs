﻿using GraphicsProgram;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicsProgramTestProject

{
    [TestClass]
    public class DrawingTests
    {
        [TestMethod]
        public void DrawTo_PointerChange_Test()
        {
            //Arrange
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(1000, 1000));

            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            DrawTo.Draw(graphicsHandler, 100, 100);

            //Act
            

            //Assert
            Assert.AreEqual(graphicsHandler.pointer.GetPointerXPos(), 100);
            Assert.AreEqual(graphicsHandler.pointer.GetPointerYPos(), 100);
        }
        [TestMethod]
        public void Pointer_SetXTest()
        {
            Pointer pointer = new Pointer();
            pointer.SetPointerXPos(100);
            Assert.AreEqual(pointer.xpos, 100);
        }
        
    }
}