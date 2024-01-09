using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProgram
{
    public class ExecuteLogic
    {
        /// <summary>
        /// Executes Mathematical Statement
        /// Takes Dictionary of Variables
        /// </summary>
        /// <param name="logicStr">Mathematics to be executed</param>
        /// <param name="variableValues">Dictionary of variables, null if not checking variables exist</param>
        /// <returns>int result</returns>
        public static int Execute(string logicStr, Dictionary<string, int> variableValues)
        {
            string[] splitLogic = CheckLogic.splitAtOperations(logicStr);
            int[] splitLogicInts = new int[splitLogic.Length];
            CheckLogic.Check(logicStr, variableValues);//check variables exist
            //convert all values to int, if int then int, if variable then into int
            for (int i = 0; i < splitLogic.Length; i += 2)
            {       
                try
                {
                    splitLogicInts[i] = int.Parse(splitLogic[i]);
                }
                catch (Exception e)
                //if its a variable name, checked already in check logic
                {
                    splitLogicInts[i] = variableValues.GetValueOrDefault(splitLogic[i],0);
                    //will not default as variables already checked
                }
            }
            int currentValue = splitLogicInts[0];
            for (int i = 1; i < splitLogic.Length; i += 2)
            {
                if (splitLogic[i] == "+") { currentValue = currentValue + splitLogicInts[i+1]; }
                if (splitLogic[i] == "-") { currentValue = currentValue - splitLogicInts[i + 1]; }
                if (splitLogic[i] == "/") { currentValue = currentValue / splitLogicInts[i + 1]; }
                if (splitLogic[i] == "*") { currentValue = currentValue * splitLogicInts[i + 1]; }
            }
            return currentValue;
        }

        /// <summary>
        /// Executes operation on two ints with operator string
        /// </summary>
        /// <param name="a">int value on the left of operator</param>
        /// <param name="b">int value on the right of operator</param>
        /// <param name="operatorStr">Operator to be executed e.g. >= </param>
        /// <returns>bool result</returns>
        public static bool executeInequality(int a, int b, string operatorStr)
        {
            
            if (operatorStr == ">") { return (a > b); }
            if (operatorStr == ">=") { return (a >= b); }
            if (operatorStr == "==") { return (a == b); }
            if (operatorStr == "<=") { return (a <= b); }
            if (operatorStr == "<") { return (a < b); }
            else { return false; }
        }
    }
}
