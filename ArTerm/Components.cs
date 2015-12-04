using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ArTerm
{
    public class Components
    {
        public RichTextBox outputBox;

        public Button openDeviceBtn;
        public Button startRecivingDataBtn;
        public Button stopRecivingDataBtn;
        public Button portsUpdateBtn;
        public Button closeConnetionBtn;
       
        public ListBox portsBox;
        public ComboBox bitsList;
        public Label baudLabel, availablePorts;

        public MenuStrip menuStrip;
        public ToolStripMenuItem menuStripItemSave, menuStripItemGraph, menuStripItemAbout;
        public ToolStripMenuItem saveItem;
        public SaveFileDialog saveFileWindow;

        private string savedFileName;
        private string clickedToolStripItem;

        private static GraphWindow graphWindow;
        public Components(MainWindow mainWindow, int width, int height)
        {
            mainWindow.Text = "Ar Terminal";
            mainWindow.Width = width;
            mainWindow.Height = height;
            mainWindow.MinimumSize = new Size(750, 500);
            graphWindow = new GraphWindow();

            outputBox = new RichTextBox();
            outputBox.AppendText("To start chose suitable port COM.\n");
            outputBox.Font = new Font("Microsoft San Serif", 13);
            outputBox.ReadOnly = true;
            outputBox.Location = new Point(12, 30);
            outputBox.Width = width / 2;
            outputBox.Height = height - 100;
            outputBox.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);

            openDeviceBtn = new Button();
            openDeviceBtn.Enabled = false;
            openDeviceBtn.Click += mainWindow.OpenDeviceBtnEvent;
            openDeviceBtn.Location = new Point(width - 170, 30);
            openDeviceBtn.Text = "Open device";
            openDeviceBtn.Font = new Font("Microsoft San Serif", 12);
            openDeviceBtn.Width = 130;
            openDeviceBtn.Height = 60;
            openDeviceBtn.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

            startRecivingDataBtn = new Button();
            startRecivingDataBtn.Location = new Point(width - 400, height - 100);
            startRecivingDataBtn.Click += mainWindow.StartRecivingDataButtonEvent;
            startRecivingDataBtn.Text = "START";
            startRecivingDataBtn.Width = 150;
            startRecivingDataBtn.Height = 50;
            startRecivingDataBtn.BackColor = Color.LightGreen;
            startRecivingDataBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

            stopRecivingDataBtn = new Button();
            stopRecivingDataBtn.Location = new Point(width - 200, height - 100);
            stopRecivingDataBtn.Click += mainWindow.StopRecivingDataButtonEvent;
            stopRecivingDataBtn.Text = "STOP!";
            stopRecivingDataBtn.Width = 150;
            stopRecivingDataBtn.Height = 50;
            stopRecivingDataBtn.BackColor = Color.IndianRed;
            stopRecivingDataBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

            portsUpdateBtn = new Button();
            portsUpdateBtn.Location = new Point(width - 155, 110);
            portsUpdateBtn.Click += mainWindow.UpdatePortBox;
            portsUpdateBtn.Text = "Update ports";
            portsUpdateBtn.Width = 100;
            portsUpdateBtn.Height = 30;
            portsUpdateBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            closeConnetionBtn = new Button();
            closeConnetionBtn.Location = new Point(width - 165, 160);
            closeConnetionBtn.Text = "Close connections";
            closeConnetionBtn.Width = 120;
            closeConnetionBtn.Height = 30;
            closeConnetionBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            closeConnetionBtn.Click += mainWindow.CloseAllConnectionEvent;

            bitsList = new ComboBox();
            bitsList.Location = new Point(width - 340, 167);
            int[] bitsValues = new int[] { 75, 110, 134, 150, 300, 600, 1200, 1800, 2400, 4800, 7200, 9600, 14400, 19200, 38400, 57600, 115200, 128000 };
            foreach (int bit in bitsValues)
            {
                bitsList.Items.Add(bit);
            }
            bitsList.SelectedIndex = 11;
            bitsList.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            baudLabel = new Label();
            baudLabel.Text = "Bits per second:";
            baudLabel.Location = new Point(width - 430, 170);
            baudLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            availablePorts = new Label();
            availablePorts.Text = "Available ports:";
            availablePorts.Location = new Point(width - 430, 30);
            availablePorts.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            portsBox = new ListBox();
            portsBox.Font = new Font("Microsoft San Serif", 10);
            portsBox.Location = new Point(width - 340, 30);
            portsBox.Click += mainWindow.OpenPortButtonEvent;
            portsBox.Width = 65;
            portsBox.Height = 100;
            portsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            menuStrip = new MenuStrip(); // initialize menu bar 

            menuStripItemSave = new ToolStripMenuItem("Save results"); // item in menu bar
            menuStrip.Items.Add(menuStripItemSave);

            menuStripItemGraph = new ToolStripMenuItem("Graph");
            menuStripItemGraph.Click += OpenGraphEvent;
            menuStrip.Items.Add(menuStripItemGraph);

            menuStripItemAbout = new ToolStripMenuItem("About");
            menuStripItemAbout.Click += AboutProgrammEvent;
            menuStrip.Items.Add(menuStripItemAbout);

            saveItem = new ToolStripMenuItem("Save to Excel 97-2003", null, GetTextFromToolStripItemEvent); // item in menu bar item
            saveItem.Click += mainWindow.ToolStripItemSaveToEvent;
            menuStripItemSave.DropDownItems.Add(saveItem); // add item of menu bar item
            saveItem = new ToolStripMenuItem("Save to Excel from 2007", null, GetTextFromToolStripItemEvent); // another...
            saveItem.Click += mainWindow.ToolStripItemSaveToEvent;
            menuStripItemSave.DropDownItems.Add(saveItem);
            //saveItem = new ToolStripMenuItem("Save to Notepad", null, mainWindow.SaveToExcelOld);
            //menuItems.DropDownItems.Add(saveItem);


            mainWindow.Controls.Add(closeConnetionBtn);
            mainWindow.Controls.Add(menuStrip);
            mainWindow.Controls.Add(portsBox);
            mainWindow.Controls.Add(portsUpdateBtn);
            mainWindow.Controls.Add(stopRecivingDataBtn);
            mainWindow.Controls.Add(startRecivingDataBtn);
            mainWindow.Controls.Add(openDeviceBtn);
            mainWindow.Controls.Add(outputBox);
            mainWindow.Controls.Add(bitsList);
            mainWindow.Controls.Add(baudLabel);
            mainWindow.Controls.Add(availablePorts);
        }
        

        
        public bool SaveFileWindow(string fileExtension)
        {
            saveFileWindow = new SaveFileDialog(); // create save file window
            saveFileWindow.InitialDirectory = @"c:\"; // startup directory
            saveFileWindow.Filter = fileExtension + "|"; // set file extensions 
            saveFileWindow.RestoreDirectory = true;
            saveFileWindow.FileName = "ArTerm_" + DateTime.Now.ToString("dd.MM.yyyy_HH:mm:ss");
            if(saveFileWindow.ShowDialog() == DialogResult.OK) // after clicked OK button save file name to variable
            {
                savedFileName = saveFileWindow.FileName;
                return true;
            }
            return false;
        }
        public GraphWindow GetGraphWindow()
        {
            return graphWindow;
        }
        public string GetSavedFileName()
        {
            return savedFileName;
        }
        public string GetClickedToolStripItemName()
        {
            return clickedToolStripItem;
        }
        public void GetTextFromToolStripItemEvent(object sender, EventArgs args)
        {
            ToolStripItem item = (ToolStripItem)sender;
            clickedToolStripItem = item.Text;
        }
        public void OpenGraphEvent(object sender, EventArgs args)
        {
            //graphWindow.ShowGraph();
            
            graphWindow.ShowGraph();
        }

        public void AboutProgrammEvent(object sender, EventArgs args)
        {
            MessageBox.Show(
                "This software is designed for tensometer (own porposes only) designed by Konrad Szczebak. It can be used as terminal but the owner" +
                " doesn't guarantee proper work with another devices.\n\nContact: kszczebak@gmail.com", "About software", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
