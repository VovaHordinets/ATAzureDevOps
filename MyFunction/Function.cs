using System;

namespace MyFunction
{
    public class Function
    {
        //(3) f(x) = sqrt(x-7) 
        public double MyFunctionResult(double x)
        {
            if (x < 0)
            {
                throw new ArithmeticException();
            }
            else
                return Math.Sqrt(x-7);
        }
    }
}
