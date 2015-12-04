using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Threading;


namespace ArTerm
{
    public class GraphWindow : Form
    {
        public Chart lineChart;
        public ChartArea chartArea;
        public Series series;
        public Button closeButton;

        public GraphWindow()
        {
            Text = "Ar Terminal Line Chart";
            Width = 700;
            Height = 500;
            MinimumSize = new Size(750, 500);
            series = new Series();

            ControlBox = false;
            lineChart = new Chart();
            chartArea = new ChartArea();
            lineChart.ChartAreas.Add(chartArea);
            series = lineChart.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;


            lineChart.Location = new Point(15, 15);
            lineChart.Width = Width - 50;
            lineChart.Height = Height - 70;
            lineChart.MinimumSize = new Size(700, 430);
            lineChart.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);

            closeButton = new Button();
            closeButton.Location = new Point(Width - 120, 20);
            closeButton.Text = "Close graph";
            closeButton.Click += new EventHandler(HideGraphEvent);

            Controls.Add(closeButton);
            Controls.Add(lineChart);
        }

        public void ShowGraph()
        {
            Show();
        }

        public void HideGraphEvent(object sender, EventArgs args)
        {
            Hide();
        }

        public void AddToGraph(string date, double weight)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    series.Points.AddXY(date, weight);
                }));
            }
            else
            {
                series.Points.AddXY(date, weight);
            }
        }
    }
}
