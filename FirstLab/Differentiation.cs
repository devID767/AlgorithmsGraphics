using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_1
{

    public enum DifferentialType
    {
        Left,
        Right,
        Central
    }

    class Differentiation
    {
        public static float DifferentialCalculate_1(Interpolation interpolation, List<float> x_wanted, int i, int n, DifferentialType differentialType)
        {
            float result = 0;
            if (differentialType == DifferentialType.Right)
            {
                result = (interpolation.Lagrang(x_wanted[i + 1], InterpolationType.Piece, n) - interpolation.Lagrang(x_wanted[i], InterpolationType.Piece, n)) / (interpolation.h);
            }
            else if (differentialType == DifferentialType.Left)
            {
                result = (interpolation.Lagrang(x_wanted[i], InterpolationType.Piece, n) - interpolation.Lagrang(x_wanted[i - 1], InterpolationType.Piece, n)) / (interpolation.h);
            }
            else if (differentialType == DifferentialType.Central)
            {
                result = (interpolation.Lagrang(x_wanted[i + 1], InterpolationType.Piece, n) - interpolation.Lagrang(x_wanted[i - 1], InterpolationType.Piece, n)) / (2 * interpolation.h);
            }
            return result;
        }

        public static float DifferentialCalculate_2(Interpolation interpolation, List<float> x_wanted, int i, int n)
        {
            float result = 0;
            float y_, _y_, _y = 0;
            y_ = interpolation.Lagrang(x_wanted[i + 1], InterpolationType.Piece, n);
            _y = interpolation.Lagrang(x_wanted[i - 1], InterpolationType.Piece, n);
            _y_ = interpolation.Lagrang(x_wanted[i], InterpolationType.Piece, n);

            result = (y_ + _y - 2 * _y_) / Convert.ToSingle(Math.Pow(interpolation.h, 2));

            return result;
        }
    }
}
