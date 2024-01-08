using GraphicsProgram;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GraphicsProgramTestProject

{
    [TestClass]
    public class OperationLogicTest
    {
        [TestMethod]
        public void CheckLogicNormalOperation_Test()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x-y";
            //Assert
            Assert.AreEqual(true, CheckLogic.Check(myLogic, variableValues));
        }


        [TestMethod]
        public void Check_SplitAtOperations_Test()
        {

            string logicStr = new string("1+3+5");
            string[] operations = new string[] { "+", "-", "/", "*" };
            string[] actualSplit = new string[] { "1", "+", "3", "+", "5" };
            string[] executedSplit = CheckLogic.splitAtOperations(logicStr);

            //Assert.AreEqual(executedSplit, actualSplit);
            Assert.IsTrue(executedSplit.SequenceEqual(actualSplit));
        }

        [TestMethod]
        public void Check_SplitAtOperations_Test2()
        {

            string logicStr = new string("1+35+5*12/5");
            string[] operations = new string[] { "+", "-", "/", "*" };
            string[] actualSplit = new string[] { "1", "+", "35", "+", "5", "*", "12", "/", "5" };
            string[] executedSplit = CheckLogic.splitAtOperations(logicStr);

            //Assert.AreEqual(executedSplit, actualSplit);
            Assert.IsTrue(executedSplit.SequenceEqual(actualSplit));
        }

        [TestMethod]
        public void CheckLogicNormalOperation_Test2()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x-y*123-12";
            //Assert
            Assert.AreEqual(true, CheckLogic.Check(myLogic, variableValues));
        }

        [TestMethod]
        public void CheckLogic_EndOnOperator_Test()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x-y*123-12+";
            //Assert
            Assert.AreEqual(false, CheckLogic.Check(myLogic, variableValues));
        }

        [TestMethod]
        public void CheckLogicNonExistentVar_Test()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x-a*123-12";
            //Assert
            Assert.ThrowsException<System.Exception>(() => CheckLogic.Check(myLogic, variableValues));
        }

        [TestMethod]
        public void CheckLogicLongVarName_Test()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["abcdef"] = 30;
            string myLogic = "2+x-abcdef*123-12";
            //Assert
            Assert.AreEqual(true, CheckLogic.Check(myLogic, variableValues));
        }

        [TestMethod]
        public void ExecuteLogic_Simple_Test1()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x";
            //Assert
            Assert.AreEqual(12, ExecuteLogic.Execute(myLogic, variableValues));
        }
        [TestMethod]
        public void ExecuteLogic_Longer_Test1()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 20;
            variableValues["z"] = 30;
            string myLogic = "2+x*5";
            //Assert
            Assert.AreEqual(60, ExecuteLogic.Execute(myLogic, variableValues));
        }
        [TestMethod]
        public void TestContainsIntegerFalse()
        {
            bool check = CommandParser.ContainsInteger(("x").ToString());
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void TestContainsIntegerTrue()
        {
            bool check = CommandParser.ContainsInteger(("x1y").ToString());
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void ExecuteLogic_Longer_AllOps_Test1()
        {
            //Arrange
            Dictionary<string, int> variableValues = new Dictionary<string, int>();

            //Act
            variableValues["x"] = 10;
            variableValues["y"] = 2;
            variableValues["z"] = 3;
            string myLogic = "2+x*5/z-2";
            //Assert
            Assert.AreEqual(18, ExecuteLogic.Execute(myLogic, variableValues));
        }




    }
}