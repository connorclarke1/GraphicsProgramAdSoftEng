using GraphicsProgram;
using System.Drawing;
using System.Windows.Forms;
using System;


namespace GraphicsProgramTestProject

{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void CommandSplit_Capitals_Good_Test()
        {
            //Arrange
            //CommandParser commandParser = new CommandParser(); static so no need to create
            String command = "Circle 20";
            CommandParser commandParser = new CommandParser();

            //Act
            String[] splitCommand;
            splitCommand = CommandParser.CommandSplit(command);

            //Assert
            String[] actualSplitCommand;
            actualSplitCommand = new string[] { "circle", "20" };
            CollectionAssert.AreEqual(splitCommand, actualSplitCommand);
        }

        [TestMethod]
        public void CommandSplit_Var_Good_Test()
        {
            //Arrange
            //CommandParser commandParser = new CommandParser(); static so no need to create
            String command = "y = 20+5";


            //Act
            String[] splitCommand;
            splitCommand = CommandParser.CommandSplit(command);

            //Assert
            String[] actualSplitCommand;
            actualSplitCommand = new string[] { "var", "y", "20+5" };
            CollectionAssert.AreEqual(splitCommand, actualSplitCommand);
        }

        [TestMethod]
        public void CommandSplit_ExtraSpaces_Test()
        {
            //Arrange
            //CommandParser commandParser = new CommandParser(); static so no need to create
            String command = "Circle          20   ";


            //Act
            String[] splitCommand;
            splitCommand = CommandParser.CommandSplit(command);

            //Assert
            String[] actualSplitCommand;
            actualSplitCommand = new string[] { "circle", "20" };
            CollectionAssert.AreEqual(splitCommand, actualSplitCommand);
        }

