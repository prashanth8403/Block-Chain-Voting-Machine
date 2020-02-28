using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Threading;

namespace Oxide
{
    public partial class Ballot : Form
    {

        string userid = Scan.userid;
        string votehash;
        static string connection = "Server=35.201.148.29; DATABASE = pbl2018;UID=prashanth.kumar;PASSWORD=Hunter21#$;";
        public MySqlConnection connect = new MySqlConnection(connection);
        Scan scan_sceen = new Scan();


        public Ballot()
        {
            InitializeComponent();
        }


        private void Ballot_Load(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(this.Width * (0.96));
            
            label1.Text = userid.ToString();
            button1.Width = width;
            button2.Width = width;
            button3.Width = width;
            button4.Width = width;
            button5.Width = width;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            insert_data("8501");
            Console.Beep(700, 1500);
            this.Hide();
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            insert_data("8502");
            Console.Beep(700, 1500);
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            insert_data("8503");
            Console.Beep(700, 1500);
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            insert_data("8504");
            Console.Beep(700, 1500);
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            insert_data("8505");
            Console.Beep(700, 1500);
            this.Hide();
        }

        public static string SILICON64(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public void insert_data(string id)
        {
            connect.Open();
            votehash = SILICON64(id + userid + "silicon-regulation");
            MySqlCommand Query1 = new MySqlCommand("INSERT INTO vote_view (userID, hash) VALUES ( '" + userid + "', '" + votehash + "'); UPDATE vote_raw SET vote = '0' where userid = '" + Convert.ToInt32(userid) + "';", connect);
            Query1.ExecuteReader();
            connect.Close();
            Thread ThreadWorker = new Thread(BlockMailer);
            ThreadWorker.Start();
        }

        public void BlockMailer()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("prashanthkumar951@gmail.com");
            mail.To.Add("bmsit@gmx.com");
            mail.To.Add("anodebcvm@gmail.com");
            mail.To.Add("cathodebcvm@gmail.com");
            mail.To.Add("koushik.s.shiroor@gmail.com");
            mail.To.Add("manu.pritham@gmail.com");
            mail.To.Add("prajwalkkp@gmail.com");
            mail.From = new MailAddress("votepbl2018@gmail.com");
            mail.Subject = "BLOCK[SECURED] 🔐- >> " + votehash +"";
            string Body = "SILICON-DPAR \n\n" +votehash;
            mail.Body = Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            string u = "votepbl2018@gmail.com", v = "Hunter21#";
            smtp.Credentials = new System.Net.NetworkCredential(u, v);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}