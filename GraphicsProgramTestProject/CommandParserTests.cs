using GraphicsProgram;
namespace GraphicsProgramTestProject

{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void CommandSplitTest()
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
        }
    }
}