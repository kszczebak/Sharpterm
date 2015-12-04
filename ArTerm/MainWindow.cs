using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace ArTerm
{
    public class MainWindow : Form
    {

        private Connection connection;
        private Database database;
        private Components components;
        public MainWindow(int width, int height)
        {
            components = new Components(this, width, height);
            database = new Database();
            connection = new Connection(this,database);
            FormClosed += OnClose;
            Application.Run(this);
        }

        public Components GetComponent()
        {
            return components;
        }
        public void OnClose(object sender, EventArgs args)
        {
            connection.Disconnect();
            //Thread.Sleep(1500);// before window close, stop connection
        }

        public void OpenPortButtonEvent(object sender, EventArgs e)
        {
            if (components.portsBox.SelectedItems.Count != 0)
            {
                components.openDeviceBtn.Enabled = true;
            }
        }

        public void OpenDeviceBtnEvent(object sender, EventArgs e)
        {
            components.outputBox.AppendText("Connecting...\n");
            connection.EstablishConnection(components.portsBox.GetItemText(components.portsBox.SelectedItem), Int32.Parse(components.bitsList.GetItemText(components.bitsList.SelectedItem)));
        }

        public void StartRecivingDataButtonEvent(object sender, EventArgs e)
        {
            connection.StartRecivingData();
        }

        public void StopRecivingDataButtonEvent(object sender, EventArgs e)
        {
            connection.Disconnect();
            //Thread.Sleep(1500);
        }

        public void CloseAllConnectionEvent(object sender, EventArgs e)
        {
            connection.Disconnect();
            //Thread.Sleep(1500);
        }

        public void SetOutputTextDuringConnection(string text) // show recived data
        {
            if ((!text.Equals("") || text != null))
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate
                        {
                            components.outputBox.AppendText(text + "\n");
                            components.outputBox.SelectionStart = components.outputBox.Text.Length; // scrolling the output box
                            components.outputBox.ScrollToCaret();
                        }
                    ));
                }
            }
        }
        public void SetOutputText(string text) // show recived data
        {
            if (!text.Equals("") || text != null)
            {
                components.outputBox.AppendText(text + "\n");
                components.outputBox.SelectionStart = components.outputBox.Text.Length; // scrolling the output box
                components.outputBox.ScrollToCaret();
            }
        }
        public void SetPortBox(System.Collections.ArrayList portList)
        {
            foreach (string port in portList)
            {
                components.portsBox.Items.Add(port);
            }
        }
        public void SetPortBox(string[] portList)
        {
            string properPortName = "";
            string properStringFormat;

            foreach (string port in portList)
            {
                properStringFormat = "[^COM\\d+$]";
                Regex regex = new Regex(properStringFormat);
                string result = regex.Replace(port, properPortName);
                components.portsBox.Items.Add(result);
            }
        }

        public void UpdatePortBox(object sender, EventArgs e)
        {
            components.portsBox.Items.Clear();
            connection.GetPortsFromSystem();
            SetPortBox(connection.GetPortsList());
        }

        public void ShowWarningMessage(string text)
        {
            MessageBox.Show(text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        public void ToolStripItemSaveToEvent(object sender, EventArgs args)
        {
            if(!connection.GetIsThreadWorking())
            {
                if (components.GetClickedToolStripItemName() == "Save to Excel 97-2003")
                {
                    if (components.SaveFileWindow(".xls"))
                    {
                        database.SaveToExcel(components.GetSavedFileName() + ".xls");
                        database.CleanDataBaseCollection();
                    }
                }
                else if (components.GetClickedToolStripItemName() == "Save to Excel from 2007")
                {
                    if (components.SaveFileWindow(".xlsx"))
                    {
                        database.SaveToExcel(components.GetSavedFileName() + ".xlsx");
                        database.CleanDataBaseCollection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot save file while connection is active!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            
        }
        
    }
}
