using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Node
{
    public partial class Core : Form
    {
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
        public Core()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            ControlPanel.Controls.Clear();
            Dashboard _formPreview = new Dashboard();
            _formPreview.TopLevel = false;
            ControlPanel.Controls.Add(_formPreview);
            _formPreview.Dock = DockStyle.Fill;
            _formPreview.Show();
        }

        private void Core_Load(object sender, EventArgs e)
        {
                ControlPanel.Controls.Clear();
                Dashboard _formPreview = new Dashboard();
                _formPreview.TopLevel = false;
                ControlPanel.Controls.Add(_formPreview);
                _formPreview.Dock = DockStyle.Fill;
                _formPreview.Show();
                Utility.home = 1;
           
            if(Utility.Juno_Function == 0)
            {
                Juno();
                Utility.Juno_Function = 1;
            } 
        }

        public void Juno()
        {
            Thread _datathread = new Thread(RequestProcessor.DataServer);
            _datathread.Start();
        }

        private void DatabaseButton_Click(object sender, EventArgs e)
        {
            ControlPanel.Controls.Clear();
            Infoboard _formPreview = new Infoboard();
            _formPreview.TopLevel = false;
            ControlPanel.Controls.Add(_formPreview);
            _formPreview.Dock = DockStyle.Fill;
            _formPreview.Show();
        }

        private void ShutdownButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalculationButton_Click(object sender, EventArgs e)
        {
            ControlPanel.Controls.Clear();
            Calculation _calculationPreview = new Calculation();
            _calculationPreview.TopLevel = false;
            ControlPanel.Controls.Add(_calculationPreview);
            _calculationPreview.Dock = DockStyle.Fill;
            _calculationPreview.Show();
        }
    }
}