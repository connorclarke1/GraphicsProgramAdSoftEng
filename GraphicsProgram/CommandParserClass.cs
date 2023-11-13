using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GraphicsProgram
{

	public class CommandParser
	{
		GraphicsHandler? graphicsHandler;//add global exeption 
		private String? multilineText;

		public void setGraphicsHandler(GraphicsHandler g) { graphicsHandler = g; }
        /// <summary>
        ///  Attempts to execute a command
        /// <br/>Example:<br/> Call this method to draw a triangle onto the bitmap image
        ///     <code>
        ///     FullParse("circle 50");
        ///     </code>
        /// This will draw a circle of radius 50
		/// 
		/// Will create pop-up error message if command incorrect
        /// </summary>
        /// <param name="inCommand">Command and param String in</param>
        /// <returns>void</returns>
        public void FullParse(string inCommand)
		{
			string[] splitCommand = CommandSplit(inCommand);
			try { 
			string strCommand = CommandExtract(splitCommand); 
			object[] paramArray = ParamExtract(splitCommand, strCommand);
            executeCommand(strCommand, paramArray);
			}
			catch (Exception e) {
				String errorMessage = "PlaceHolder";
				errorMessage= e.Message;
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        /// <summary>
        /// Splits Command at whitespace and removes extra whitespace
        /// <br/>Example:<br/> Call this method to split a command and param into a string array
        ///     <code>
        ///     CommandSplit("circle 50");
        ///     </code>
        /// This will return String[] {"circle", "50"}
        /// </summary>
        /// <param name="commandStr">Full Command String</param>
        /// <returns>String[] ArrayOfStrings</returns>
        public static string[] CommandSplit(String commandStr)
		{
			string[] parsedStr = commandStr.ToLower().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			return parsedStr;
		}

        /// <summary>
        /// Extracts command from first position in array of split strings
        /// <br/>Example:<br/> Call this method to confirm command is valid
        ///     <code>
        ///     CommandExtract("circle 50");
        ///     </code>
        /// This will return "circle"
		/// Invalid Commands will throw Exceptions
        /// </summary>
        /// <param name="commandArray">Full Command Array from SplitCommand</param>
        /// <returns>String Command</returns>
        public static string CommandExtract(String[] commandArray)
		{
			string[] validCommands = { "circle" ,
										"moveto",
										"drawto",
										"clear",
										"reset",
										"rectangle",
										"circle",
										"triangle",
										"colour",
										"fill",
										"run"};
			if (commandArray.Length == 0)
			{
				throw new Exception("Command Array Empty");
			}
			if (validCommands.Contains(commandArray[0].ToLower()))
			{
				string commandExtracted = commandArray[0].ToLower();
				return commandExtracted;
			}
			else
			{
				throw new Exception("Invalid command: " + commandArray[0]);
			}
			
		}

        /// <summary>
        /// Returns parameters for given command, will have type depending on command and will throw errors if incorrect parameters are inputted
        /// <br/>Example:<br/> Call this method to get command parameters
        ///     <code>
        ///     ParamExtract("circle 50");
        ///     </code>
        /// This will return int[] {50}
		/// 
		/// <code>
        ///     ParamExtract("fill off");
        ///     </code>
        /// This will return string[] {"off"}
		/// 
		/// <code>
        ///     ParamExtract("rectangle 50 50");
        ///     </code>
        /// This will return int[] {50, 50}
		/// 
		/// 
        /// </summary>
        /// <param name="commandArray">Full Command Array from SplitCommand</param>
		/// <param name="commandStr">Command String from ExtractCommand</param>
        /// <returns>object[] ArrayOfParamObjects</returns>
        public static object[] ParamExtract(String[] commandArray, string commandStr)
		{
			object[] paramArray;
			Type[] typeArray;
			String[] NoParams = { "clear", "reset","run" };
			String[] OneIntParams = {"circle"};
			String[] TwoIntParams = {"moveto", "drawto", "rectangle"};
			String[] FourIntParams = { "triangle" };
			String[] OneStrParams = {"colour", "fill"};
			if (NoParams.Contains(commandStr))
			{
				if (commandArray.Length != 1) { throw new Exception("Command: " + commandStr + " should have no parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				paramArray = ParamExtractArray(commandArray, new Type[0]);
			}
			else if (OneIntParams.Contains(commandStr))
			{

				if (commandArray.Length != 2) { throw new Exception("Command: " + commandStr + " should have one integer parameter, " + (commandArray.Length - 1).ToString() + " given."); }
				if (!(int.TryParse(commandArray[1], out _))) { throw new Exception("Command: " + commandStr + " Non-int value invalid, Please use integers"); }
				typeArray = new Type[1];
				typeArray[0] = typeof(int);
				paramArray = ParamExtractArray(commandArray, typeArray);
			}
			else if (TwoIntParams.Contains(commandStr))
			{
				if (commandArray.Length != 3) { throw new Exception("Command: " + commandStr + " should have two integer parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				for (int i =1; i < commandArray.Length; i++) 
				{ 
					if (!(int.TryParse(commandArray[i], out _))) { throw new Exception("Command: " + commandStr + " Non-int value at position " + i.ToString() + " invalid, Please use integers"); }
				}
				typeArray = new Type[2];
                typeArray[0] = typeof(int);
                typeArray[1] = typeof(int);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
            else if (FourIntParams.Contains(commandStr))
            {
                if (commandArray.Length != 5) { throw new Exception("Command: " + commandStr + " should have four integer parameters, " + (commandArray.Length - 1).ToString() + " given."); }
                for (int i = 1; i < commandArray.Length; i++)
                {
                    if (!(int.TryParse(commandArray[i], out _))) { throw new Exception("Command: " + commandStr + " Non-int value at position " + i.ToString() + " invalid, Please use integers"); }
                }
                typeArray = new Type[4];
                typeArray[0] = typeof(int);
                typeArray[1] = typeof(int);
                typeArray[2] = typeof(int);
                typeArray[3] = typeof(int);
                paramArray = ParamExtractArray(commandArray, typeArray);
            }
            else if (OneStrParams.Contains(commandStr))
            {
                if (commandArray.Length != 2) { throw new Exception("Command: " + commandStr + " should have one string parameter, " + (commandArray.Length - 1).ToString() + " given."); }
				typeArray = new Type[1];
                typeArray[0] = typeof(string);
                paramArray = ParamExtractArray(commandArray, typeArray);
				checkParamRange(paramArray, commandStr);
            }
			else { return null; }//will never run as command has already been checked but add global exception handling

			return paramArray;
		}

        /// <summary>
        /// Converts Parameters to correct Type, throws errors if incompatible type
        /// Used within CommandParser.ParamExtract
        /// </summary>
        /// <param name="commandArray">Full Command Array from SplitCommand</param>
        /// <returns>object[] ParamsExtracted</returns>
        public static object[] ParamExtractArray(String[] commandArray, Type[] typeArray)
		{
			object[] paramArray;
			if ((commandArray.Length - 1) != typeArray.Length) { throw new Exception("Internal Error: Incorrect Exceptions Parsed to ParamExtractArray"); }//incorrect amount of params
            paramArray = new object[commandArray.Length - 1];

            for (int i = 0; i < commandArray.Length - 1; i++)
            {
                if (!TryConvert(commandArray[i+1], typeArray[i]))
                {
					throw new Exception("Parameter Conversion to Int Failed");
                }
                paramArray[i] = Convert.ChangeType(commandArray[i + 1], typeArray[i]);
            }
			return paramArray;
        }

        /// <summary>
        /// Checks Parameters are in range
        /// <br/>Example:<br/> Call this method to confirm parameter is valid
        ///     <code>
        ///     checkParamRange("circle 50");
        ///     </code>
        /// This will return true
        /// Parameters out of range will return false, for example fill yes, must be on or off
        /// </summary>
        /// <param name="paramArray">Full Param Array from ParamExtract</param>
		/// <param name="commandStr">Command String from commandExtract</param>
        /// <returns>Bool inRange</returns>
        public static bool checkParamRange(Object[] paramArray, string commandStr) 
		{
			if (paramArray.Length == 0) { return true;}
            else if (paramArray[0] is String)
            //all commands only have one type of param, eg all ints or strings
            {
				string[] colours = { "black","blue","green","red", "white","yellow" };
				string[] onOff = {"on" , "off"};
				if (commandStr == "fill")
				{
					if (onOff.Contains(paramArray[0]))
					{
						return true;
					}
					else
					{
						throw new Exception("Fill parameter must be on/off");
					}
				}
                if (commandStr == "colour")
                {
                    if (colours.Contains(paramArray[0]))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Colour: " + paramArray[0] + " not supported");
                    }
                }
				//only int params can get here and any int is in range just may not be visible if too big
                return true;
            }

            else if (paramArray[0] is int)
            //all commands only have one type of param, eg all ints or strings
            {
				return true; //All int params should be okay since the graphics can cut off parts out of range
				//potentially add out of range for extremely large numbers
			}
			else { return false; }//Global Exception
        }

        /// <summary>
        /// Returns a true or false for a values conversion possibility
        /// </summary>
        /// <param name="value">Value to attempt to convert</param>
        /// <param name="targetType">Type to attempt to convert to</param>
        /// <returns>Bool convertable</returns>
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

        /// <summary>
        /// Sets text from multiline box to be used
        /// </summary>
        /// <param name="text">Text from textbox</param>
        /// <returns>void</returns>
        public void SetMultilineText(String text)
		{
			multilineText = text;
		}

        /// <summary>
        /// Executes Command, takes commandStr once validated, executes necesarry code for command
        /// </summary>
        /// <param name="strCommand">Validated Command String</param>
        /// <param name="paramArray">Validated parameter array for command</param>
        /// <returns>void</returns>
        public void executeCommand(String strCommand, object[] paramArray)
		{
			if (strCommand == "colour") 
			{
				String colourString = (String)(paramArray[0]);
				graphicsHandler.SetColour(StringToColour.Convert(colourString)); 
			}
			if (strCommand == "moveto")
			{
				int[] intArray = new int[2];
				intArray[0] = (int)paramArray[0];
                intArray[1] = (int)paramArray[1];
                graphicsHandler.pointer.SetPointerPos(intArray);
			}
            if (strCommand == "drawto")
            {
                int[] intArray = new int[2];
                intArray[0] = (int)paramArray[0];
                intArray[1] = (int)paramArray[1];
                DrawTo.Draw(graphicsHandler, intArray[0], intArray[1]);
            }
			if (strCommand == "clear")
			{
				Clear.ClearMethod(graphicsHandler);
			}
			if (strCommand == "reset")
			{
				graphicsHandler.pointer.SetPointerXPos(0);
				graphicsHandler.pointer.SetPointerYPos(0);
			}
			if (strCommand == "fill")
			{
				if ((String)paramArray[0] == "on"){ graphicsHandler.SetFill(true); }
                if ((String)paramArray[0] == "off") { graphicsHandler.SetFill(false); }
            }
			if (strCommand == "circle")
			{
				Circle.Draw(graphicsHandler, (int)paramArray[0]);
			}
			if (strCommand == "rectangle")
			{
				Rectangle.Draw(graphicsHandler, (int)paramArray[0], (int)paramArray[1]);
			}
			if (strCommand == "triangle")
			{
				Triangle.Draw(graphicsHandler, (int)paramArray[0], (int)paramArray[1], (int)paramArray[2], (int)paramArray[3]);
			}
			if (strCommand == "run")
			{
				if (GetSyntaxErrorArray().Length != 0) { throw new Exception("Errors in multi line program please use syntax button"); }
				//Commands String Array Get done on button press
				RunMultiple(multilineText);
				//Execute commands in for loop
			}

        }

        /// <summary>
        /// Runs multiple commands from multiple command textbox, splits text at line breakpoints
        /// </summary>
        /// <param name="multilineText">Text from multiline text box</param>
        /// <returns>void</returns>
        public void RunMultiple(String multilineText) 
		{
			if (multilineText == null)
			{
				throw new Exception("Nothing in multiple command text area");
			}
			String[] multiCommandArray = multilineText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
			for (int i=0; i < multiCommandArray.Length; i++ )
			{
				FullParse(multiCommandArray[i]);
			}

        }

        /// <summary>
        /// Runs through multi line program and records syntax errors and lines
        /// </summary>
        /// <returns>String[] ArrayOfSyntaxErrors</returns>
        public String[] GetSyntaxErrorArray()
		{
			if (multilineText == null)
			{
				return new String[0];
			}
			String[] multiCommandArray = multilineText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

			String[] errorMessages = new string[0];
			for (int i = 0; i < multiCommandArray.Length; i++)
			{
				string[] splitCommand = CommandSplit(multiCommandArray[i]);
				try
				{
					string strCommand = CommandExtract(splitCommand);
					object[] paramArray = ParamExtract(splitCommand, strCommand);
					executeCommand(strCommand, paramArray);
					Clear.ClearMethod(graphicsHandler);
				}
				catch (Exception e)
				{
					Array.Resize(ref errorMessages, errorMessages.Length + 1);
					errorMessages[errorMessages.Length - 1] = ("Line "+ (i+1).ToString() + ": " + e.Message);
				}
			}
			return errorMessages;
		}

        /// <summary>
        /// RCreates pop up box displaying syntax errors in multiline program, will also inform user if none present
        /// </summary>
        /// <returns>void</returns>
        public void CheckSyntaxMessage() {

			String[] errorMessages = GetSyntaxErrorArray();
			if (multilineText.Length == 0)
			{
                MessageBox.Show("Multiple line program box empty", "Syntac Checker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (errorMessages.Length > 0) 
			{ 
				String errorMessage = string.Join(Environment.NewLine, errorMessages);
				MessageBox.Show(errorMessage, "Syntac Checker", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
                MessageBox.Show("No Errors","Syntax Checker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //return errorMessages;
        }

		


    }
}