using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chua_MExer05
{
    public static class BooleanPostfixEvaluator
    {
        public static bool Evaluate(Queue<string> postfix)
        {
            var stack = new Stack<float>();
            var boolstack = new Stack<bool>();
            float x = 0f;
            float y = 0f;
            bool xb = default;
            bool yb = default;
            bool result = default;
            foreach (string item in postfix)
            {
                //from algo 1.i.
                if (IsNumber(item))
                {
                    float number = Parse(item);
                    stack.Push(number);
                }
                else if (IsLogicalOperand(item))
                {
                    xb = boolstack.Pop();
                    if(item != "!") yb = boolstack.Pop();
                    result = ValueOfLogicalOperand(xb, yb, item);
                    boolstack.Push(result);
                }
                else if (IsRelationalOperators(item))
                {
                    x = stack.Pop();
                    y = stack.Pop();
                    result = ValueOfRelationalOperators(x, y, item);
                    boolstack.Push(result);
                }
                else if(IsEqualityOperators(item))
                {
                    x = stack.Pop();
                    y = stack.Pop();
                    result = ValueOfEqualityOperator(x, y, item);
                    boolstack.Push(result);
                }
            }

            result = boolstack.Pop();
            return result;

        }


        private static bool ValueOfLogicalOperand(bool x, bool y, string op)
        {
            return op switch
            {
                "&&" => y && x,
                "||" => y || x,
                "!" => !x,
                _ => throw new InvalidOperationException("Unknown Operator"),
            };


        }


        private static bool ValueOfRelationalOperators(float x, float y, string op)
        {
            return op switch
            {
                "<" => y < x,
                ">" => y > x,
                "<=" => y <= x,
                ">=" => y >= x,
                _ => throw new InvalidOperationException("Unknown Operator"),
            };
        }


        private static bool ValueOfEqualityOperator(float x, float y, string op)
        {

            return op switch
            {
                "==" => y == x,
                "!=" => y != x,
                _ => throw new InvalidOperationException("Unknown Operator"),
            };



        }


        private static bool IsLogicalOperand(string str)
        {
            return str switch
            {
                "&&" => true,
                "||" => true,
                "!" => true,
                _ => false,
            };
        }

        private static bool IsRelationalOperators(string str)
        {
            return str switch
            {
                "<" => true,
                ">" => true,
                "<=" => true,
                ">=" => true,
                _ => false,
            };
        }

        private static bool IsEqualityOperators(string str)
        {

            return str switch
            {
                "==" => true,
                "!=" => true,
                _ => false,
            };
        }

        private static bool IsNumber(string number)
        {
            float result = 0;
            bool isNUmber = float.TryParse(number, out result);
            return isNUmber;
        }

        private static float Parse(string number)
        {
            float result = 0;
            bool isNumber = float.TryParse(number, out result);
            return result;
        }
    }
}
