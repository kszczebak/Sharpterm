using System;

using System.IO.Ports;
using System.Threading;
namespace ArTerm
{
    public class Connection
    {
        private static SerialPort _serialPort;
        private Thread dataThread;
        private MainWindow gui;
        private Database dataBase;
        private GraphProcesses graphProcess;

        private string[] portsTab;
        private string data;
        private bool isThreadWorking;

        public Connection(MainWindow gui, Database dataBase)
        {
            this.gui = gui;
            _serialPort = new SerialPort();
            this.dataBase = dataBase;
            GetPortsFromSystem();
            gui.SetPortBox(portsTab);
        }

        public bool GetIsThreadWorking()
        {
            return isThreadWorking;
        }

        public string[] GetPortsList()
        {
            return portsTab;
        }

        public void GetPortsFromSystem()
        {
            if (portsTab != null)
            {
                portsTab = null;
            }
            portsTab = SerialPort.GetPortNames();
        }

        public void EstablishConnection(string port, int bits)
        {
            if (!port.Equals("") && !_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.PortName = port;
                    _serialPort.BaudRate = bits;
                    _serialPort.Open();
                    if (_serialPort.IsOpen)
                    {
                        gui.SetOutputText("Connected to " + port + "!\n");
                    }
                }
                catch (Exception e)
                {
                    gui.ShowWarningMessage("Error: " + e);
                }
            }
            else if (_serialPort.IsOpen)
            {
                gui.ShowWarningMessage("Connection is already established!");
            }
        }

        public void StartRecivingData()
        {
            if (_serialPort.IsOpen && isThreadWorking)
            {
                gui.ShowWarningMessage("You have established connection with your device!");
            }
            else if (_serialPort.IsOpen && !isThreadWorking)
            {
                isThreadWorking = true;
                dataThread = new Thread(ReciveData);
                dataThread.Start();
            }
            else if (!_serialPort.IsOpen)
            {
                gui.ShowWarningMessage("Firstly establish your connection with device!");
            }
        }

        public void Disconnect()
        {
            if (_serialPort.IsOpen && isThreadWorking)
            {
                isThreadWorking = false;
                _serialPort.Close();
                gui.SetOutputText("Disconnected!");
            }
            else if (_serialPort.IsOpen && !isThreadWorking)
            {
                _serialPort.Close();
                gui.SetOutputText("Disconnected!");
            }
        }

        public void ReciveData()
        {
            string toSend; // data with current time
            graphProcess = new GraphProcesses(gui.GetComponent().GetGraphWindow());
            dataBase.CleanDataBaseCollection(); // clean DB before thread start
            gui.SetOutputTextDuringConnection(DateTime.Now.ToString("dd.MM.yyyy") + "\n---------"); // header of data reciver
            while (isThreadWorking)
            {
                try
                {
                    data = _serialPort.ReadLine(); // read all recived data via COM port
                    if (!data.Equals("")) // do if data isn't empty
                    {
                        toSend = DateTime.Now.ToString("HH:mm:ss:ff\t") + data; // combine recived data with current time
                        graphProcess.SendDataToGraph(toSend);
                        dataBase.AddToDatabase(toSend); // send to DB
                        gui.SetOutputTextDuringConnection(toSend); // put recived data into output box
                    }
                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    //gui.SetOutputTextDuringConnection("error" + e);
                    Disconnect();
                }
            }
            Disconnect();
        }
    }
}
