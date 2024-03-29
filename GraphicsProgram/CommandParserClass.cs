﻿namespace GraphicsProgram
{
    using System.Threading;
    public class CommandParser
	{
		GraphicsHandler? graphicsHandler;
		private String? multilineText;
        public Dictionary<string, int> variableValues;
        private bool insideIf;
        public bool ifBool;
        private int pointer;
        private int whileStart;
        private bool insideWhile;
        private bool whileBool;
        private string[] whileLogic;
        private string[] operators = new string[] {"<=","<","==",">=",">" };
        private Dictionary<string, int> methodStartPointers;
        private bool inMethod;
        private bool methodBool;
        private int methodResume;
        private bool methodCreation;
        public Dictionary<string, int> methodNames; //name, pointer
        private Dictionary<string, int> methodParamsReq; // name, params required
        private Dictionary<string, int> methodVars; //same as variableValues but to be used in methods
        private Dictionary<string, string[]> methodVarNames;
        private string currentMethod;
        private string settingMethod;
        private bool checkingSyntax;

        public CommandParser()
        {
            variableValues = new Dictionary<string, int>();
            methodNames = new Dictionary<string, int>();
            methodStartPointers = new Dictionary<string, int>();
            methodVars = new Dictionary<string, int>();
            methodParamsReq = new Dictionary<string, int>();
            methodVarNames = new Dictionary<string, string[]>();
            insideIf = false;
            ifBool = false;
            pointer = 0;
            whileStart = -1;
            whileBool = false;
            insideWhile = false;
            whileLogic =new string[] {"","" , ""};
            methodBool = false;
            methodResume = -1;
            inMethod = false;
            methodCreation = false;
            currentMethod = "";
            settingMethod = "";
            checkingSyntax = false;           
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
        public void FullParse(string inCommand, int pointer = 0)
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
            //have to check of there is single = as there is no prefix when setting variables with x = 10 etc
            //only if single equals and no < or > since while x <= 5 is valid
			if (commandStr.Count(c => c == '=') == 1 && commandStr.Count(c => c == '<') == 0 && commandStr.Count(c => c == '>') == 0) 
			{
				parsedStr = commandStr.Split("="); 
				Array.Resize(ref parsedStr, parsedStr.Length + 1);
				parsedStr[2] = RemoveWhitespace(parsedStr[1]);
				parsedStr[1] = RemoveWhitespace(parsedStr[0]);
				parsedStr[0] = "var";
				
			}
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
        public string CommandExtract(String[] commandArray)  //was static
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
										"var",
                                        "if",
                                        "while",
                                        "endif",
                                        "endwhile",
                                        "method",
                                        "endmethod",
                                        "wait",
                                        "save",
                                        "load"};
			if (commandArray.Length == 0)
			{
				throw new Exception("Command Array Empty");
			}
			if (validCommands.Contains(commandArray[0].ToLower()))
			{
                if (commandArray[0].ToLower() == "var")//if single = so treated as var
                {
                    if(string.Join("",commandArray).Contains("if") || string.Join("", commandArray).Contains("while"))
                    {
                        throw new Exception("Logic Statement(IF/WHILE) Statement must use ==");
                    }
                    
                    else if (string.Join("",commandArray).Count(c => c == '=') > 2)
                    {
                        throw new Exception("Logic Statement IF/While has too many = please use ==");
                    }
                }
				string commandExtracted = commandArray[0].ToLower();
				return commandExtracted;
			}
            else if (methodNames.ContainsKey(commandArray[0]))
            {
                currentMethod = commandArray[0];
                return "methodex";
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
        public object[] ParamExtract(String[] commandArray, string commandStr)
		{
			object[] paramArray;
			Type[] typeArray;
			String[] NoParams = { "clear", "reset","run", "endif", "endwhile" ,"endmethod"};
			String[] OneIntParams = {"circle", "wait"};
			String[] TwoIntParams = {"moveto", "drawto", "rectangle"};
			String[] FourIntParams = { "triangle" };
			String[] OneStrParams = {"colour", "fill", "save", "load"};
            String[] OneVariablesParam =  {"method", "methodex"};
			String[] TwoStrParams = {"var"};
            String[] DoubleLogicalParams = {"if","while" };
            if (methodCreation) { return new string[0]; }
			else if (NoParams.Contains(commandStr))
			{
				if (commandArray.Length != 1) { throw new Exception("Command: " + commandStr + " should have no parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				paramArray = ParamExtractArray(commandArray, new Type[0]);
			}
			else if (OneIntParams.Contains(commandStr))
			{
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }

                if (commandArray.Length != 2) { throw new Exception("Command: " + commandStr + " should have one integer parameter, " + (commandArray.Length - 1).ToString() + " given."); }
                if (!(int.TryParse(commandArray[1], out _)))
                {
                    //if variable exists then fine
                    if (selectedVariableValues.ContainsKey(commandArray[1]))
                    {
                        commandArray[1] = selectedVariableValues.GetValueOrDefault(commandArray[1], 0).ToString();
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
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }

                if (commandArray.Length != 3) { throw new Exception("Command: " + commandStr + " should have two integer parameters, " + (commandArray.Length - 1).ToString() + " given."); }
				for (int i =1; i < commandArray.Length; i++) 
				{ 
					if (!(int.TryParse(commandArray[i], out _))) 
                    {
                        if (selectedVariableValues.ContainsKey(commandArray[i]))
                        {
                            commandArray[i] = selectedVariableValues.GetValueOrDefault(commandArray[i], 0).ToString();
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
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }

                if (commandArray.Length != 5) { throw new Exception("Command: " + commandStr + " should have four integer parameters, " + (commandArray.Length - 1).ToString() + " given."); }
                for (int i = 1; i < commandArray.Length; i++)
                {
                    if (!(int.TryParse(commandArray[i], out _)))
                    {
                        if (selectedVariableValues.ContainsKey(commandArray[i]))
                        {
                            commandArray[i] = selectedVariableValues.GetValueOrDefault(commandArray[i], 0).ToString();
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
            else if (OneVariablesParam.Contains(commandStr))
            {
                if (commandStr == "method") { settingMethod = commandArray[0]; }
                commandArray[0] = "";
                paramArray = new string[commandArray.Length - 1];
                for (int i = 0; i < commandArray.Length - 1; i++)
                {
                    paramArray[i] = commandArray[i + 1];
                }
                checkParamRange(paramArray, commandStr);
            }
            else if (DoubleLogicalParams.Contains(commandStr))
            {
                commandArray[0] = "";
                string commandArrayStr = string.Join("", commandArray);
                string foundOperator = operators.FirstOrDefault(commandArrayStr.Contains, null);//operators in order of => then > so that => found first
                
                if (foundOperator != null)
                {
                    paramArray = commandArrayStr.Split(foundOperator);
                    Array.Resize(ref paramArray, paramArray.Length + 1) ;
                    paramArray[2] = foundOperator;
                }
                
                else
                {
                    throw new Exception("If/While needs ==, <, <=, >=, >");
                }

            }
            else if (commandStr == "thread") 
            {
                paramArray = new string[commandArray.Length - 1];
                for (int i = 0; i < paramArray.Length; i++)
                {
                    paramArray[i] = (string)commandArray[i + 1];

                }

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
				//only check ints 
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
        public bool checkParamRange(Object[] paramArray, string commandStr) 
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
					if (!ContainsInteger(paramArray[0].ToString()))
                    {
                        if (CheckLogic.Check(paramArray[1].ToString(), null)) 
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Invalid Logic when setting variable");
                        }
                    }
                    else
                    {
                        throw new Exception("Variable name must not contain integer");
                    }
				}
                if (commandStr == "if" || commandStr == "while")
                {
                        if (CheckLogic.Check((string)paramArray[0], null))
                        {
                            if (CheckLogic.Check((string)paramArray[1], null))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            throw new Exception("Logic error within statement");
                        }
                }
                if (commandStr == "methodex")
                {
                    if (methodParamsReq.GetValueOrDefault(currentMethod, -1) != paramArray.Length) { throw new Exception("Incorrect Number of params for method " + currentMethod + methodParamsReq.GetValueOrDefault(currentMethod, -1).ToString() + paramArray.Length.ToString()); }
                    for (int i = 0; i < paramArray.Length; i++)
                    {
                        if (!int.TryParse((string)paramArray[i], out _))
                        {
                            if (!paramArray[i].ToString().All(char.IsLetter))
                            {
                                //if not an int then var name, var name must be letters only
                                throw new Exception("Variable name " + paramArray[i] + " is invalid, must be int value or letter");
                            }
                        }
                    }
                    return true;

                }
                if (commandStr == "save" || commandStr == "load") { return true; }

                //only int params can get here and any int is in range even when large
                //or file name on save
                return true;
            }

            else if (paramArray[0] is int)
            //all commands only have one type of param, eg all ints or strings
            {
				return true; //All int params should be okay since the graphics can cut off parts out of range
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
                return false;
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
            
            pointer++;
            if (strCommand == "endwhile")
            {
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }
                if (ExecuteLogic.executeInequality(ExecuteLogic.Execute((string)whileLogic[0], selectedVariableValues), ExecuteLogic.Execute((string)whileLogic[1], selectedVariableValues), (string)whileLogic[2]))
                {
                    pointer = whileStart;
                    return;
                }
                whileBool = false;
                insideWhile = false;
                return;

            }
            if(methodCreation && strCommand != "endmethod"){ return; }//method doesnt execute on creation
            if (strCommand == "endmethod") 
            {
                if (methodCreation && strCommand == "endmethod") { methodCreation = false; return; }
                if (inMethod)
                {
                    inMethod = false;
                    pointer = methodResume;
                    methodVars.Clear();
                }
            }
            if (strCommand == "endif") {insideIf = false; ifBool = false; }
            if (insideIf && !ifBool) { return; } //if inside if statement but not true
            if (insideWhile && !whileBool) { return; }//if in while but not true
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

            if (strCommand == "save") 
            { 
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, (string)paramArray[0]), multilineText);
            }

            if (strCommand == "load")
            {
                string fileText = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, (string)paramArray[0]));
                RunMultiple(fileText);
            }

            if (strCommand == "thread")
            {
                
                Thread[] threadArray;
                
                string[] textArray = new string[paramArray.Length];
                threadArray = new Thread[paramArray.Length];
                

                for (int i = 0; i < paramArray.Length; i++)
                {
                    
                    string myText = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, (string)paramArray[i]));
                    threadArray[i] = new Thread(() => RunMultiple(myText));
                    threadArray[i].Start();
                }
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
            if (strCommand == "wait") { 
                if (!checkingSyntax) { Thread.Sleep((int)paramArray[0]); }
                
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
                resetAllInternalVars();
                RunMultiple(multilineText);
				
			}
            if (strCommand == "var")
            {
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else{selectedVariableValues = variableValues; }
                //check if var in dictionary
                if (selectedVariableValues.ContainsKey((string)paramArray[0])) { selectedVariableValues[(string)paramArray[0]] = ExecuteLogic.Execute((string)paramArray[1],selectedVariableValues); }
                //update var to result of logic
                //else create key
                else { selectedVariableValues.Add((string)paramArray[0], ExecuteLogic.Execute((string)paramArray[1], selectedVariableValues)); }
                
            }
            if (strCommand == "if")
            {
                insideIf = true;
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }
                if (ExecuteLogic.executeInequality(ExecuteLogic.Execute((string)paramArray[0], selectedVariableValues), ExecuteLogic.Execute((string)paramArray[1], selectedVariableValues), (string)paramArray[2]))
                {
                    ifBool = true;
                }
            }
            if (strCommand == "while")
            {
                insideWhile = true;
                whileBool = false;
                whileStart = pointer - 1;
                whileLogic[0] = (string)paramArray[0];
                whileLogic[1] = (string)paramArray[1];
                whileLogic[2] = (string)paramArray[2];
                Dictionary<string, int> selectedVariableValues;
                if (inMethod) { selectedVariableValues = methodVars; }
                else { selectedVariableValues = variableValues; }
                if (ExecuteLogic.executeInequality(ExecuteLogic.Execute((string)whileLogic[0], selectedVariableValues), ExecuteLogic.Execute((string)whileLogic[1], selectedVariableValues), (string)whileLogic[2]))
                {
                    whileBool = true;
                }
            }
            if (strCommand == "method")
            {
                if (paramArray.Length > 1)
                {
                    string[] currentMethodVars = paramArray[1].ToString().ToLower().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    methodNames.Add((string)paramArray[0], pointer); //pointer already increased
                    methodParamsReq.Add((string)paramArray[0], paramArray.Length - 1);
                    methodStartPointers.Add((string)paramArray[0], pointer);

                    string[] methodParamNames = new string[paramArray.Length - 1];
                    for (int i = 0; i < paramArray.Length - 1; i++)
                    {
                        methodParamNames[i] = (string)paramArray[i + 1];
                        
                    }
                    methodVarNames.Add((string)paramArray[0], methodParamNames);
                }
                else
                {
                    methodNames.Add((string)paramArray[0], pointer); //pointer already increased
                    methodParamsReq.Add((string)paramArray[0], 0);
                    methodStartPointers.Add((string)paramArray[0], pointer);
                    string[] methodParamNames = new string[paramArray.Length - 1];
                    for (int i = 0; i < paramArray.Length - 1; i++)
                    {
                        methodParamNames[i] = (string)paramArray[i + 1];
                        
                    }
                    methodVarNames.Add((string)paramArray[0], methodParamNames);
                }
                methodCreation = true;
                
                
            }
            if (strCommand == "methodex")
            {
                if (paramArray.Length == methodParamsReq[currentMethod])
                {
                    methodResume = pointer;
                    pointer = methodNames[currentMethod.ToString()];
                    inMethod = true;
                    methodVars = new Dictionary<string, int>();
                    string[] varNames = methodVarNames.GetValueOrDefault(currentMethod, new string[0]);
                    for (int j = 0; j < methodParamsReq.GetValueOrDefault(currentMethod, 0); j++)
                    {
                        if (int.TryParse((string)paramArray[j], out _)) { methodVars.Add(varNames[j], int.Parse((string)paramArray[j])); }
                        else { methodVars.Add(varNames[j], variableValues[(string)paramArray[j]]); }

                    }

                    methodCreation = false;
                }
                else
                {
                    throw new Exception("Incorrect amount of variables in " + paramArray[0] + ", requires " + methodParamsReq[currentMethod].ToString());
                }
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
            pointer = 0;
            while (pointer < multiCommandArray.Length)
            {
                FullParse(multiCommandArray[pointer]);
            }
            resetAllInternalVars();
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
            checkingSyntax = true;
			for (int i = 0; i < multiCommandArray.Length; i++)
			{
				string[] splitCommand = CommandSplit(multiCommandArray[i]);
				try
				{   

					string strCommand = CommandExtract(splitCommand);
                    if (!methodCreation && strCommand != "methodex")
                    {
                        object[] paramArray = ParamExtract(splitCommand, strCommand);
                        executeCommand(strCommand, paramArray);
                    }
                    else if (strCommand == "endmethod"){ methodCreation = false; }//ignores within method creation since syntax executes program in backround, var errors in method only checkable on execution
                    Clear.ClearMethod(graphicsHandler);
                    
				}
				catch (Exception e)
				{
					Array.Resize(ref errorMessages, errorMessages.Length + 1);
					errorMessages[errorMessages.Length - 1] = ("Line "+ (i+1).ToString() + ": " + e.Message);
				}
			}
            if (insideIf)
            {
                Array.Resize(ref errorMessages, errorMessages.Length + 1);
                errorMessages[errorMessages.Length - 1] = ("Unclosed IF Statement Please Use endif");
            }
            resetAllInternalVars();
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
            resetAllInternalVars();
        }

        /// <summary>
        /// Resets all internal values
        /// </summary>
        /// <returns>void</returns>
        private void resetAllInternalVars()
        {
            variableValues = new Dictionary<string, int>();
            methodNames = new Dictionary<string, int>();
            methodVars = new Dictionary<string, int>();
            methodParamsReq = new Dictionary<string, int>();
            methodVarNames = new Dictionary<string, string[]>();
            insideIf = false;
            ifBool = false;
            pointer = 0;
            whileStart = -1;//this will be used to check to see if while loop exists if not -1
            whileBool = false;
            insideWhile = false;
            whileLogic = new string[] { "", "", "" };
            methodBool = false;
            methodResume = -1;
            methodStartPointers = new Dictionary<string, int>();
            inMethod = false;
            methodCreation = false;
            currentMethod = "";
            settingMethod = "";
            graphicsHandler.SetColour(StringToColour.Convert("black"));
            graphicsHandler.SetFill(false);
            graphicsHandler.pointer.SetPointerXPos(0);
            graphicsHandler.pointer.SetPointerYPos(0);
            checkingSyntax = false;

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

        public Dictionary<string, int> getMethodParamsReq()
        {
            return methodParamsReq;
        }

        public Dictionary<string, int> getMethodNames()
        {
            return methodNames;
        }

        public bool getInMethod()
        {
            return inMethod;
        }

        public bool getMethodBool()
        {
            return methodBool;
        }

        public bool getMethodCreation()
        {
            return methodCreation;
        }

        public Dictionary<string, string[]> getMethodVarNames()
        {
            return methodVarNames;
        }

        public void setCurrentMethod(string myCurrentMethod)
        {
            currentMethod = myCurrentMethod;
        }

        public Dictionary<string, int> getMethodVars() 
        {
            return methodVars;
        }

        public bool getInsideWhile() { return insideWhile; }

        public bool getWhileBool() { return whileBool; }

        public string[] getWhileLogic() { return whileLogic; }

        public int getWhileStart() { return whileStart; }

        public Dictionary<string,int> getVariableValues() { return variableValues; }

        public bool getInsideIf() { return insideIf; }

        public bool getIfBool() { return ifBool; }


    }
}