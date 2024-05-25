using System;
using System.Windows;
using System.Windows.Controls;

namespace MotherboardTester
{
    public partial class MultimeterWindow : Window
    {
        public MultimeterWindow()
        {
            InitializeComponent();
        }


        public void MeasureVoltageAndResist(string v, string r)
        {
            vValue.Text = v;
            rValue.Text = r;
        }


    }
}
