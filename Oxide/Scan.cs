using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;
using System.Media;
using MySql.Data.MySqlClient;


namespace Oxide
{
    
    public partial class Scan : Form
    {
        static string connection = "Server=35.201.148.29; DATABASE = pbl2018;UID=prashanth.kumar;PASSWORD=Hunter21#$;";
        public static MySqlConnection connect = new MySqlConnection(connection);
        public static string userid;
        public static int vote;
        FilterInfoCollection capturedev;
        private VideoCaptureDevice finalframe;

        public Scan()
        {
            InitializeComponent();
           
        }

        private void Scan_Load(object sender, EventArgs e)
        {
            capturedev = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Dev in capturedev)
            CameraTag.Text = CameraTag.Text + Dev.Name;
            qrcode(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader red = new BarcodeReader();
            string dec;
            if(Imager.Image != null)
            {
                Result res = red.Decode((Bitmap)Imager.Image);
                try
                {
                    dec = res.ToString().Trim();
                    Clock.Stop();
                    qrcode();
                    label1.Text = dec;
                    /*if(dec == "close$close")
                    {
                        connect.Open();
                        MySqlCommand Query1 = new MySqlCommand("INSERT INTO vote_misc (hash) VALUES ('1');", connect);
                        Query1.ExecuteReader();
                        connect.Close();
                        Application.Exit();
                    }
                    int range = Convert.ToInt32(dec);
                    if(range <= 50010 && range >= 50001)
                    {
                        connect.Open();
                        MySqlCommand Query1 = new MySqlCommand("select vote from vote_raw where userid = '" + dec + "' ", connect);
                        using (MySqlDataReader rdr = Query1.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                vote = (int)rdr["vote"];
                            }
                        }
                        connect.Close();
                        if (vote == 0)
                        {
                            Console.Beep(2500, 1500);
                        }
                        else
                        {
                            Console.Beep(700, 500);
                            userid = dec;
                            Ballot ballot_screen = new Ballot();
                            ballot_screen.Show();
                            userid = dec;
                            connect.Close();
                        }
                    }*/
                }
                catch (Exception ex)
                 { }
                }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Imager.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception ex)
            {

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
