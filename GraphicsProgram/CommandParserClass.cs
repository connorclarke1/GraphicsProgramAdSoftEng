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
			object[] paramArray = ParamExtract(splitCommand, strCommand);
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
				string commandExtracted = commandArray[0].ToLower();
				return commandExtracted;
			}
			else
			{
				//TODO add command error flag here
				return "Command not valid";
			}
			
		}

		public static object[] ParamExtract(String[] commandArray, string commandStr)
		//up to two parameters for each command
		//TODO add error for params
		//TODO create way to return parameters after checking
		{
			object[] paramArray;
			Type[] typeArray;
			String[] NoParams = { "clear", "reset" };
			String[] OneIntParams = {"circle"};
			String[] TwoIntParams = {"moveto", "drawto", "rectangle"};
			String[] FourIntParams = { "triangle" };
			String[] OneStrParams = {"colour", "fill"};
			if (NoParams.Contains(commandStr)) 
			{
				paramArray = ParamExtractArray(commandArray, new Type[0]);
			}
            else if (OneIntParams.Contains(commandStr))
            {
				typeArray = new Type[1];
				typeArray[0] = typeof(int);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
            else if (TwoIntParams.Contains(commandStr))
            {
                typeArray = new Type[2];
                typeArray[0] = typeof(int);
                typeArray[1] = typeof(int);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
            else if (FourIntParams.Contains(commandStr))
            {
                typeArray = new Type[4];
                typeArray[0] = typeof(int);
                typeArray[1] = typeof(int);
                typeArray[2] = typeof(int);
                typeArray[3] = typeof(int);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
            else if (OneStrParams.Contains(commandStr))
            {
                typeArray = new Type[1];
                typeArray[0] = typeof(string);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
			else { return null; }//will never run as command has already been checked but add global exception handling

			return paramArray;
		}

		public static object[] ParamExtractArray(String[] commandArray, Type[] typeArray)
		{
			//TODO add range arrays, tuples for ints, list for strings
			object[] paramArray;
			if ((commandArray.Length - 1) != typeArray.Length) { return null; }//incorrect amount of params
            paramArray = new object[commandArray.Length - 1];

            for (int i = 0; i < commandArray.Length - 1; i++)
            {
                if (!TryConvert(commandArray[i+1], typeArray[i]))
                {
                    return null; // Conversion failed for at least one element, type error
                }
                paramArray[i] = Convert.ChangeType(commandArray[i + 1], typeArray[i]);
            }//now param array is filled with correct types
			return paramArray;
        }

		static void checkParamRange() 
		{
			String[] paramArray = {"PlaceHolder"};
            if (paramArray[0] is String)
            //all commands only have one type of param, eg all ints or strings
            {

            }

            if (paramArray[0] is int)
            //all commands only have one type of param, eg all ints or strings
            {

            }
        }

        static bool TryConvert(string value, Type targetType)
        {
            try
            {
                // Use Convert.ChangeType to attempt the conversion
                Convert.ChangeType(value, targetType);
                return true;
            }
            catch (Exception)
            {
                return false; // Conversion failed
            }
        }
    }
}