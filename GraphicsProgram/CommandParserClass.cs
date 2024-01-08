using System;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace GraphicsProgram
{

	public class CommandParser
	{
		GraphicsHandler? graphicsHandler;
		private String? multilineText;
        public Dictionary<string, int> variableValues;

        public CommandParser()
        {
            //Dictionary<string, int> 
            variableValues = new Dictionary<string, int>();
            //variableValues.Add("UnaccessablePlaceHolder1", 1000);
        }

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
			string[] parsedStr = new string[] { };
			if (commandStr.Count(c => c == '=') == 1) 
			{
				parsedStr = commandStr.Split("="); 
				//should only have 2 items in array
				Array.Resize(ref parsedStr, parsedStr.Length + 1);//should be 2 -> 3
				parsedStr[2] = RemoveWhitespace(parsedStr[1]);
				parsedStr[1] = RemoveWhitespace(parsedStr[0]);
				parsedStr[0] = "var";
				//increase size by one, make [0] "var" for use in rest of parser
			}//while loops etc have double equals
			else {parsedStr = commandStr.ToLower().Split(new char[0], StringSplitOptions.RemoveEmptyEntries); }
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
										"run",
										"var"};
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
                commandArray = commandArray;
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
        public object[] ParamExtract(String[] commandArray, string commandStr)
		{
			object[] paramArray;
			Type[] typeArray;
			String[] NoParams = { "clear", "reset","run" };
			String[] OneIntParams = {"circle"};
			String[] TwoIntParams = {"moveto", "drawto", "rectangle"};
			String[] FourIntParams = { "triangle" };
			String[] OneStrParams = {"colour", "fill"};
			String[] TwoStrParams = {"var"};
			if (NoParams.Contains(commandStr))
			{
				if (commandArray.Length != 1) { throw new Exception("Command: " + commandStr + " should have no parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				paramArray = ParamExtractArray(commandArray, new Type[0]);
			}
			else if (OneIntParams.Contains(commandStr))
			{

				if (commandArray.Length != 2) { throw new Exception("Command: " + commandStr + " should have one integer parameter, " + (commandArray.Length - 1).ToString() + " given."); }
                if (!(int.TryParse(commandArray[1], out _)))
                {
                    //if variable exists then fine
                    if (variableValues.ContainsKey(commandArray[1]))
                    {
                        commandArray[1] = variableValues.GetValueOrDefault(commandArray[1], 0).ToString();
                    }
                    else
                    {
                        throw new Exception("Command: " + commandStr + " Non-int value invalid, Please use integers or assigned variables");
                    }
                }
				typeArray = new Type[1];
				typeArray[0] = typeof(int);
				paramArray = ParamExtractArray(commandArray, typeArray);
			}
			else if (TwoIntParams.Contains(commandStr))
			{
				if (commandArray.Length != 3) { throw new Exception("Command: " + commandStr + " should have two integer parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				for (int i =1; i < commandArray.Length; i++) 
				{ 
					if (!(int.TryParse(commandArray[i], out _))) 
                    {
                        if (variableValues.ContainsKey(commandArray[i]))
                        {
                            commandArray[i] = variableValues.GetValueOrDefault(commandArray[i], 0).ToString();
                        }
                        else
                        {
                            throw new Exception("Command: " + commandStr + " Non-int value at position " + i.ToString() + " invalid, Please use integers of assigned variables");
                        }
                    }
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
                    if (!(int.TryParse(commandArray[i], out _)))
                    {
                        if (variableValues.ContainsKey(commandArray[i]))
                        {
                            commandArray[i] = variableValues.GetValueOrDefault(commandArray[i], 0).ToString();
                        }
                        else 
                        { 
                        throw new Exception("Command: " + commandStr + " Non-int value at position " + i.ToString() + " invalid, Please use integers or assigned variable");
                        }
                    }
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
            else if (TwoStrParams.Contains(commandStr))
            {
                if (commandArray.Length != 3) { throw new Exception("Invalid Variable assignment"); }
                typeArray = new Type[2];
                typeArray[0] = typeof(string);
				typeArray[1] = typeof(string);
                paramArray = ParamExtractArray(commandArray, typeArray);
                checkParamRange(paramArray, commandStr);
            }
            else { return null; }

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
				//only need to check int 
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
				if (commandStr == "var")
				{
					//if paramArray[0] has no integers
					if (!ContainsInteger(paramArray[0].ToString()))
                    {
                        //if paramArray[1] checkLogic
                        if (CheckLogic.Check(paramArray[1].ToString(), null)) 
                        {
                            return true;
                        }
                        //else exception
                        else
                        {
                            throw new Exception("Invalid Logic when setting variable");
                        }
                    }
                    //else exception
                    else
                    {
                        throw new Exception("Variable name must not contain integer");
                    }
					
				}
				//only int params can get here and any int is in range even when large
                return true;
            }

            else if (paramArray[0] is int)
            //all commands only have one type of param, eg all ints or strings
            {
				return true; //All int params should be okay since the graphics can cut off parts out of range
				//potentially add out of range for extremely large numbers
			}
			else { return false; }
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
				
				RunMultiple(multilineText);
				
			}
            if (strCommand == "var")
            {
                //check if var in dictionary
                if (variableValues.ContainsKey((string)paramArray[0])) { variableValues[(string)paramArray[0]] = ExecuteLogic.Execute((string)paramArray[1],variableValues); }
                //update var to result of logic
                //else
                else { variableValues.Add((string)paramArray[0], ExecuteLogic.Execute((string)paramArray[1], variableValues)); }
                //string logicStr = (string)paramArray[1];
                //int result = ExecuteLogic.Execute((string)paramArray[1], variableValues);
                //variableValues[(string)paramArray[0]] = ExecuteLogic.Execute((string)paramArray[1], variableValues);
                ///create var in dict
                ///update var to result of logic
                ///
                //this should create the key in the dictionary if not already there
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
        /// Creates pop up box displaying syntax errors in multiline program, will also inform user if none present
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
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static bool ContainsInteger(string input)
        {
            foreach (char c in input)
            {
                if (int.TryParse(c.ToString(), out _))
                {
                    return true;
                }
            }
            return false;
        }


    }
}