using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms_1
{

    public enum InterpolationType
    {
        Global,
        Piece
    }

    class Interpolation
    {
        private List<float> x_table;
        private List<float> y_table;
        private float[] x_originTable;
        private float[] y_originTable;

        public float a;
        public float b;
        public float h;


        private List<float> arrayClosesValue = new List<float>();

        private InterpolationType _interpolation;

        private List<int> count_xy = new List<int>();

        public Interpolation(List<float> x_table, List<float> y_table)
        {
            this.x_table = x_table;
            this.y_table = y_table;

            x_originTable = new float[x_table.Count];
            y_originTable = new float[y_table.Count];
            x_table.CopyTo(x_originTable);
            y_table.CopyTo(y_originTable);
        }

        public void SetXValues(float a = 0, float b = 1.5f, float h = 0.01f)
        {
            this.a = a;
            this.b = b;
            this.h = h;
        }

        private void GetOriginArray()
        {
            x_table.Clear();
            y_table.Clear();

            for (int i = 0; i < x_originTable.Length; i++)
            {
                x_table.Add(x_originTable[i]);
                y_table.Add(y_originTable[i]);
            }
        }

        public float Lagrang(float x, InterpolationType interpolation, int n = 0)
        {
            switch (interpolation)
            {
                case InterpolationType.Global:
                    return LagrangCalculate(x);
                case InterpolationType.Piece:
                    for (int i = 0; i <= n; i++)
                    {
                        count_xy.Add(ClosesValue(x));
                    }
                    break;
            }
            _interpolation = interpolation;
            return NextStep(x, n);
        }
        private float NextStep(float x, int n = 0)
        {
            ChangeArray(n);
            arrayClosesValue.Clear();

            float result = LagrangCalculate(x);
            GetOriginArray();
            count_xy.Clear();

            return result;
        }

        private float LagrangCalculate(float x)
        {
            float result = 0;

            for (int i = 0; i < y_table.Count; i++)
            {
                result += y_table[i] * l(i, x);
            }

            return result;
        }

        private float l(int k, float x)
        {
            float result = 1;

            for (int i = 0; i < x_table.Count; i++)
            {
                if (k != i)
                {
                    result *= (x - x_table[i]) / (x_table[k] - x_table[i]);
                }
            }
            return result;
        }

        private void ChangeArray(int n = 0)
        {
            List<float> x_smth = new List<float>();
            List<float> y_smth = new List<float>();

            if (n != 0)
            {
                if (n < 1 || n >= x_table.Count) throw new Exception($"n не може бути менше 1 та більше максимального степеня {x_table.Count - 1}");
                for (int i = 0; i < count_xy.Count; i++)
                {
                    x_smth.Add(x_table[count_xy[i]]);
                    y_smth.Add(y_table[count_xy[i]]);
                }
            }

            if (_interpolation == InterpolationType.Piece)
            {
                x_table.Clear();
                y_table.Clear();
                foreach (var item in x_smth)
                {
                    x_table.Add(item);
                }
                foreach (var item in y_smth)
                {
                    y_table.Add(item);
                }
            }

        }

        private int ClosesValue(float x)
        {
            float result = 0;
            int count = -1;

            float minvalue = 0;
            float absoluteMinValue = x + 1;

            for (int i = 0; i < x_table.Count; i++)
            {
                minvalue = Math.Abs(x - x_table[i]);
                if (absoluteMinValue >= minvalue && !IsInArray(x_table[i], arrayClosesValue))
                {
                    count = i;
                    result = x_table[i];
                    absoluteMinValue = minvalue;
                }
            }
            if(count == -1)
            {
                for (int i = 0; i < x_table.Count; i++)
                {
                    minvalue = Math.Abs(x - x_table[i]);
                    if (absoluteMinValue < minvalue && !IsInArray(x_table[i], arrayClosesValue))
                    {
                        count = i;
                        result = x_table[i];
                        absoluteMinValue = minvalue;
                    }
                }
            }

            arrayClosesValue.Add(result);
            return count;
        }

        private bool IsInArray(float obj, List<float> array)
        {
            if (array == null)
            {
                return false;
            }
            foreach (var item in array)
            {
                if (obj == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
