namespace Chua_MExer05
{
    public static class BooleanInfixToPostfixConverter
    {

        private static bool IsOpeningDelimiter(char ch)
        {
            return ch switch
            {
                '(' => true,
                '[' => true,
                '{' => true,
                _ => false,
            };
        }
        private static bool IsClosingDelimiter(char ch)
        {
            return ch switch
            {
                ')' => true,
                ']' => true,
                '}' => true,
                _ => false,
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

        public static int GetPrecedence(string op)
        {
            if(IsRelationalOperators(op)) return 4;
            if (IsEqualityOperators(op)) return 3;
            return op switch
            {
                "!" => 5,
                "&&" => 2,
                "||" => 1,
                _ => throw new InvalidOperationException("Unknown Logical Operator"),
            };

            
        }

        private static int ComparePrecedence(string ch, string peek)
        {
            if (GetPrecedence(peek) >= GetPrecedence(ch)) return 0;
            else return 1;
        }

        public static Queue<string> ToPostfix(string input)
        {
            var stack = new Stack<string>();
            var postfix = new Queue<string>();
            string currentNumber = string.Empty;
            string currentOperator = string.Empty;

            input = input.Replace(" ", string.Empty);
            input = input.Replace("!=", "a");
            input = input.Replace("<=", "b");
            input = input.Replace(">=", "c");
            input = input.Replace("||", "d");


            foreach (char ch in input)
            {
                
                if (!char.IsDigit(ch) && !string.IsNullOrEmpty(currentNumber) && !ch.Equals('.'))
                {
                    postfix.Enqueue(currentNumber);
                    currentNumber = string.Empty;
                }
                if (ch.Equals('.') || char.IsDigit(ch) || ch.Equals('-'))
                {
                    currentNumber += ch;
                }
                else if (IsOpeningDelimiter(ch)) stack.Push(ch.ToString());
                else if (IsClosingDelimiter(ch))
                {
                    string peek = stack.Peek();
                    // Algorithm 1.2.A
                    while (IsLogicalOperand(peek) || IsEqualityOperators(peek) || IsRelationalOperators(peek))
                    {
                        string op = stack.Pop();
                        postfix.Enqueue(op);
                        peek = stack.Peek();
                    }
                    // Algorithm 1.2.B
                    stack.Pop();
                }
                else //if(!char.IsDigit(ch) && !IsClosingDelimiter(ch) && !IsOpeningDelimiter(ch) && !ch.Equals('.') && !ch.Equals('-'))
                {
                    currentOperator += ch;
                    if(currentOperator.Contains("a")) currentOperator = currentOperator.Replace("a", "!=");
                    if(currentOperator.Contains("b")) currentOperator = currentOperator.Replace("b", "<=");
                    if(currentOperator.Contains("c")) currentOperator = currentOperator.Replace("c", ">=");
                    if(currentOperator.Contains("d")) currentOperator = currentOperator.Replace("d", "||");

                    if (IsLogicalOperand(currentOperator) || IsEqualityOperators(currentOperator) || IsRelationalOperators(currentOperator))
                    {
                        if (stack.Count == 0)
                        {
                            stack.Push(currentOperator);
                            currentOperator = string.Empty;
                        }
                        else
                        {
                            string peek = stack.Peek();
                            // Algorithm 1.3.A
                            while (IsLogicalOperand(peek) || IsEqualityOperators(peek) || IsRelationalOperators(peek))
                            {
                                int precedence = ComparePrecedence(currentOperator, peek);
                                if (precedence == 0)
                                {
                                    string op = stack.Pop();
                                    postfix.Enqueue(op);
                                }
                                if (stack.Count == 0 || precedence == 1) break;
                                if (stack.Count == 0) break;
                                peek = stack.Peek();
                            }

                            stack.Push(currentOperator);
                            currentOperator = string.Empty;
                        }
                        
                    }

                }
                
            }
            if (!string.IsNullOrEmpty(currentNumber))
            {
                postfix.Enqueue(currentNumber);
                currentNumber = string.Empty;
            }
            // Algorithm 2
            while (stack.Count > 0)
            {
                postfix.Enqueue(stack.Pop());
            }
            return postfix;

        }

    }
}
