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
            PositionTimer.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //int i = 10;
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
            if (!_focuser.thePort.IsOpen)
            {
                return;
            }
            _focuser.thePort.WriteLine("p");

            //_focuser._position = 20;
            string resp = _focuser.thePort.ReadLine();
            DbgMsg(resp);
            string[] parts = resp.Split(' ');
            if (parts[0] != "P")
            {
                DbgMsg("Could not parse response");
            }
            else
            {
                _focuser._position = Convert.ToInt16(parts[1]);
                ActualPosition.Text = parts[1];
            }
        }

        private void DbgMsg(string msg)
        {
            if (_debugForm == null)
            {
                return;
            }
            _debugForm.AddText(msg);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            _focuser.thePort.WriteLine("m "+TargetPosition.Value.ToString());

            //_focuser._position = 20;
            string resp = _focuser.thePort.ReadLine();
            DbgMsg(resp);
            string[] parts = resp.Split(' ');
            if (parts[0] != "M")
            {
                DbgMsg("Could not parse response");
            }
            else
            {
                //_focuser._position = Convert.ToInt16(parts[1]);
                //ActualPosition.Text = parts[1];
            }

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (StopButton.Text == "Stop!")
            {
                // stop the focuser
                StopButton.Text = "Resume";
                SendCommand("h");
            }
            else
            {
                // resume
                StopButton.Text = "Stop!";
                SendCommand("r");
            }
        }

        private string SendCommand(string Cmd)
        {
            DbgMsg(String.Concat(">>>", Cmd));

            _focuser.thePort.WriteLine(Cmd);

            string[] in_parts = Cmd.Split(' ');

            string resp = _focuser.thePort.ReadLine();
            DbgMsg(String.Concat("<<<", resp));
            string[] out_parts = resp.Split(' ');
            if (out_parts[0] != (in_parts[0].ToUpper()))
            {
                DbgMsg("Could not parse response");
                return "";
            }
            else
            {
                if (out_parts.Length > 1)
                {
                    if (in_parts.Length > 1 && in_parts[1] != out_parts[1])
                    {
                        DbgMsg("Did not receive argument back");
                    }
                    return out_parts[1];
                }
                else
                    return "";
            }

        }
    }
}
