using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASCOM.OpenFocuser
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        public void AddText(string theText) {
            if (this.IsDisposed)
                return;
            textBox.AppendText(theText);
            textBox.AppendText("\r\n");
        }
    }
}
