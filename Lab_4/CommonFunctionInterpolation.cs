using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_1
{
    public static class CommonFunctionInterpolation
    {
        public static List<float> missings = new List<float>();

        public static double Y(double x)
        {
            return Math.Sin(x + 2) - 1.5;
        }
        public static double X(double y)
        {
            return 0.5 - Math.Cos(y - 2);
        }

        public static float[] VectorMinus(float[] vector1, float[] vector2)
        {
            float[] result_vector = new float[vector1.Length];
            for (int i = 0; i < vector1.Length; i++)
            {
                result_vector[i] = Math.Abs(vector1[i] - vector2[i]);
            }
            return result_vector;
        }

        public static float Norma(float[] vector)
        {
            float result = vector[0];
            foreach (var item in vector)
            {
                if (item > result)
                    result = item;
            }
            return result;
        }
    }
}
