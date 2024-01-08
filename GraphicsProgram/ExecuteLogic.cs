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
                    //should never default as variables already checked
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
    }
}
