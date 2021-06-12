using System;
using System.Collections.Generic;

namespace Algorithms_1
{
    public static class MyFunction
    {
        public static float GetValue(float x)
        {
            return Convert.ToSingle(Math.Log(Math.Pow(x, 2) + x + 1));

        }

        public static float GetFirstDerive(float x)
        {
            return Convert.ToSingle(((2 * x + 1) / (Math.Pow(x, 2) + x + 1)));
        }

        public static float GetSecondDerive(float x)
        {
            return Convert.ToSingle((-2 * Math.Pow(x, 2) - 2 * x + 1) / (Math.Pow(Math.Pow(x, 2) + x + 1, 2)));
        }
    }

    class Lab1
    {
        public static int type;

        public static List<float> x_array = new List<float>();

        public static List<float> y_arrayDefault = new List<float>();
        public static List<float> y_arrayLagrangePiece = new List<float>();
        public static List<float> y_arrayLagrangeGlobal = new List<float>();

        public static List<float> y_arrayDerive1 = new List<float>();
        public static List<float> y_arrayDerive2 = new List<float>();
        public static List<float> y_arrayAproxi1 = new List<float>();
        public static List<float> y_arrayAproxi2 = new List<float>();

        public static void SecondPart()
        {
            type = 2;

            var x_table = new List<float>() { 0.5f, 0.69f, 0.78f, 0.990f, 1.21f, 1.34f, 1.51f, 1.63f, 1.71f, 1.83f };
            var y_table = new List<float>() { 0.55962f, 0.77293f, 0.87062f, 1.0886f, 1.30313f, 1.41963f, 1.566561f, 1.66523f, 1.72884f, 1.82114f };

            var interpolation = new Interpolation(x_table, y_table);
            interpolation.SetXValues(0, 1.5f, 0.01f);

            var y_wanted = new List<float>();
            var x_wanted = new List<float>();
            while (interpolation.a <= interpolation.b)
            {
                x_wanted.Add(interpolation.a);
                interpolation.a += interpolation.h;
            }

            int n = 1;

            Console.WriteLine("               y'(x)            похідна 1         апроксимація    |     похідна 2             апроксимація");
            Console.WriteLine("          ________________________________________________________|___________________________________________");
            Console.WriteLine("                                                                  |                                          ");
            for (int i = 0; i < x_wanted.Count; i++)
            {
                float derive_1, derive_2, aproxi_1, aproxi_2 = 0;

                derive_1 = MyFunction.GetFirstDerive(x_wanted[i]);
                if (i == 0)                       aproxi_1 = Differentiation.DifferentialCalculate_1(interpolation, x_wanted, i, n, DifferentialType.Right);
                else if (i == x_wanted.Count - 1) aproxi_1 = Differentiation.DifferentialCalculate_1(interpolation, x_wanted, i, n, DifferentialType.Left);
                else                              aproxi_1 = Differentiation.DifferentialCalculate_1(interpolation, x_wanted, i, n, DifferentialType.Central);

                if (i != 0 && i != x_wanted.Count - 1)
                {
                    aproxi_2 = Differentiation.DifferentialCalculate_2(interpolation, x_wanted, i, n);
                }
                derive_2 = MyFunction.GetSecondDerive(x_wanted[i]);


                string result;
                result = $"               y'({ShowX(x_wanted[i])}) =       {ShowFunction(derive_1)}        {ShowFunction(derive_1)} ";
                result += $"   |    {ShowFunction(derive_2)}             {aproxi_2}";
                Console.WriteLine(result);
                Console.WriteLine("          ________________________________________________________|___________________________________________");
                Console.WriteLine("                                                                  |                                          ");

                x_array.Add(x_wanted[i]);
                y_arrayDerive1.Add(derive_1);
                y_arrayDerive2.Add(derive_2);
                y_arrayAproxi1.Add(aproxi_1);
                y_arrayAproxi2.Add(aproxi_2);
            }
        }

        public static void FirstPart()
        {
            type = 1;

            var x_table = new List<float>() { 0.5f, 0.69f, 0.78f, 0.990f, 1.21f, 1.34f, 1.51f, 1.63f, 1.71f, 1.83f };
            var y_table = new List<float>() { 0.55962f, 0.77293f, 0.87062f, 1.0886f, 1.30313f, 1.41963f, 1.566561f, 1.66523f, 1.72884f, 1.82114f };

            var interpolation = new Interpolation(x_table, y_table);
            interpolation.SetXValues();

            var x_wanted = new List<float>();
            while (interpolation.a <= interpolation.b)
            {
                x_wanted.Add(interpolation.a);
                interpolation.a += interpolation.h;
            }

            int n = 1;

            Console.WriteLine($" x                y               Кускова інтерполяція {n} степіню         Глобальна інтерполяція");
            Console.WriteLine();

            float y_default, y_LagrangePiece, y_LagrangeGlobal = 0;
            foreach (float x in x_wanted)
            {
                y_default = MyFunction.GetValue(x);
                y_LagrangePiece = interpolation.Lagrang(x, InterpolationType.Piece, n);
                y_LagrangeGlobal = interpolation.Lagrang(x, InterpolationType.Global);

                Console.WriteLine($"{ShowX(x)}          {ShowFunction(y_default)}                     {ShowFunction(y_LagrangePiece)}                       {ShowFunction(y_LagrangeGlobal)}");
                Console.WriteLine("____________________________________________________________________________________________________ \n");

                x_array.Add(x);
                y_arrayDefault.Add(y_default);
                y_arrayLagrangePiece.Add(y_LagrangePiece);
                y_arrayLagrangeGlobal.Add(y_LagrangeGlobal);
            }
        }

        public static string ShowX(float x)
        {
            string result = String.Empty;
            result = Math.Round(x, 2).ToString();
            if (result == "1") result += "   ";
            else if (result == "1,1") result += " ";
            else if (result == "1,2") result += " ";
            else if (result == "1,3") result += " ";
            else if (result == "1,4") result += " ";
            else if (result == "1,5") result += " ";
            return result;
        }

        public static string ShowFunction(float x)
        {
            string result = x.ToString();
            while(result.Length < 11)
            {
                result += " ";
            }
            return result;
        }
    }
}
