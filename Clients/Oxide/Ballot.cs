using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Silicon;
using Newtonsoft.Json.Linq;

namespace Oxide
{
    public partial class Ballot : Form
    {
        public Ballot()
        {
            InitializeComponent();
        }

        static bool sentflag = true;
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

        private void Ballot_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);
            this.StartPosition = FormStartPosition.Manual;
            this.Width = 650;
            this.Height = 980;
        }

        private void OPTION1_Click(object sender, EventArgs e)
        {
            RemoteSend(1);
        }

        private void OPTION2_Click(object sender, EventArgs e)
        {
            RemoteSend(2);
        }

        private void OPTION3_Click(object sender, EventArgs e)
        {
            RemoteSend(3);
        }

        private void OPTION4_Click(object sender, EventArgs e)
        {
            RemoteSend(4);
        }

        private void OPTION5_Click(object sender, EventArgs e)
        {
            RemoteSend(5);
        }

        public void RemoteSend(int option)
        {
            Console.Beep(700, 1500);
            string _CONFIGURATION_DATA_ = File.ReadAllText(@Configuration.CONFIGPATH);
            JObject ConfigurationData = JObject.Parse(_CONFIGURATION_DATA_);
            JArray ConfigItem = (JArray)ConfigurationData["CONFIGURATION"];
            bool flag = true;
            string ID = "";
            if(option == 1)
            {
                ID = SILICON64.GenerateHash("70001"+ Utility.USERID);
            }
            else if(option == 2)
            {
                ID = SILICON64.GenerateHash("70002" + Utility.USERID);
            }
            else if(option == 3)
            {
                ID = SILICON64.GenerateHash("70003" + Utility.USERID);
            }
            else if(option ==4)
            {
                ID = SILICON64.GenerateHash("70004" + Utility.USERID);
            }
            else if(option == 5)
            {
                ID = SILICON64.GenerateHash("70005" + Utility.USERID);
            }
            else
            {
                MessageBox.Show("Invalid Option");
                flag = false;
            }
            if(flag)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string Data = ID + "$" + timestamp;
                

                for (int i = 0; i < ConfigItem.Count; i++)
                {
                    //Thread.Sleep(100);
                    int PORT_NO = Configuration.FetchPort(i);
                    string SERVER_IP = Configuration.FetchServer(i);
                    sentflag = true;
                    SendData(SERVER_IP, Data, PORT_NO);
                    //threads.Add(new Thread(() => SendData(SERVER_IP, Data, PORT_NO)));
                }
                if(sentflag)
                {
                    if (File.Exists(@Utility.DATAPATH))
                    {
                        string _USER_DATA_ = File.ReadAllText(Utility.DATAPATH);
                        JObject UserData = JObject.Parse(_USER_DATA_);
                        JArray items = (JArray)UserData["UserBlock"];
                        Console.WriteLine("STATUS UPDATED : " + Utility.BLOCKID.ToString());
                        Console.WriteLine("USER   ID      : " + ((int)items[Utility.BLOCKID]["ID"]).ToString());
                        Console.WriteLine("User Name      : " + (string)items[Utility.BLOCKID]["Name"]);
                        items[Utility.BLOCKID]["Status"] = SILICON64.GenerateHash((string)items[Utility.BLOCKID]["ID"] + (string)items[Utility.BLOCKID]["Name"] + "0");
                        using (FileStream FILEHANDLE = File.Create(@Utility.DATAPATH))
                        {
                            string FileText = "{\"UserBlock\":\r\n" + items.ToString() + "\r\n}";
                            Byte[] ByteData = new UTF8Encoding(true).GetBytes(FileText);
                            FILEHANDLE.Write(ByteData, 0, ByteData.Length);
                            FILEHANDLE.Close();
                        }
                    }
                }
                this.Hide();
            }
        }
        public static void SendData(string SERVER_IP, string Data, int PORT_NO)
        {
            try
            {
                sentflag = true;
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
                    sentflag = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + Ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
