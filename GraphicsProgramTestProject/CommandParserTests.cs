using GraphicsProgram;
namespace GraphicsProgramTestProject

{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void CommandSplit_Capitals_Test()
        {
            //Arrange
            //CommandParser commandParser = new CommandParser(); static so no need to create
            String command = "Circle 20";


            //Act
            String[] splitCommand;
            splitCommand = CommandParser.CommandSplit(command);

            //Assert
            String[] actualSplitCommand;
            actualSplitCommand = new string[]{"circle", "20"};
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
            String[] commandArray = new string[] {"moveto"};


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
            String[] commandArray = new string[] { "moveto" , "100", "500" ,"Extra", "unneeded"};


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
            String[] commandArray = new string[] {"InvalidCommand", "params"};


            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => CommandParser.CommandExtract(commandArray));
        }
        [TestMethod]
        public void ParamExtract_NoParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] {"clear"};
            String command = "clear";

            //Act
            Object[] Params = new object[] { };

            //Assert
            CollectionAssert.AreEqual(Params, CommandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_OneIntParam_WithStringInstead_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "circle", "one" };
            String command = "circle";


            //Assert
            Assert.ThrowsException<System.Exception>(() => CommandParser.ParamExtract(commandArray, command));
        }
        [TestMethod]
        public void ParamExtract_OneIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "circle", "100" };
            String command = "circle";

            //Act
            Object[] Params = new Object[] { 100 };

            //Assert
            CollectionAssert.AreEqual(Params, CommandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(int));
        }
        [TestMethod]
        public void ParamExtract_TwoIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "drawto", "100", "200" };
            String command = "drawto";

            //Act
            Object[] Params = new Object[] { 100 , 200};

            //Assert
            CollectionAssert.AreEqual(Params, CommandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(int));
            Assert.IsInstanceOfType(Params[1], typeof(int));
        }
        [TestMethod]
        public void ParamExtract_FourIntParam_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "triangle", "100", "200" , "500", "300"};
            String command = "triangle";

            //Act
            Object[] Params = new Object[] { 100, 200, 500, 300 };

            //Assert
            CollectionAssert.AreEqual(Params, CommandParser.ParamExtract(commandArray, command));
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

            //Act
            Object[] Params = new Object[] {"on"};

            //Assert
            CollectionAssert.AreEqual(Params, CommandParser.ParamExtract(commandArray, command));
            Assert.IsInstanceOfType(Params[0], typeof(string));
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
            Object[] paramArray = new Object[] {(String)"on"};
            String command = "fill";

            //Act

            //Assert
            Assert.AreEqual(true, CommandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Fill_BadString_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] {(String)"BadString"};
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
    }
}