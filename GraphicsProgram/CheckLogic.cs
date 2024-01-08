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
        


        //checks the logical portion of an operation is valid
        //for example for x = x + 5, x+5 is the logical portion and is valid
        //must be in the form ([var or int][operation])* [var or int]
        //leave variables as null unless being executed
        public static bool Check(string logicStr, Dictionary<string, int> variableValues)
        {
            //remove whitespace
            logicStr.Replace(" ", "");
            //TODO seperate by operations
            string[] splitLogic = new string[] {};


            splitLogic = splitAtOperations(logicStr);

            //check either existing variables or integers
            if (splitLogic.Length % 2 == 0 ) { return false; }//if even then not in format
            for (int i = 0; i < splitLogic.Length; i += 2)//odd numbers are vars or ints
            {
                if (!int.TryParse(splitLogic[i], out _))
                {
                    //if the variable is not an integer check variables
                    //variables will be in dictionary
                    if (variableValues == null) { return true; } ;//if null fed in as variables then dont worry
                    if (!variableValues.ContainsKey(splitLogic[i])) { throw new Exception("Variable " + splitLogic[i] + " not set"); }
                }
            }
            return true;
        }

        public static string[] splitAtOperations(string logicStr)
        {
            //string[] operations = new string[] { "+", "-", "/", "*" };
            string[] splitLogic = new string[] { };
            string currentStr = new string ("");
            for (int i=0;i < logicStr.Length; i++)
            {
                //if last char
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
                }//adds last one on

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
    }
}