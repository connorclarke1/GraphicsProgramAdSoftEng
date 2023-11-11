using System;
using System.Linq;

namespace GraphicsProgram
{

	public class CommandParser
	{
		public static void FullParse(string inCommand)
		// returns array with command, and parameters, fully checked
		//TODO change void to array so returns
		//TODO take command from textbox
		{
			string[] splitCommand = CommandSplit(inCommand);
			string strCommand = CommandExtract(splitCommand);
		}
		public static string[] CommandSplit(String commandStr)
		//Splits command into array of strings, splits at whitespace and removes empty enteries, e.g double spaces
		//TODO make all lower case to avoid case issues
		{
			string[] parsedStr = commandStr.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			return parsedStr;
		}

		public static string CommandExtract(String[] commandArray)
		//TODO add commandArray null exception
		//TODO update command list as they are created
		{
			//array of valid commands in lower case
			string[] validCommands = { "circle" ,
										"moveto",
										"drawto",
										"clear",
										"reset",
										"rectangle",
										"circle",
										"triangle",
										"colour",
										"fill"};
			//commandArray[0] will always be command
			if (commandArray.Length == 0)
			{
				//TODO add commandArray null error flag
				return "Error, commandArray null";
			}
			if (validCommands.Contains(commandArray[0].ToLower()))
			{
				string commandExtracted = commandArray[0];
				return commandExtracted;
			}
			else
			{
				//TODO add command error flag here
				return "Command not valid";
			}
			
		}

		public static object[] ParamExtract(String[] commandArray)
		//up to two parameters for each command
		//TODO add error for params
		//TODO create way to return parameters after checking
		{
			object[] paramArray;
			if (commandArray[0] == "circle")
			{
				if (commandArray.Length == 2)
				{
					//if parameter is integer
					if (int.TryParse(commandArray[1], out _))
					{
						paramArray = new object[1];
						//already confirmed param is integer
						paramArray[0] = int.Parse(commandArray[1]);
						return paramArray;
					}
					else
					{
						//return "Parameter incorrect, must be integer";
						return null;
					}
				}
			}
			else if (commandArray[0] == "moveto")
			{
				if (commandArray.Length == 3)
				{
					//if parameter is integer
					if (int.TryParse(commandArray[1], out _))
					{
						paramArray = new object[2];
						//already confirmed param is integer
						paramArray[0] = int.Parse(commandArray[1]);
						paramArray[1] = int.Parse(commandArray[2]);
						return paramArray;
					}
					else
					{
						//return "Parameter incorrect, must be integer";
						return null; //add errors here, only one param or not int
					}
				}

				else
				{
					//return "Error incorrect amount of params, should be 1";
					return null;
				}

			}
				
			
			return null;//error exception of command invalid, should never get here since validated already
		}
	}
}