//tabs=4
// --------------------------------------------------------------------------------
//
// ASCOM Focuser driver for OpenFocuser
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Focuser interface version: 1.0
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	1.0.0	Initial edit, from ASCOM Focuser Driver template
// --------------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO.Ports;


using ASCOM;
//using ASCOM.Helper;
//using ASCOM.Helper2;
using ASCOM.Interface;

namespace ASCOM.OpenFocuser
{
    //
    // Your driver's ID is ASCOM.OpenFocuser.Focuser
    //
    // The Guid attribute sets the CLSID for ASCOM.OpenFocuser.Focuser
    // The ClassInterface/None addribute prevents an empty interface called
    // _Focuser from being created and used as the [default] interface
    //
    [Guid("1776706f-fad0-4dd4-bf7e-fef953ef7cf4")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Focuser : IFocuser
    {
        //
        // Driver ID and descriptive string that shows in the Chooser
        //
        public static string s_csDriverID = "ASCOM.OpenFocuser.Focuser";
        private static string s_csDriverDescription = "OpenFocuser, an Arduino based Focuser";
        private MainForm theForm;
        private Config _config = new Config();
        public int _position = 0;
        public SerialPort thePort;
        private int refCount = 0;


        //
        // Constructor - Must be public for COM registration!
        //
        public Focuser()
        {
            thePort = new SerialPort();
            thePort.BaudRate = 9600;
            if (_config.ComPort != "")
            {
                thePort.PortName = _config.ComPort;
                //thePort.Open();
            }
            thePort.NewLine = "\r\n";

            string s = this.ToString();

            //thePort.WriteLine("hello");
        }

        #region ASCOM Registration
        //
        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        private static void RegUnregASCOM(bool bRegister)
        {
            Helper.Profile P = new Helper.Profile();
            P.DeviceTypeV = "Focuser";					//  Requires Helper 5.0.3 or later
            if (bRegister)
                P.Register(s_csDriverID, s_csDriverDescription);
            else
                P.Unregister(s_csDriverID);
            try										// In case Helper becomes native .NET
            {
                Marshal.ReleaseComObject(P);
            }
            catch (Exception) { }
            P = null;
        }

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }
        #endregion

        //
        // PUBLIC COM INTERFACE IFocuser IMPLEMENTATION
        //

        #region IFocuser Members

        public bool Absolute
        {
            // TODO Replace this with your implementation
            get { return true; }
        }

        public void Halt()
        {
            SendCommand("h");
        }

        public bool IsMoving
        {
            // TODO Replace this with your implementation
            get { return GetIntegerResponse("i") == 1; }
        }

        public bool Link
        {
            // TODO Replace this with your implementation
            get { return thePort.IsOpen; }
            set 
            {
                if (value && !thePort.IsOpen)
                {
                    thePort.Open();
                    if (theForm == null)
                    {
                        theForm = new MainForm(this, ref _config);
                    }
                    theForm.Show();
                    theForm.EnableUpdateTimer();
                } else if (!value && thePort.IsOpen) {
                    thePort.Close();
                    theForm.Dispose();
                    theForm = null;
                }
            }
        }

        public int MaxIncrement
        {
            get { return _config.MaxIncrement; }
        }

        public int MaxStep
        {
            get { return _config.MaxIncrement;  }
        }

        public void Move(int val)
        {
            // TODO Replace this with your implementation
            SendCommand("m " + val.ToString());
        }

        public int Position
        {
            get { return _position; }
        }

        public void SetupDialog()
        {
            SetupDialogForm F = new SetupDialogForm(this, ref _config);
            F.ShowDialog();
            
        }

        public double StepSize
        {
            get { return 1; }
        }

        public bool TempComp
        {
            // TODO Replace this with your implementation
            get { return false; }
            set { throw new PropertyNotImplementedException("TempComp", true); }
        }

        public bool TempCompAvailable
        {
            get { return false; }
        }

        public double Temperature
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("Temperature", false); }
        }

        #endregion

        public string Version
        {
            get { return SendCommand("v"); }
        }

        #region communications

        public string SendCommand(string Cmd)
        {
            if (!Link)
            {
                throw new PropertyNotImplementedException("SendCommand: Not connected", false);
            }
            theForm.DbgMsg(String.Concat(">>>", Cmd));

            thePort.WriteLine(Cmd);

            string[] in_parts = Cmd.Split(' ');

            string resp = thePort.ReadLine();
            theForm.DbgMsg(String.Concat("<<<", resp));
            string[] out_parts = resp.Split(' ');
            if (out_parts[0] != (in_parts[0].ToUpper()))
            {
                theForm.DbgMsg("Could not parse response");
                return "";
            }
            else
            {
                if (out_parts.Length > 1)
                {
                    if (in_parts.Length > 1 && in_parts[1] != out_parts[1])
                    {
                        theForm.DbgMsg("Did not receive argument back");
                    }
                    return out_parts[1];
                }
                else
                    return "";
            }

        }

        public int GetPosition()
        {
            _position = GetIntegerResponse("p");
            return _position;
        }

        public int GetTargetPosition()
        {
            return GetIntegerResponse("a");
        }

        public int GetIntegerResponse(string cmd)
        {
            string resp = SendCommand(cmd);
            return Convert.ToInt16(resp);
        }

        #endregion
    }
}
