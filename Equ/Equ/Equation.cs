using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Equ
{
    class Equation
    {
        List<string> Left = new List<string>();
        List<string> Right = new List<string>();
        string equLeft = "";
        string equRight = "";
        string wholeEquation = "";
        double d, calculateXvalue = 0;
        string tempValue = "";

        public Equation(string[] args)
        {
            try
            {
                if (args[0] != "equ")
                {
                    Console.WriteLine("The first word should be 'equ'.");
                }
                else
                {

                    LeftSide(args);
                    RightSide(args);

                    //make "wholeEquation" not over flow 
                    wholeEquation = equLeft + equRight + ' ';
                    if (!wholeEquation.Contains('X'))
                    {
                        Console.WriteLine("Please check your Input");
                    }
                    else
                    {
                        calculate(wholeEquation);
                       // printout whole equation
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Please check your input and the equation should be the example 'ax + b = 0'", e.Message);
            }
        }

        //The left side equation will be simplified like "ax" 
        
        private void LeftSide(string[] @equation)
        {
            //make int Length = equation.Length;

            for (int i = 1; i < equation.Length; i++)
            {
                // calc the first x and number
                if (equation[i].Contains("X") && (i == 1) && (equation[i + 1] != "*" || equation[i + 1] != "/"))
                    // the first include X and operator is not * or /
                {
                    Left.Add(equation[i]);
                    operatorControlLeft(equation[i + 1]);
                }
                else if (IsNumeric(equation[i]) && (i == 1) && (equation[i + 1] != "*" || equation[i + 1] != "/"))
                    // the first is number and operator is not * or /
                {
                    Left.Add(equation[i]);
                    operatorControlLeft(equation[i + 1]);
                }

                // calc minus and plus
                if (equation[i] == "+" || equation[i] == "-")
                {
                    // calc the X

                    if ((equation[i + 1].Contains("X") || IsNumeric(equation[i + 1])) && (equation[i + 1] != "*" || equation[i + 1] != "/"))
                    {
                        Left.Add(equation[i + 1]);
                        operatorControlLeft(equation[i + 2]);
                    }
                }
                // break point of left side
                if (equation[i] == "=")
                {
                    Left.Add(equation[i]);
                    break;
                }
                
                // calc mutliple 
                if (equation[i] == "*")
                
                {
                    //When element in the right side of "*" is X
                    if (equation[i + 1].Contains("X"))
                    {
                        //if the element in the left side of "*" is Number
                        if (IsNumeric(equation[i - 1]))
                        {
                            
                            if (equation[i + 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i - 1]) * GetNumberDouble(equation[i + 1]);
                                equation[i + 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Left.Remove(equation[i]);
                            Left.Add(equation[i + 1]);
                            Left.Remove(equation[i - 1]);
                            operatorControlLeft(equation[i + 2]);

                            // Console.WriteLine("test!:equation[i+1] is {0}", equation[i + 1]);

                        }
                        

                    }
                    // when elements in both left and right side is number
                    else if (IsNumeric(equation[i - 1]) && IsNumeric(equation[i + 1]))
                    {
                        d = Convert.ToDouble(equation[i - 1]) * Convert.ToDouble(equation[i + 1]);
                        equation[i + 1] = Convert.ToString(d);
                        //Console.WriteLine($"the value of d is {d}");
                        tempValue = Convert.ToString(d);

                        if (d != 0)
                        {
                            Left.Add(Convert.ToString(d));

                        }
                        Left.Remove(equation[i]);
                        Left.Remove(equation[i - 1]);

                        operatorControlLeft(equation[i + 2]);

                    }
                    //when element in left side of "*" is x
                    else if (equation[i - 1].Contains("X"))
                    {

                        //if right side of "*" is number
                        if (IsNumeric(equation[i + 1]))
                        {
                            
                            if (equation[i - 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i + 1]) * GetNumberDouble(equation[i - 1]);
                                equation[i + 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Left.Add(equation[i + 1]);
                            Left.Remove(equation[i - 1]);
                            Left.Remove(equation[i]);
                            operatorControlLeft(equation[i + 2]);

                        }
                    }

                }
                

                else if (equation[i] == "/")
                
                {
                    //When element in the right side of "/" is x
                    if (equation[i + 1].Contains("X"))
                    {
                        //if the element in the left side of "/" is Number
                        if (IsNumeric(equation[i - 1]))
                        {
                            
                            if (equation[i + 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i - 1]) / GetNumberDouble(equation[i + 1]);
                                equation[i + 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Left.Remove(equation[i]);
                            Left.Add(equation[i + 1]);
                            Left.Remove(equation[i - 1]);
                            operatorControlLeft(equation[i + 2]);

                            //Console.WriteLine("test!:equation[i+1] is {0}", equation[i + 1]);

                        }
                    }
                    // When both left and right side of "/" are number
                    else if (IsNumeric(equation[i - 1]) && IsNumeric(equation[i + 1]))
                    {
                        if (Convert.ToDouble(equation[i + 1]) != 0)
                        {
                            d = Convert.ToDouble(equation[i - 1]) / Convert.ToDouble(equation[i + 1]);
                            equation[i + 1] = Convert.ToString(d);
                            //Console.WriteLine($"the value of d is {d}");
                            tempValue = Convert.ToString(d);
                        }
                        else
                        {
                            Console.WriteLine("Error! Zero can not be divided!");
                            break;
                        }


                        if (d != 0)
                        {
                            Left.Add(Convert.ToString(d));

                        }
                        Left.Remove(equation[i]);
                        Left.Remove(equation[i - 1]);

                        operatorControlLeft(equation[i + 2]);

                    }
                    //when element in left side of "/" is x
                    else if (equation[i - 1].Contains("X"))
                    {
                        //if right side of "/" is number
                        if (IsNumeric(equation[i + 1]))
                        {
                           
                            if (equation[i - 1].Contains("X"))
                            {
                                calculateXvalue = GetNumberDouble(equation[i - 1]) / Convert.ToDouble(equation[i + 1]);
                                equation[i + 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Left.Add(equation[i + 1]);
                            Left.Remove(equation[i - 1]);
                            Left.Remove(equation[i]);
                            operatorControlLeft(equation[i + 2]);

                        }

                    }

                }
                
            }

            foreach (string element in Left)
            {

                equLeft += element;


            }
            //Console.WriteLine("left side is {0}", equLeft);
        }

        //The right side equation will be simplified as "ax + b "
       
        private void RightSide(string[] @equation)
        {

            for (int i = equation.Length - 1; i > 0; i--)
            {
                //
                if (equation[i] == "=")
                {
                    break;
                }
                else
                // calc the first x and number
                if ((equation[i].Contains("X")) && (i == (equation.Length - 1)) && (equation[i - 1] != "*" || equation[i - 1] != "/"))
                {
                    Right.Add(equation[i]);
                    operatorControlRight(equation[i - 1]);
                }
                else if (IsNumeric(equation[i]) && (i == (equation.Length - 1)) && (equation[i - 1] != "*" || equation[i - 1] != "/"))
                {
                    Right.Add(equation[i]);
                    operatorControlRight(equation[i - 1]);
                }
                // calc minus and plus
                if (equation[i] == "+" || equation[i] == "-")
                {
                    //calc the X

                    if ((equation[i - 1].Contains("X")|| IsNumeric(equation[i - 1])) && (equation[i - 1] != "*" || equation[i - 1] != "/"))
                    {
                        Right.Add(equation[i - 1]);
                        operatorControlRight(equation[i - 2]);
                    }
                }

                //  calculation *
                if (equation[i] == "*")
                
                {
                    //When element in the right side of "*" is x
                    if (equation[i + 1].Contains("X"))
                    {
                        //if the element in the left side of "*" is Number
                        if (IsNumeric(equation[i - 1]))
                        {
                           
                            if (equation[i + 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i - 1]) * GetNumberDouble(equation[i + 1]);
                                equation[i - 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Right.Remove(equation[i]);
                            Right.Add(equation[i - 1]);
                            Right.Remove(equation[i + 1]);
                            operatorControlRight(equation[i - 2]);

                            // Console.WriteLine("test!:equation[i+1] is {0}", equation[i + 1]);

                        }
                        
                    }
                    // when elements in both left and right side is number
                    else if (IsNumeric(equation[i - 1]) && IsNumeric(equation[i + 1]))
                    {
                        // Console.WriteLine("TEST");
                        d = Convert.ToDouble(equation[i - 1]) * Convert.ToDouble(equation[i + 1]);
                        equation[i - 1] = Convert.ToString(d);
                        //Console.WriteLine($"the value of d is {d}");
                        tempValue = Convert.ToString(d);

                        if (d != 0)
                        {
                            Right.Add(Convert.ToString(d));
                        }
                        Right.Remove(equation[i]);
                        Right.Remove(equation[i + 1]);
                        operatorControlRight(equation[i - 2]);

                    }
                    //when element in left side of "*" is x
                    else if (equation[i - 1].Contains("X"))
                    {
                        //if right side of "*" is number
                        if (IsNumeric(equation[i + 1]))
                        {
                            
                            if (equation[i - 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i + 1]) * GetNumberDouble(equation[i - 1]);
                                equation[i - 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Right.Add(equation[i - 1]);
                            Right.Remove(equation[i + 1]);
                            Right.Remove(equation[i]);
                            operatorControlRight(equation[i - 2]);

                        }
                    }
                }
                

                else if (equation[i] == "/")
                {
                    //When element in the right side of "/" is x
                    if (equation[i + 1].Contains("X"))
                    {
                        //if the element in the left side of "/" is Number
                        if (IsNumeric(equation[i - 1]))
                        {
                            
                            if (equation[i + 1].Contains("X"))
                            {
                                calculateXvalue = Convert.ToDouble(equation[i - 1]) / GetNumberDouble(equation[i + 1]);
                                equation[i - 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Right.Remove(equation[i]);
                            Right.Add(equation[i - 1]);
                            Right.Remove(equation[i + 1]);
                            operatorControlRight(equation[i - 2]);
                            //Console.WriteLine("test!:equation[i+1] is {0}", equation[i + 1]);
                        }
                        //if the element in the left side of "/" is x
                        else if (equation[i - 1].Contains("X"))
                        {
                            calculateXvalue = GetNumberDouble(equation[i - 1]) / GetNumberDouble(equation[i + 1]);
                            // if left of "/" is "X"
                            if (equation[i - 1].Contains("X"))
                            {
                                if (equation[i - 1].Contains("X"))
                                {
                                    equation[i - 1] = Convert.ToString(calculateXvalue);
                                }
                                
                            }                           
                            Right.Add(equation[i - 1]);
                            Right.Remove(equation[i + 1]);
                            Right.Remove(equation[i]);
                            operatorControlRight(equation[i - 2]);
                        }

                    }
                    // When both left and right side of "/" are number
                    else if (IsNumeric(equation[i - 1]) && IsNumeric(equation[i + 1]))
                    {
                        if (Convert.ToDouble(equation[i + 1]) != 0)
                        {
                            d = Convert.ToDouble(equation[i - 1]) / Convert.ToDouble(equation[i + 1]);
                            equation[i - 1] = Convert.ToString(d);
                            //Console.WriteLine($"the value of d is {d}");
                            tempValue = Convert.ToString(d);
                        }
                        else
                        {
                            Console.WriteLine("Zero can not be the divided.");
                            break;
                        }


                        if (d != 0)
                        {
                            Right.Add(Convert.ToString(d));

                        }
                        Right.Remove(equation[i]);
                        Right.Remove(equation[i + 1]);

                        operatorControlRight(equation[i - 2]);

                    }

                    //when element in left side of "/" is x
                    else if (equation[i - 1].Contains("X"))
                    {
                        //if right side of "/" is number
                        if (IsNumeric(equation[i + 1]))
                        {
                            
                            if ( equation[i - 1].Contains("X"))
                            {
                                calculateXvalue = GetNumberDouble(equation[i - 1]) / Convert.ToDouble(equation[i + 1]);
                                equation[i - 1] = Convert.ToString(calculateXvalue) + "X";

                            }
                            Right.Add(equation[i - 1]);
                            Right.Remove(equation[i + 1]);
                            Right.Remove(equation[i]);
                            operatorControlRight(equation[i - 2]);

                        }

                    }
                }

            }
            // reorder the sequence of ListeRight
            Right.Reverse();
            foreach (string element in Right)
            {
                equRight += element;
            }
            // Console.WriteLine($"Right equation {equRight}");

        }

        // check the string is a number or not
        private bool IsNumeric(string str)
        {
            string temp = @"^[+-]?\d*(,\d{3})*(\.\d+)?$";
            Regex abc = new Regex(temp);
            if (abc.IsMatch(str))
            {
                return true;
            }
            else
            {
                // Console.WriteLine("It is not a Number!");
                return false;
            }
        }

        // pick the numbers in front of X
        private double GetNumberDouble(string str)
        {

            string result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != 'X')
                {
                    result += str[i];
                }
                else
                {
                    break;
                }
            }
            //if the front number of X is empty, it will return 1.
            if (result.Length == 0)
            {
                return 1;
            }
            else
            {
                return double.Parse(result);
            }
        }

        // check the string is an operator or not
        private bool checkOperator(string str)
        {
            if (str == "+")
            {
                return true;
            }
            else if (str == "-")
            {
                return true;
            }
            else if (str == "*")
            {
                return true;
            }
            else if (str == "/")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        // calculat X
        private void calculate(string equation)
        {
            //The equation will simplify as "ax+b=cx+d" 
            //variable "sign" will control the positive or negative
            // n is the length of the equation you give
            // j is a temporary position of the simplified equation
            int n = equation.Length, sign = 1, j = 0;
            double a = 0, b = 0, c = 0, x1, x2, delta;
            //Console.WriteLine($"Length of this equation is {n}");

            try
            {
                for (int i = 0; i < n; i++)
                {
                    if (equation[i] == '+' || equation[i] == '-')
                    {
                        if (i > j)
                        {
                            c += double.Parse(equation.Substring(j, i - j)) * sign;

                        }
                        j = i;
                    }
                    else if (equation[i] == 'X' && (i + 1 < n))
                    {
                        if (i == j || equation[i - 1] == '+')
                        {
                            b += sign;
                        }
                        else if (equation[i - 1] == '-')
                        {
                            b -= sign;
                        }
                        else
                        {
                            b += double.Parse(equation.Substring(j, i - j)) * sign;
                        }
                        j = i + 1;
                    }
                    else if (equation[i] == 'X' && (i + 1 < n))
                    {
                        if (i == j || equation[i - 1] == '+')
                        {
                            a += sign;
                        }
                        else if (equation[i - 1] == '-')
                        {
                            a -= sign;
                        }
                        else
                        {
                            a += double.Parse(equation.Substring(j, i - j)) * sign;
                        }
                        j = i + 2;
                    }
                    else if (equation[i] == '=')
                    {
                        //Console.WriteLine($"i={i}");
                        if (i > j) c += double.Parse(equation.Substring(j, i - j)) * sign;
                        //Console.WriteLine($"j={j}, capture={i - j},b={b}");
                        sign = -1;
                        j = i + 1;
                        //
                    }
                }
                if (j < n - 1)
                {
                    // Console.WriteLine("N is {0}",n);
                    c += double.Parse(equation.Substring(j)) * sign;

                }
                // if the equation is like "ax + b = 0"
                if (a == 0)
                {
                    if (b == 0 && b == c)
                    {
                        Console.WriteLine("Infinite Solution!");
                    }
                    else if (b == 0 && b != c)
                    {
                        Console.WriteLine("No solution!");
                    }
                    else
                    {
                        double result = (-c / b);
                        Console.WriteLine($"x = {result}");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input!", e.Message);
            }

        }
        //deal with the operator situation in list
        private void operatorControlLeft(string str)
        {
            if (checkOperator(str))
            {
                Left.Add(str);

            }
        }
        //deal with the operator situation in list
        private void operatorControlRight(string str)
        {
            if (checkOperator(str))
            {
                Right.Add(str);

            }
        }
    }
}
