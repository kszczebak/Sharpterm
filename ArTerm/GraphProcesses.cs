using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArTerm
{
    public class GraphProcesses
    {
        public Thread graphThread;
        private GraphWindow graphWindow;
        
        public GraphProcesses(GraphWindow graphWindow)
        {
            this.graphWindow = graphWindow;
        }

        public void SendDataToGraph(string recivedData)
        {
            if (graphWindow != null)
            {
                string date;
                string weight;
                if (!recivedData.Equals("") || recivedData != null)
                {
                    date = recivedData.Substring(0, 11); // get date form data
                    if (System.Text.RegularExpressions.Regex.IsMatch(recivedData.Substring(12, 2), "^\\d{2}$")) // if weight has 2 decimals before dot
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(recivedData.Substring(15, 3), "^\\d{3}$")) // if weight has 3 decimals after dot - do staff
                        {
                            weight = recivedData.Substring(12, 2) + "," + recivedData.Substring(15, 3); // convert weight to proper format
                            graphWindow.AddToGraph(date, Convert.ToDouble(weight));
                        }
                    }
                }
            }
        }
    }
}
