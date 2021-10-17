using System;
using System.Collections.Generic;

namespace NaturalNumberProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int number;
            Console.Write("Please input a number:");
            number = Convert.ToInt32(Console.ReadLine());

            Console.Write(FindSmallerN(number));

        }


        
        static long FindSmallerN(int N)
        {
            // if 'N' is a single digit number, no simplification required
            if (N >= 0 && N <= 9)
                return N;

            
            Stack<int> digits = new Stack<int>();

            // Divide N by the numbers from 9 to 2 until all the numbers are used or 'N > 1
            for (int i = 9; i >= 2 && N > 1; i--)
            {
                while (N % i == 0)
                {
                    // Pusg the value of i that divides N
                    // onto the stack
                    digits.Push(i);
                    N = N / i;
                }
            }

            // No solution (Q=-1)
            if (N != 1)
                return -1;
            

            // pop digits from the stack and add to Q
            long Q = 0;
            while (digits.Count != 0)
            {
                Q = Q * 10 + digits.Peek();
                digits.Pop();
            }

            
            return Q;
        }
    }
}
