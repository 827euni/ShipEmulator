using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShipEmulator
{
    public partial class rpmGauge : UserControl
    {

        int mMinRPM = 500;
        int mMaxRPM = 1500;
        int rpm;
        public rpmGauge()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
