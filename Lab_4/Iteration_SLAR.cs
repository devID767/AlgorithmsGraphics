using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_1
{
    class Iteration_SLAR
    {
        private static float X1(float x2, float x3)
        {
            return (19 - 27 * x2 + 17 * x3) / 56;
        }

        private static float X2(float x1, float x3)
        {
            return (31 - 14 * x1 - 13 * x3) / 50;
        }

        private static float X3(float x1, float x2)
        {
            return (12 - 8 * x1 - 13 * x2) / 37;
        }

        public static float[] SimpleIteration_SLAR(float x, float accuracy = 0.0001f)
        {
            float x1 = x;
            float x2 = x;
            float x3 = x;

            float X_1;
            float X_2;
            float X_3;

            float missing;
            float[] resultOld = new float[3];
            float[] result = new float[3];

            result[0] = x1;
            result[1] = x2;
            result[2] = x3;

            int k = 1;
            while (k <= 1000)
            {
                resultOld[0] = x1;
                resultOld[1] = x2;
                resultOld[2] = x3;

                X_1 = X1(x2, x3);
                X_2 = X2(x1, x3);
                X_3 = X3(x1, x2);

                x1 = X_1;
                x2 = X_2;
                x3 = X_3;

                result[0] = x1;
                result[1] = x2;
                result[2] = x3;

                missing = CommonFunctionInterpolation.Norma(CommonFunctionInterpolation.VectorMinus(result, resultOld));
                CommonFunctionInterpolation.missings.Add(missing);
                Console.WriteLine($"k = {k}\t x1 = {x1}\t x2 = {x2}\t x3 = {x3}\t missing {missing}");
                if (missing < accuracy)
                {
                    break;
                }
                else
                {
                    k++;
                }
            }
            if (k == 1000)
                throw new InvalidOperationException("інтераційний процесс є незбіжним");
            return result;
        }

        public static float[] ZeydalIteration_SLAR(float x, float accuracy = 0.0001f)
        {
            float x1 = x;
            float x2 = x;
            float x3 = x;

            float missing;
            float[] resultOld = new float[3];
            float[] result = new float[3];

            result[0] = x1;
            result[1] = x2;
            result[2] = x3;

            int k = 1;
            while (k <= 1000)
            {
                resultOld[0] = x1;
                resultOld[1] = x2;
                resultOld[2] = x3;

                x1 = X1(x2, x3);
                x2 = X2(x1, x3);
                x3 = X3(x1, x2);

                result[0] = x1;
                result[1] = x2;
                result[2] = x3;

                missing = CommonFunctionInterpolation.Norma(CommonFunctionInterpolation.VectorMinus(result, resultOld));
                CommonFunctionInterpolation.missings.Add(missing);
                Console.WriteLine($"k = {k}\t x1 = {x1}\t x2 = {x2}\t x3 = {x3}\t missing {missing}");
                if (missing < accuracy)
                {
                    break;
                }
                else
                {
                    k++;
                }
            }
            if (k == 1000)
                throw new InvalidOperationException("інтераційний процесс є незбіжним");
            return result;
        }
    }
}
