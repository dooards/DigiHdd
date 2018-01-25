using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using DigiSparkDotNet;

namespace DigiHddLamp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerAsync();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var digiSpark = new ArduinoUsbDevice();
            byte data = 0;
            digiSpark.WriteBytes(new[] { data });
            Application.Exit();
        }

        private void digiSpark_ArduinoUsbDeviceChangeNotifier(object sender, EventArgs e)
        {
            if (sender.ToString() == "False")
            {
                notifyIcon1.BalloonTipTitle = "Notice";
                notifyIcon1.BalloonTipText = "Device Unconnected";
                notifyIcon1.ShowBalloonTip(3000);
            }

            else if (sender.ToString() == "True")
            {
                notifyIcon1.BalloonTipTitle = "Notice";
                notifyIcon1.BalloonTipText = "Device Connected";
                notifyIcon1.ShowBalloonTip(3000);
            }
            else
            {

            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var pcDiskReadCps = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            var digiSpark = new ArduinoUsbDevice();
            digiSpark.ArduinoUsbDeviceChangeNotifier += digiSpark_ArduinoUsbDeviceChangeNotifier;

            while (true)
            {

                byte data;

                float y;
                y = pcDiskReadCps.NextValue();

                if (y == 0)
                {
                    data = 0;
                    digiSpark.WriteBytes(new[] { data });
                }
                if (y < 0.01)
                {
                    data = 0;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 0.01 && y < 1)
                {
                    data = 1;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 1 && y < 10)
                {
                    data = 2;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 10 && y < 20)
                {
                    data = 3;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 20 && y < 30)
                {
                    data = 4;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 30 && y < 40)
                {
                    data = 5;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 40 && y < 50)
                {
                    data = 6;
                    digiSpark.WriteBytes(new[] { data });
                }
                else if (y >= 50)
                {
                    data = 8;
                    digiSpark.WriteBytes(new[] { data });
                }


                Application.DoEvents(); // Gather USB events
                Thread.Sleep(1000);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
