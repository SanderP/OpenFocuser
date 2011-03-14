using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ASCOM.OpenFocuser
{
    public partial class MainForm : Form
    {
        public MainForm(Focuser f, ref Config c)
        {
            _focuser = f;
            _config = c;

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_focuser.Link)
            {
                VersionLabel.Text = _focuser.Version;
                PositionTimer.Enabled = true;
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            if (_debugForm == null)
            {
                _debugForm = new DebugForm();
            }
            _debugForm.Show();
        }

        private void PositionTimer_Tick(object sender, EventArgs e)
        {
            if (_focuser.Link)
                ActualPosition.Text = _focuser.GetPosition().ToString();
            else
                ActualPosition.Text = "NC";
        }

        public void DbgMsg(string msg)
        {
            if (_debugForm == null)
            {
                return;
            }
            _debugForm.AddText(msg);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            _focuser.Move(Convert.ToInt16(TargetPosition.Value));
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopButton.Text == "Stop!")
            {
                // stop the focuser
                StopButton.Text = "Resume";
                _focuser.SendCommand("s");
            }
            else
            {
                // resume
                StopButton.Text = "Stop!";
                _focuser.SendCommand("r");
            }
        }

        private void PresetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void EnableUpdateTimer()
        {
            PositionTimer.Enabled = true;
        }

    }
}
