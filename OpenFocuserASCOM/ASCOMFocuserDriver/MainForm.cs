using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using ASCOM.
using ASCOM.Interface;
using ASCOM.Utilities;
using ASCOM.DriverAccess;

namespace ASCOMFocuserDriver
{
    public partial class MainForm : Form
    {
        private Focuser _focuser;
        private string _progID;
        //private string _appID = "ASCOM.OpenFocuser.Focuser";
        //private Profile _profile = new Profile();

        public MainForm()
        {
            //_progID = _profile.GetValue(_appID, "ChosenFocuser");

            InitializeComponent();
            ChosenFocuser.Text = _progID;
            if (_progID == null || _progID == "")
                ConnectButton.Enabled = false;
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            ASCOM.Utilities.Chooser _chooser = new Chooser();
            _chooser.DeviceType = "Focuser";
            _progID = _chooser.Choose(_progID);
            _chooser.Dispose();
            ChosenFocuser.Text = _progID;
            //_profile.WriteValue(_appID, "ChosenFocuser", _progID);
            if (_progID == "")
                return;
            if (_progID == "")
                ConnectButton.Enabled = false;
            else
                ConnectButton.Enabled = true;
            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ConnectButton.Text == "Connect" && _progID != "")
            {
                if (_focuser == null)
                {
                    _focuser = new Focuser(_progID);
                }
                _focuser.Link =true;
                ConnectButton.Text = "Disconnect";
            }
            else
            {
                _focuser.Link = false;
                ConnectButton.Text = "Connect";
            }
        }


    }
}
