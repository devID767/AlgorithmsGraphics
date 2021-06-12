using System;
using System.Collections.Generic;

namespace Algorithms_1
{
    public static class Iteration
    {
        private static float gx(float x)
        {
            return Convert.ToSingle(Math.Sin(x + 2) - 1.5f);
        }

        private static float gy(float y)
        {
            return Convert.ToSingle(0.5f - Math.Cos(y - 2));
        }

        public static float[] SimpleIteration(float startIterationX, float startIterationY, float accuracy = 0.0001f)
        {
            float x = startIterationX;
            float xy;
            float y = startIterationY;

            float[] x_vector = new float[2] { x, y };
            float[] x_vectorOld = new float[2];
            float missing;

            int k = 1;
            do
            {
                x_vectorOld[0] = x;
                x_vectorOld[1] = y;

                xy = x;
                x = gy(y);
                y = gx(xy);

                x_vector[0] = x;
                x_vector[1] = y;

                missing = CommonFunctionInterpolation.Norma(CommonFunctionInterpolation.VectorMinus(x_vector, x_vectorOld));
                CommonFunctionInterpolation.missings.Add(missing);
                Console.WriteLine($"k = {k}\t x = {x}\t y = {y}\t missing = {missing}");
                if (missing < accuracy)
                {
                    break;
                }
                else
                {
                    k++;
                }
            } while (k <= 10000);
            if (k == 10000)
            {
                throw new InvalidOperationException("Ітераційний процесс незбіжний");
            }
            return x_vector;
        }
        public static float[] ZeydelIteration(float startIterationX, float startIterationY, float accuracy = 0.0001f)
        {
            float x = startIterationX;
            float y = startIterationY;

            float[] x_vector = new float[2] { x, y };
            float[] x_vectorOld = new float[2];
            float missing;


            x_vector[0] = x;
            x_vector[1] = y;

            int k = 1;
            do
            {
                x_vectorOld[0] = x;
                x_vectorOld[1] = y;

                x = gy(y);
                y = gx(x);

                x_vector[0] = x;
                x_vector[1] = y;

                missing = CommonFunctionInterpolation.Norma(CommonFunctionInterpolation.VectorMinus(x_vector, x_vectorOld));
                CommonFunctionInterpolation.missings.Add(missing);
                Console.WriteLine($"k = {k}\t x = {x}\t y = {y}\t missing = {missing}");
                if (missing < accuracy)
                {
                    break;
                }
                else
                {
                    k++;
                }
            } while (k <= 10000);
            if (k == 10000)
            {
                throw new InvalidOperationException("Ітераційний процесс незбіжний");
            }
            return x_vector;
        }
    }
}
