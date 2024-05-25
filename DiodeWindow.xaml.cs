using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MotherboardTester
{
    /// <summary>
    /// Логика взаимодействия для DiodeWindow.xaml
    /// </summary>
    public partial class DiodeWindow : Window
    {
        public DiodeWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;  
        }


        public void DiodeActivate(string[] val)
        {
            foreach(string s in val)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(500); // Пауза между миганиями в миллисекундах
                    ChangeCircleColor(s == "1");
                });
            }
        }

        private void ChangeCircleColor(bool isVisible)
        {
            SolidColorBrush brush = isVisible ? Brushes.LightGreen : Brushes.Green;
            Dispatcher.Invoke(() => Diode.Fill = brush);
        }
    }
}
