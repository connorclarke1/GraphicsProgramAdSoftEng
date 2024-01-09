using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    public class CheckLogic
    {
        private static string[] operations = new string[] { "+", "-", "/", "*" };
        


        
        public static bool Check(string logicStr, Dictionary<string, int> variableValues)
        {
            //remove whitespace
            logicStr.Replace(" ", "");
            string[] splitLogic = new string[] {};
            splitLogic = splitAtOperations(logicStr);

            if (splitLogic.Length % 2 == 0 ) { return false; }//if even then not in format of num op num etc
            for (int i = 0; i < splitLogic.Length; i += 2)//odd numbers are vars or ints
            {
                
                checkVar(splitLogic[i]);
                if (!int.TryParse(splitLogic[i], out _))
                {
                    //check variables if not int
                    if (variableValues == null) { return true; } ;//if null fed in as variables then dont worry as syntax checking
                    if (!variableValues.ContainsKey(splitLogic[i])) { throw new Exception("Variable " + splitLogic[i] + " not set"); }
                }
            }
            return true;
        }

        public static string[] splitAtOperations(string logicStr)
        {
            string[] splitLogic = new string[] { };
            string currentStr = new string ("");
            for (int i=0;i < logicStr.Length; i++)
            {
                if (logicStr.Length == i+1) {
                    if (operations.Any(logicStr[i].ToString().Contains)) //if last is operation
                    {
                        Array.Resize(ref splitLogic, splitLogic.Length + 1);
                        splitLogic[splitLogic.Length - 1] = currentStr;
                        Array.Resize(ref splitLogic, splitLogic.Length + 1);
                        splitLogic[splitLogic.Length - 1] = logicStr[i].ToString();
                    }
                    else
                    {
                        currentStr += logicStr[i];
                        Array.Resize(ref splitLogic, splitLogic.Length + 1);
                        splitLogic[splitLogic.Length - 1] = currentStr;
                    }
                }

                //if current string is operation
                if (operations.Any(logicStr[i].ToString().Contains))
                {
                    //if currentStr exists, add to splitLogic
                    Array.Resize(ref splitLogic, splitLogic.Length + 1);
                    splitLogic[splitLogic.Length-1] = currentStr;
                    //add operation as single item in array
                    Array.Resize(ref splitLogic, splitLogic.Length + 1);
                    splitLogic[splitLogic.Length-1] = logicStr[i].ToString();

                    //reset currentStr
                    currentStr = "";

                }
                else
                {
                    //update currentStr
                    currentStr += logicStr[i];

                }
                

            }
            return splitLogic;
        }

        public static bool checkVar(string var)
        {
            if (int.TryParse(var, out _))
            {
                //if integer varaible
                return true;
            }
            else if(var.All(char.IsLetter))
            {
                //if var name only letters
                return true;
            }
            else if (CommandParser.ContainsInteger(var))
            {
                throw new Exception("Error: Variable " + var + " contains integers and letters");
            }
            else
            {
                throw new Exception("Unrecognised Charater in " + var + " | Please only use letters");
            }
        }
    }


}