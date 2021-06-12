using System;
using System.Collections.Generic;

 namespace Algorithms_1
{
    class Lab_3
    {
        public List<float> missingBisection = new List<float>();
        public List<float> missingNuton = new List<float>();

        float MyFunction_2(float x)
        {
            double x1 = -1 / (4 * x * Math.Pow(x, 0.5));
            double x2 = -2 / (9 * x * Math.Pow(x, 2 / 3));
            double x3 = -3 / (16 * Math.Pow(x, 3 / 4) * Math.Abs(x));
            return Convert.ToSingle(x1 + x2 + x3);
        }

        float MyFunction_1(float x)
        {
            double x1 = 1 / (2 * Math.Pow(x, 0.5));
            double x2 = 1 / (3 * Math.Pow(x, 0.666667));
            double x3 = 1 / (4 * Math.Pow(x, 0.75));
            return Convert.ToSingle(1 + x1 + x2 + x3);
        }

        public float MyFunction(float x)
        {
            return Convert.ToSingle(x + Math.Pow(x, 0.5) + Math.Pow(x, 0.333333333) + Math.Pow(x, 0.25) - 5);
        }

        public float Nuton(float a, float b, float accuracy = 0.0001f)
        {
            float x = 0;
            if (MyFunction(a) * MyFunction_2(a) > 0)
            {
                x = a;
            }
            else
            {
                x = b;
            }

            float x0 = 0;
            int k = 0;
            float missing = 0;
            do
            {
                x0 = x;
                x = x0 - (MyFunction(x0) / MyFunction_1(x0));
                missing = Math.Abs(x - x0);
                missingNuton.Add((float)Math.Log10(missing));
                Console.WriteLine($"k = {k}\t x = {x}\t |x-x0| = {missing}\t");
                if (MyFunction(x) == 0)
                {
                    break;
                }
                k++;

            } while (missing > accuracy);
            return x;
        }

        public float Bisection(float a, float b, float accuracy = 0.0001f)
        {
            int k = 0;
            float x = 0;
            float missing = 0;
            do
            {
                missing = Math.Abs(b - a);
                missingBisection.Add((float)Math.Log10(missing));
                x = (a + b) / 2;
                float function = MyFunction(x);
                Console.WriteLine($"k = {k}\t a = {a}\t b = {b}\t x = {x}\t |b-a| = {missing}\t fk = {function}");
                if (function == 0)
                {
                    break;
                }
                if (MyFunction(a) * MyFunction(x) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
                k++;
            } while (missing > accuracy);
            return x;
        }
    }
}
