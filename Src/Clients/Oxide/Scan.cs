using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.Net.Sockets;
using System.IO;
using Silicon;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Oxide
{
    public partial class Scan : Form
    {
        FilterInfoCollection capturedev;
        private VideoCaptureDevice finalframe;
        
        BarcodeReader ReadQR = new BarcodeReader();
        string QRCode = "";
        public Scan()
        {
            InitializeComponent();
        }

        bool enabled = false;
        private bool _dragging;
        private Point _offset;

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point
                    (currentScreenPos.X - _offset.X,
                     currentScreenPos.Y - _offset.Y);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            _offset.X = e.X;
            _offset.Y = e.Y;
            _dragging = true;

        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void Scan_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
           // this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            ErrorTag.Text = "";
                capturedev = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo Dev in capturedev)
                    CameraTag.Text = CameraTag.Text + Dev.Name;
                qrcode();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Imager.Image != null)
            {
               
                Result InputRead = ReadQR.Decode((Bitmap)Imager.Image);
                try
                {
                    if(InputRead.ToString().Trim() != "")
                        QRCode = InputRead.ToString().Trim();
                    Clock.Stop();
                    qrcode();
                    label1.Text = QRCode;
                    if (QRCode == "close$")
                    {
                        string _CONFIGURATION_DATA_ = File.ReadAllText(@Configuration.CONFIGPATH);
                        JObject BLOCKCHAIN = JObject.Parse(_CONFIGURATION_DATA_);
                        JArray items = (JArray)BLOCKCHAIN["CONFIGURATION"];
                        for (int i = 0; i < items.Count; i++)
                        {
                            int PORT_NO = Configuration.FetchPort(i);
                            string SERVER_IP = Configuration.FetchServer(i);
                            SendLock(SERVER_IP, PORT_NO);
                            label1.Text = "SYSTEM LOCKED";
                            Clock.Stop();
                            Imager.Image = null;
                        }
                    }
                    else
                    {
                        //
                        //
                        Utility.USERID = Convert.ToInt32(QRCode);
                        //
                        //
                        if (Utility.USERID >= 10001 && Utility.USERID <= 10006)
                        {
                            string ___BLOCKDATA = File.ReadAllText(@Utility.DATAPATH);
                            JObject BLOCKCHAIN = JObject.Parse(___BLOCKDATA);
                            JArray items = (JArray)BLOCKCHAIN["UserBlock"];
                            for (int x = 0; x < items.Count; x++)
                            {
                                //Console.WriteLine((string)items[x]["Status"]);
                                //Console.WriteLine(SILICON64.GenerateHash((string)items[x]["ID"] + (string)items[x]["Name"] + "1"));
                                if (Utility.USERID == (int)items[x]["ID"])
                                {
                                    Console.WriteLine((string)items[x]["Status"]);
                                    Console.WriteLine(SILICON64.GenerateHash((string)items[x]["ID"] + (string)items[x]["Name"] + "1"));
                                    if ((string)items[x]["Status"] == SILICON64.GenerateHash((string)items[x]["ID"]+ (string)items[x]["Name"]+"1"))
                                    {
                                        Utility.BLOCKID = x;
                                        //this.Hide();
                                        Ballot _ballot = new Ballot();
                                        _ballot.Visible = true;
                                        break;
                                    }
                                    else if ((string)items[x]["Status"] == SILICON64.GenerateHash((string)items[x]["ID"] + (string)items[x]["Name"] + "0"))
                                    {
                                        ErrorTag.Text = "User already Voted";
                                        Console.Beep(2500, 1500);
                                    }
                                    else
                                    {
                                        ErrorTag.Text = "Invalid User Data";
                                        Console.Beep(2500, 1500);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception Exeception)
                {
                    //MessageBox.Show(Exeception.Message);
                }
            }
        }

        public static void SendLock(string SERVER_IP, int PORT_NO)
        {
            string Data = "LOCK$ACTIVATE";
            try
            {
                TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(Data);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[" + SERVER_IP + ":" + PORT_NO.ToString() + "]" + "--Data Transferred");
                Console.ForegroundColor = ConsoleColor.White;
                /*byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                string reply = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                if (reply == "1")
                {

                }
                else
                {
                    Console.WriteLine("Error sending Text");
                }*/
                client.Close();
            }
            catch (Exception Ex)
            {
                if (Ex.Message != "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + Ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Imager.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception Exeception)
            {//
                MessageBox.Show(Exeception.Message);
            }
        }

        private void qrcode()
        {
            finalframe = new VideoCaptureDevice(capturedev[0].MonikerString);
            finalframe.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            finalframe.Start();
            Clock.Enabled = true;
            Clock.Start();
        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (finalframe != null)
                if (finalframe.IsRunning == true)
                {
                    finalframe.Stop();
                }
        }
    }
}