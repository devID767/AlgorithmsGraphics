using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Algorithms_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DrawGraph();
        }

        private void Algorithm(GraphPane pane)
        {
            //AddGraphsAlg_1(pane);
            //AddGraphsAlg_3(pane);
            AddGraphsAlg_4(pane);
        }

        private void AddGraphsAlg_4(GraphPane pane)
        {
            // 0 - Default  1 - Missing_1   2 - Missing_2   3 - Missing_3   4 - Missing_4
            int typeGraphs = 4;

            PointPairList First = new PointPairList();
            PointPairList Second = new PointPairList();
            PointPairList Missings = new PointPairList();

            if (typeGraphs == 0)
            {
                for (float x = -5; x < 5; x += 0.1f)
                {
                    First.Add(x, CommonFunctionInterpolation.Y(x));
                    Second.Add(CommonFunctionInterpolation.X(x), x);
                }
                LineItem paneFirst = pane.AddCurve("", First, Color.Blue, SymbolType.None);
                paneFirst.Line.Width = 2;
                LineItem paneSecond = pane.AddCurve("", Second, Color.Red, SymbolType.None);
                paneSecond.Line.Width = 2;
            }
            else if (typeGraphs == 1)
            {
                Iteration.SimpleIteration(1, -1.5f);
                
            }
            else if (typeGraphs == 2)
            {
                Iteration_SLAR.SimpleIteration_SLAR(0);
                
            }
            else if(typeGraphs == 3)
            {
                Iteration.ZeydelIteration(1, -1.5f);
            }
            else if(typeGraphs == 4)
            {
                Iteration_SLAR.ZeydalIteration_SLAR(0);
            }

            for (int i = 1; i < CommonFunctionInterpolation.missings.Count; i++)
            {
                Missings.Add(i, Math.Log10(CommonFunctionInterpolation.missings[i]));
            }
            LineItem paneMissing = pane.AddCurve("", Missings, Color.Blue, SymbolType.None);
            paneMissing.Line.Width = 2;
        }

        private void AddGraphsAlg_3(GraphPane pane)
        {
            // 0 - Default  1 - Bisection   2 - Nuton
            int typeGraphs = 2;

            float x_min = 0;
            float x_max = 5;

            float a = 1.4f;
            float b = 1.6f;
            float accuracy = 0.0001f;

            Lab_3 lab = new Lab_3();
            lab.Bisection(a, b, accuracy);
            lab.Nuton(a, b, accuracy);

            // Создадим список точек
            PointPairList First = new PointPairList();
            PointPairList Second = new PointPairList();
            PointPairList MissingBisection = new PointPairList();
            PointPairList MissingNuton = new PointPairList();

            if (typeGraphs == 0)
            {
                for (float x = x_min; x < x_max; x += 0.1f)
                {
                    First.Add(x, lab.MyFunction(x) + 5);
                    Second.Add(x, 5);
                }
                LineItem paneDefault = pane.AddCurve("y = x + x^(1/2) + x^(1/3) + x^(1/4)", First, Color.Blue, SymbolType.None);
                paneDefault.Line.Width = 2;
                LineItem paneSecond = pane.AddCurve("y = 5", Second, Color.Green, SymbolType.None);
                paneSecond.Line.Width = 2;
            }
            else if (typeGraphs == 1)
            {
                for (int k = 0; k < lab.missingBisection.Count; k++)
                {
                    MissingBisection.Add(k, lab.missingBisection[k]);
                }
                LineItem paneMBisection = pane.AddCurve("lg(|b - a|)", MissingBisection, Color.Green, SymbolType.None);
                paneMBisection.Line.Width = 2;
            }
            else if (typeGraphs == 2)
            {
                for (int k = 0; k < lab.missingNuton.Count; k++)
                {
                    MissingNuton.Add(k, lab.missingNuton[k]);
                }
                LineItem MissingMNuton = pane.AddCurve("lg(|x(k) - x(k-1)|", MissingNuton, Color.Green, SymbolType.None);
                MissingMNuton.Line.Width = 2;
            }
        }
        private void AddGraphsAlg_1(GraphPane pane)
        {
            int part = 2;
            int def = 2;

            if (part == 1)
                Lab1.FirstPart();
            else if (part == 2)
                Lab1.SecondPart();

            // Создадим список точек
            PointPairList Default = new PointPairList();
            PointPairList LagrangePiece = new PointPairList();
            PointPairList LagrangeGlobal = new PointPairList();

            PointPairList Derive_1 = new PointPairList();
            PointPairList Derive_2 = new PointPairList();
            PointPairList Aproxi_1 = new PointPairList();
            PointPairList Aproxi_2 = new PointPairList();

            for (int i = 0; i < Lab1.x_array.Count; i++)
            {
                if (part == 1)
                {
                    Default.Add(Lab1.x_array[i], Lab1.y_arrayDefault[i]);
                    LagrangePiece.Add(Lab1.x_array[i], Lab1.y_arrayLagrangePiece[i]);
                    LagrangeGlobal.Add(Lab1.x_array[i], Lab1.y_arrayLagrangeGlobal[i]);

                    LineItem paneDefault = pane.AddCurve("Формула", Default, Color.Blue, SymbolType.None);
                    paneDefault.Line.Width = 2;
                    LineItem panePiece = pane.AddCurve("Кусочний Лагранж", LagrangePiece, Color.Red, SymbolType.None);
                    panePiece.Line.Width = 2;
                    LineItem paneGlobal = pane.AddCurve("Глобальний Лагранж", LagrangeGlobal, Color.Green, SymbolType.None);
                    paneGlobal.Line.Width = 2;
                }
                else if (part == 2)
                {
                    if (def == 1)
                    {
                        Derive_1.Add(Lab1.x_array[i], Lab1.y_arrayDerive1[i]);
                        Aproxi_1.Add(Lab1.x_array[i], Lab1.y_arrayAproxi1[i]);
                        LineItem paneDerive_1 = pane.AddCurve("перша похідна", Derive_1, Color.Blue, SymbolType.None);
                        paneDerive_1.Line.Width = 2;
                        LineItem paneAproxi_1 = pane.AddCurve("ароксимація", Aproxi_1, Color.Green, SymbolType.None);
                        paneAproxi_1.Line.Width = 2;
                    }
                    else if (def == 2)
                    {
                        Derive_2.Add(Lab1.x_array[i], Lab1.y_arrayDerive2[i]);
                        Aproxi_2.Add(Lab1.x_array[i], Lab1.y_arrayAproxi2[i]);
                        LineItem paneDerive_2 = pane.AddCurve("друга похідна", Derive_2, Color.Red, SymbolType.None);
                        paneDerive_2.Line.Width = 2;
                        LineItem paneAproxi_2 = pane.AddCurve("ароксимація", Aproxi_2, Color.Purple, SymbolType.None);
                        paneAproxi_2.Line.Width = 2;
                    }
                }
            }
        }

        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            pane.Title.Text = "Axis Cross Demo";
            pane.XAxis.Title.Text = "My X Axis";
            pane.YAxis.Title.Text = "My Y Axis";

            Algorithm(pane);

            // Set the Y axis intersect the X axis at an X value of 0.0
            pane.YAxis.Cross = 0.0;
            // Turn off the axis frame and all the opposite side tics
            pane.Chart.Border.IsVisible = false;
            pane.XAxis.MajorTic.IsOpposite = false;
            pane.XAxis.MinorTic.IsOpposite = false;
            pane.YAxis.MajorTic.IsOpposite = false;
            pane.YAxis.MinorTic.IsOpposite = false;

            // Установим такой интервал изменения по оси X, чтобы наибольшие значения графика
            // остались за пределами отображаемой области
            //pane.XAxis.Scale.Min = x_min;
            //pane.XAxis.Scale.Max = x_max;

            // По оси Y установим автоматический подбор масштаба
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;

            // !!! Установим значение параметра IsBoundedRanges как true.
            // !!! Это означает, что при автоматическом подборе масштаба
            // !!! нужно учитывать только видимый интервал графика
            pane.IsBoundedRanges = true;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
