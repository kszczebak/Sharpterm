using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ZedGraph;
using System.Threading;

namespace ArTerm
{
    class Chart : Form
    {
        public Thread graphThread;
        public ZedGraphControl zedControl;
        public Chart()
        {
            this.Text = "Ar Terminal Line Chart";
            this.Width = 700;
            this.Height = 500;
            this.MinimumSize = new Size(750, 500);
            zedControl = new ZedGraphControl();
            zedControl.Location = new System.Drawing.Point(15, 15);
            zedControl.Width = this.Width - 50;
            zedControl.Height = this.Height - 75;
            zedControl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
            this.Controls.Add(zedControl);
            
            
            this.Show();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            CreateGraph(zedControl);
        }

        public void CreateGraph(ZedGraphControl graphControl)
        {
            GraphPane graph = graphControl.GraphPane;
            graph.Title.Text = "My Test Graph\n(For CodeProject Sample)";
            graph.XAxis.Title.Text = "My X Axis";
            graph.YAxis.Title.Text = "My Y Axis";

            // Make up some data arrays based on the Sine function
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
                list1.Add(x, y1);
                list2.Add(x, y2);
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = graph.AddCurve("Porsche",
                  list1, Color.Red, SymbolType.Diamond);

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve2 = graph.AddCurve("Piper",
                  list2, Color.Blue, SymbolType.Circle);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            graphControl.AxisChange();
        }
    }
}
