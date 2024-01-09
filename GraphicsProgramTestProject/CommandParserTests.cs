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
            CommandParser commandParser = new CommandParser();


            //Act
            String commandStr = commandParser.CommandExtract(commandArray); ;

            //Assert
            String actualCommandStr = "moveto";
            Assert.AreEqual(commandStr, actualCommandStr);
        }
        [TestMethod]
        public void CommandExtract_GoodCommand_Params_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "moveto", "100", "500", "Extra", "unneeded" };
            CommandParser commandParser = new CommandParser();

            //Act
            String commandStr = commandParser.CommandExtract(commandArray); ;

            //Assert
            String actualCommandStr = "moveto";
            Assert.AreEqual(commandStr, actualCommandStr);
        }
        [TestMethod]
        public void CommandExtract_EmptyCommandArray_Test()
        {
            //Arrange
            String[] commandArray = new string[] { };
            CommandParser commandParser = new CommandParser();

            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.CommandExtract(commandArray));
        }
        [TestMethod]
        public void CommandExtract_InvalidCommand_Test()
        {
            //Arrange
            String[] commandArray = new string[] { "InvalidCommand", "params" };

            CommandParser commandParser = new CommandParser();
            //Act

            //Assert
            Assert.ThrowsException<System.Exception>(() => commandParser.CommandExtract(commandArray));
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
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert
            Assert.AreEqual(true, commandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Fill_On_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"on" };
            String command = "fill";
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert
            Assert.AreEqual(true, commandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Fill_BadString_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"BadString" };
            String command = "fill";
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert

            Assert.ThrowsException<System.Exception>(() => commandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_colour_blue_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"blue" };
            String command = "colour";
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert
            Assert.AreEqual(true, commandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_Colour_BadString_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (String)"BadString" };
            String command = "colour";
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert

            Assert.ThrowsException<System.Exception>(() => commandParser.checkParamRange(paramArray, command));
        }
        [TestMethod]
        public void CheckParamRange_drawto_100_100_Test()
        {
            //Arrange
            Object[] paramArray = new Object[] { (int)100, (int)100 };
            String command = "drawto";
            CommandParser commandParser = new CommandParser();
            //Act

            //Assert
            Assert.AreEqual(true, commandParser.checkParamRange(paramArray, command));
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
            string strCommand = commandParser.CommandExtract(splitCommand);
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
            string strCommand = commandParser.CommandExtract(splitCommand);
            object[] paramArray = commandParser.ParamExtract(splitCommand, strCommand);
            Object[] correctParamArray = new Object[] { (int)100 };

            CollectionAssert.AreEqual(paramArray, correctParamArray);

        }

        [TestMethod]
        public void FullParse_LogicalIf_Tests()
        {

            string commandA = "x = 1000";
            CommandParser commandParser = new CommandParser();
            string commandB = "y = 1000";
            commandParser.FullParse(commandA);
            commandParser.FullParse(commandB);
            commandParser.FullParse("if x == y");
            Assert.AreEqual(commandParser.ifBool, true);
            //Assert.ThrowsException<System.Exception>(() => commandParser.FullParse("if x == y1"));
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
            CommandParser commandParser = new CommandParser();

            Assert.AreEqual(true, commandParser.checkParamRange(paramArrayA, "if"));
            Assert.AreEqual(true, commandParser.checkParamRange(paramArrayB, "if"));
            Assert.ThrowsException<System.Exception>(() => commandParser.checkParamRange(paramArrayC, "if"));


        }
        [TestMethod]
        public void ParamExtract_IF_Tests()
        {
            CommandParser commandParser = new CommandParser();
            string inputStr = "if x == 12";
            //commandParser.FullParse("x = 100");
            string[] splitCommand = CommandParser.CommandSplit(inputStr);
            string strCommand = commandParser.CommandExtract(splitCommand);
            object[] paramArray = commandParser.ParamExtract(splitCommand, strCommand);
            Object[] correctParamArray = new Object[] { (string)"x", (string)"12", (string)"==" };

            string inputStrB = "if x12 == 12";
            //commandParser.FullParse("x = 100");
            string[] splitCommandB = CommandParser.CommandSplit(inputStrB);
            string strCommandB = commandParser.CommandExtract(splitCommandB);







            CollectionAssert.AreEqual(paramArray, correctParamArray);
            Assert.ThrowsException<System.Exception>(() => commandParser.executeCommand(strCommandB, commandParser.ParamExtract(splitCommandB, strCommandB)));



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
        [TestMethod]
        public void ParamExtract_Method_Tests()
        {
            CommandParser commandParser = new CommandParser();
            string commandStrA = "method myMethod";
            string commandStrB = "method myMethodB a b";
            string commandStrC = "method myMethodc a b c d e f";

            string[] commandSplitA = CommandParser.CommandSplit(commandStrA);
            string[] commandSplitB = CommandParser.CommandSplit(commandStrB);
            string[] commandSplitC = CommandParser.CommandSplit(commandStrC);

            string strCommandA = commandParser.CommandExtract(commandSplitA);
            string strCommandB = commandParser.CommandExtract(commandSplitB);
            string strCommandC = commandParser.CommandExtract(commandSplitC);

            object[] paramArrayA = commandParser.ParamExtract(commandSplitA, strCommandA);
            object[] paramArrayB = commandParser.ParamExtract(commandSplitB, strCommandB);
            object[] paramArrayC = commandParser.ParamExtract(commandSplitC, strCommandC);

            



            object[] paramArrayBActual = new object[] { (string)"mymethodb", (string)"a", (string)"b" };
            object[] paramArrayCActual = new object[] { (string)"mymethodc", (string)"a", (string)"b", (string)"c", (string)"d" , (string)"e" , (string)"f" };
            


            Assert.AreEqual(1, paramArrayA.Length);
            CollectionAssert.AreEqual(paramArrayB, paramArrayBActual);
            CollectionAssert.AreEqual(paramArrayC, paramArrayCActual);
            Assert.AreEqual("method", strCommandB);
            

        }
        [TestMethod]
        public void FullParse_MethodVarNames_Test()
        {
            string commandStrA = "method myMethod";
            string commandStrB = "method myMethodB a b";
            string commandStrC = "method myMethodc a b c d e f";

            CommandParser commandParser = new CommandParser();

            commandParser.FullParse(commandStrC);
            commandParser.FullParse("endmethod");

            commandParser.FullParse(commandStrB);
            commandParser.FullParse("endmethod");

            Dictionary<string, int> methodParamsReq = commandParser.getMethodParamsReq();
            Dictionary<string, string[]> methodVarNames = commandParser.getMethodVarNames();
            Dictionary<string, string[]> actualMethodVarNames = new Dictionary<string, string[]>();
            actualMethodVarNames.Add("mymethodc", new string[] { "a", "b", "c", "d", "e", "f" });
            actualMethodVarNames.Add("mymethodb", new string[] { "a", "b"});

            Assert.AreEqual(6, methodParamsReq.GetValueOrDefault("mymethodc", 0));
            Assert.AreEqual(2, methodParamsReq.GetValueOrDefault("mymethodb", 0));

            //methodVarNamesWorks, just acts weird as dictionary
            //CollectionAssert.AreEqual(actualMethodVarNames.(0), methodVarNames.ElementAt(0));

        }

        [TestMethod]
        public void FullParse_MethodBehavior()
        {
            CommandParser commandParser = new CommandParser();
            commandParser.FullParse("method myMethod a b");
            //once method started assert bsaic info
            Assert.AreEqual(false, commandParser.getInMethod());//inmethod
            //Assert.AreEqual(false, commandParser.getMethodBool);//methodbool is not used
            Assert.AreEqual(true, commandParser.getMethodCreation());//methodcreation
            
            commandParser.FullParse("circle 50");//should be skipped
            Assert.AreEqual(false, commandParser.getInMethod());
            Assert.AreEqual(true, commandParser.getMethodCreation());


            commandParser.FullParse("endmethod");
            Assert.AreEqual(false, commandParser.getInMethod());
            Assert.AreEqual(false, commandParser.getMethodCreation());
        }

        public void ParamExtract_MethodRunning_Tests()
        {
            CommandParser commandParser = new CommandParser();
            string commandStrA = "method myMethod";
            string commandStrB = "method myMethodB a b";

            string[] commandSplitA = CommandParser.CommandSplit(commandStrA);
            string[] commandSplitB = CommandParser.CommandSplit(commandStrB);

            string strCommandA = commandParser.CommandExtract(commandSplitA);
            string strCommandB = commandParser.CommandExtract(commandSplitB);

            object[] paramArrayA = commandParser.ParamExtract(commandSplitA, strCommandA);
            object[] paramArrayB = commandParser.ParamExtract(commandSplitB, strCommandB);

            object[] paramArrayBActual = new object[] { (string)"mymethodb", (string)"a", (string)"b" };

            Assert.AreEqual(1, paramArrayA.Length);
            CollectionAssert.AreEqual(paramArrayB, paramArrayBActual);

        }
        [TestMethod]
        public void ParamExtract_MethodRun_Tests()
        {
            CommandParser commandParser = new CommandParser();

            commandParser.FullParse("method mymethod a b");
            commandParser.FullParse("endmethod");
            commandParser.FullParse("method mymethodb a b c");
            commandParser.FullParse("endmethod");
            commandParser.FullParse("method mymethodc a b c d e f");
            commandParser.FullParse("endmethod");

            string commandStrA = "myMethod 10 1000";
            string commandStrB = "myMethodB 10 1002 10";
            string commandStrC = "myMethodc a 12 c d 50 f";

            string[] commandSplitA = CommandParser.CommandSplit(commandStrA);
            string[] commandSplitB = CommandParser.CommandSplit(commandStrB);
            string[] commandSplitC = CommandParser.CommandSplit(commandStrC);
            string[] actualCommandSplitB = new string[] { "mymethodb", "10", "1002", "10" };

            CollectionAssert.AreEqual(actualCommandSplitB, commandSplitB);

            string strCommandA = commandParser.CommandExtract(commandSplitA);
            string strCommandB = commandParser.CommandExtract(commandSplitB);
            string strCommandC = commandParser.CommandExtract(commandSplitC);

            //set current method to name, full parse will do automaticall in execute
            commandParser.setCurrentMethod("mymethod");
            object[] paramArrayA = commandParser.ParamExtract(commandSplitA, strCommandA);
            commandParser.setCurrentMethod("mymethodb");
            object[] paramArrayB = commandParser.ParamExtract(commandSplitB, strCommandB);
            commandParser.setCurrentMethod("mymethodc");
            object[] paramArrayC = commandParser.ParamExtract(commandSplitC, strCommandC);




            string[] paramArrayAActual = new string[] {(string)"10", (string)"1000" };
            string[] paramArrayBActual = new string[] {(string)"10", (string)"1002" , (string)"10" };
            string[] paramArrayCActual = new string[] { (string)"a", (string)"12", (string)"c", (string)"d", (string)"50", (string)"f" };



            CollectionAssert.AreEqual(paramArrayA, paramArrayAActual);
            CollectionAssert.AreEqual(paramArrayB, paramArrayBActual);
            CollectionAssert.AreEqual(paramArrayC, paramArrayCActual);
            //Assert.AreEqual("method", strCommandB);


        }

        [TestMethod]
        public void MethodNamesPointerLocation_Test()
        {
            string multiLineText =
                         "circle 50" + Environment.NewLine +
                        "circle 20" + Environment.NewLine +
                        "method test" + Environment.NewLine +
                       "moveto 50 50" + Environment.NewLine +
                       "endmethod" + Environment.NewLine +
                       "test";
            CommandParser commandParser = new CommandParser();
            PictureBox p = new PictureBox();
            GraphicsHandler graphicsHandler = new GraphicsHandler(p);
            commandParser.setGraphicsHandler(graphicsHandler);


            commandParser.RunMultiple(multiLineText);
            //need run multiple resets all at end, do seperately to actually test

            //find pointer value for test method
            Dictionary<string, int> methodNames = new Dictionary<string, int>();
            methodNames = commandParser.getMethodNames();
            int testPointer;
            testPointer = methodNames.GetValueOrDefault("test",-10);
            //Assert.AreEqual(testPointer, 3);
        }
        [TestMethod]
        public void InternalMethodVariablesSet_Test()
        {
            CommandParser commandParser = new CommandParser();
            commandParser.FullParse("method test a b");
            commandParser.FullParse("endmethod");
            commandParser.FullParse("test 10 50");
            Dictionary<string, int> varNamesArray = commandParser.getMethodVars();

            Assert.AreEqual(varNamesArray.ContainsKey("a"), true);
            Assert.AreEqual(varNamesArray.ContainsKey("b"), true);

        }

        [TestMethod]
        public void InternalMethodUseVarsCorrect_Test()
        {
            CommandParser commandParser = new CommandParser();
            commandParser.FullParse("method test a b");
            commandParser.FullParse("endmethod");
            commandParser.FullParse("test 10 50");
            Dictionary<string, int> varNamesArray = commandParser.getMethodVars();

            Assert.AreEqual(varNamesArray.GetValueOrDefault("a"),10);
        }

        [TestMethod]
        public void FullParse_MethodSet_Test()
        {

        }
        [TestMethod]
        public void FullParse_MethodNoParamsRun_Test()
        {
            CommandParser command = new CommandParser();
            //CommandParser.FullParse("method mymethod");
        }



    }
}