        [TestMethod]
        public void CommandExtract_GoodCommand_NoParams_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "moveto" };


            //Act
            String commandStr = CommandParser.CommandExtract(commandArray); ;

            //Assert
            String actualCommandStr = "moveto";
            Assert.AreEqual(commandStr, actualCommandStr);
        }
        [TestMethod]
        public void CommandExtract_GoodCommand_Params_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "moveto", "100", "500", "Extra", "unneeded" };


            //Act
            String commandStr = CommandParser.CommandExtract(commandArray); ;

            //Assert
            String actualCommandStr = "moveto";
            Assert.AreEqual(commandStr, actualCommandStr);
        }
        [TestMethod]
        public void CommandExtract_EmptyCommandArray_Test()
        {
            //Arrange
            String[] commandArray = new string[] { };


            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => CommandParser.CommandExtract(commandArray));
        }
        [TestMethod]
        public void CommandExtract_InvalidCommand_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "InvalidCommand", "params" };


            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => CommandParser.CommandExtract(commandArray));
        }
        [TestMethod]
        public void ParamExtract_NoParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "clear" };
            String command = "clear";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new object[] { };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_NoParamWhenNeeded_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "circle" };
            String command = "circle";
            CommandParser commandParser = new CommandParser();

            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_OneIntParam_WithStringInstead_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "circle", "one" };
            String command = "circle";
            CommandParser commandParser = new CommandParser();


            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_OneIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "circle", "100" };
            String command = "circle";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new Object[] { 100 };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(int));
        }
        [TestMethod]
        public void ParamExtract_TwoIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "drawto", "100", "200" };
            String command = "drawto";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new Object[] { 100, 200 };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(int));
            Assert.IsInstanceOfType(Params[1], typeof(int));
        }
        [TestMethod]
        public void ParamExtract_FourIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "triangle", "100", "200", "500", "300" };
            String command = "triangle";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new Object[] { 100, 200, 500, 300 };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(int));
            Assert.IsInstanceOfType(Params[1], typeof(int));
            Assert.IsInstanceOfType(Params[2], typeof(int));
            Assert.IsInstanceOfType(Params[3], typeof(int));
        }
        [TestMethod]
        public void ParamExtract_OneStrParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "fill", "on" };
            String command = "fill";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new Object[] { "on" };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(string));
        }
        [TestMethod]
        public void ParamExtract_VarParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "var", "x", "100+x-y" };
            String command = "var";
            CommandParser commandParser = new CommandParser();

            //Act
            Object[] Params = new Object[] { "x", "100+x-y" };

            //Assert
            CollectionAssert.AreEqual(Params, commandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(string));
        }
        [TestMethod]
        public void ParamExtract_VarLogicException_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "var", "x", "100+x-y/" };
            String command = "var";
            CommandParser commandParser = new CommandParser();

            //Act



            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_ParamIntException_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "var", "x12y", "100+x-y" };
            String command = "var";
            CommandParser commandParser = new CommandParser();

            //Act


            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.ParamExtract(commandArray, command));
        }


        [TestMethod]
        public void CheckParamRangeNoParam_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { };
            String command = "clear";

            //Act

            //Assert
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Fill_On_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"on" };
            String command = "fill";

            //Act

            //Assert
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Fill_BadString_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"BadString" };
            String command = "fill";

            //Act

            //Assert

            Assert.ThrowsException<System.Exception>(() => CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_colour_blue_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"blue" };
            String command = "colour";

            //Act

            //Assert
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Colour_BadString_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"BadString" };
            String command = "colour";

            //Act

            //Assert

            Assert.ThrowsException<System.Exception>(() => CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_drawto_100_100_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (int)100, (int)100 };
            String command = "drawto";

            //Act

            //Assert
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void ExecuteCommand_FillOn_Test()
        {
            //Arrange
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(100, 100));
            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            CommandParser commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);

            //Act
            commandParser.FullParse("fill on");

            //Assert
            Assert.AreEqual(true, graphicsHandler.fill);
        }
        [TestMethod]
        public void ExecuteCommand_VarSetWorking_Test()
        {
            //Arrange
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(100, 100));
            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            CommandParser commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);

            //Act
            commandParser.FullParse("x = 100+5");

            //Assert
            Assert.AreEqual(105, commandParser.variableValues["x"]);
        }
        [TestMethod]
        public void ExecuteCommand_VarUpdateX_Test()
        {
            //Arrange
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(100, 100));
            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            CommandParser commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);

            //Act
            commandParser.FullParse("x = 100+5");
            commandParser.FullParse("x = x + 100");

            //Assert
            Assert.AreEqual(205, commandParser.variableValues["x"]);
        }
        [TestMethod]
        public void ExecuteCommand_Var_VarDoesntExist_Test()
        {
            //Arrange
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(100, 100));
            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            CommandParser commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);

            //Act
            //commandParser.FullParse("x = 100+y");
            string[] splitCommand = CommandParser.CommandSplit("x = 100+y");
            string strCommand = CommandParser.CommandExtract(splitCommand);
            object[] paramArray = commandParser.ParamExtract(splitCommand, strCommand);

            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.executeCommand(strCommand, paramArray));
        }
        [TestMethod]
        public void ParamExtract_withVariable_Test()
        {
            CommandParser commandParser = new CommandParser();
            string commandStr = "circle x";
            commandParser.FullParse("x = 100");
            string[] splitCommand = CommandParser.CommandSplit(commandStr);
            string strCommand = CommandParser.CommandExtract(splitCommand);
            object[] paramArray = commandParser.ParamExtract(splitCommand, strCommand);
            Object[] correctParamArray = new Object[] { (int)100 };

            CollectionAssert.AreEqual(paramArray, correctParamArray);

        }

        [TestMethod]
        public void FullParse_LogicalIf_Tests()
        {

            string commandA = "x = 1000";
            CommandParser commandParser = new CommandParser();
            string commandB = "y = 100";
            commandParser.FullParse(commandA);
            commandParser.FullParse(commandB);
            Assert.ThrowsException<System.Exception>(() => commandParser.FullParse("if x == y1"));
            //Assert that inside if when they are correct
            //no exception will be thrown
        }

        [TestMethod]
        public void CheckParamRange_IF_Tests()
        {
            Type[] typeArray = new Type[2];
            typeArray[0] = typeof(string);
            typeArray[1] = typeof(string);
            string[] paramArrayA = new string[] { "10", "20" };
            string[] paramArrayB = new string[] { "x", "20" };
            string[] paramArrayC = new string[] { "x12", "20" };

            Assert.AreEqual(true, CommandParser.checkParamRange(paramArrayA, "if"));
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArrayB, "if"));
            Assert.ThrowsException<System.Exception>(() => CommandParser.checkParamRange(paramArrayC, "if"));


        }
        [TestMethod]
        public void ParamExtract_IF_Tests()
        {
            CommandParser commandParser = new CommandParser();
            string inputStr = "if x == 12";
            //commandParser.FullParse("x = 100");
            string[] splitCommand = CommandParser.CommandSplit(inputStr);
            string strCommand = CommandParser.CommandExtract(splitCommand);
            object[] paramArray = commandParser.ParamExtract(splitCommand, strCommand);
            Object[] correctParamArray = new Object[] { (string)"x", (string)"12" };

            string inputStrB = "if x12 == 12";
            //commandParser.FullParse("x = 100");
            string[] splitCommandB = CommandParser.CommandSplit(inputStrB);
            string strCommandB = CommandParser.CommandExtract(splitCommandB);
            
            





            CollectionAssert.AreEqual(paramArray, correctParamArray);
            Assert.ThrowsException<System.Exception>(() => commandParser.ParamExtract(splitCommandB, strCommandB));



        }
        [TestMethod]
        public void ExecuteCommand_IF_Tests()
        {
            //need to change currently just pasted 
            //need to include the setting of in if etc
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = (new Bitmap(100, 100));
            GraphicsHandler graphicsHandler = new GraphicsHandler(pictureBox);
            CommandParser commandParser = new CommandParser();
            commandParser.setGraphicsHandler(graphicsHandler);

            //Act
            commandParser.FullParse("x = 100+5");

            //Assert
            Assert.AreEqual(105, commandParser.variableValues["x"]);
        }

    }
}