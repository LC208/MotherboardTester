using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MotherboardTester
{
    /// <summary>
    /// Логика взаимодействия для ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public ChoiceWindow()
        {
            InitializeComponent();
        }

        private void ChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            Button source = (Button)e.Source;
            var answers = new Dictionary<int, string>()
            {
                { 1, "gndV3Multimetr"},
                { 2, "gndV5Multimetr"},
                { 3, "gndV12Multimetr"},
                { 4, "gndQuartzMultimetr"},
                { 5, "gndUSBMultimetr"},
                { 6, "gndKTSMultimetr"},
                { 7, "pcieTester"},
                { 8, "biosDiode"}
            };
            var desc = new Dictionary<int, string>()
            {
                { 1, "КЗ на 3.3В"},
                { 2, "КЗ на 5В"},
                { 3, "КЗ на 12В"},
                { 4, "Проблема в часовом кварце"},
                { 5, "Ю.М. неисправен с большой долей вероятности"},
                { 6, "Разряжена батарейка 3V"},
                { 7, "Разрыв на линиях PCIE"},
                { 8, "Неисправна микросхема BIOS или повреждена(стёрта) прошивка"}
            };
            Random rng = new Random();
            for (int i = 1; i <= answers.Count; i++) 
            {
                for (int j = 1; j<= answers.Count;j++)
                if(rng.NextDouble() >= 0.5)
                {
                    (answers[i], answers[j]) = (answers[j], answers[i]);
                    (desc[i], desc[j]) = (desc[j], desc[i]);
                }
            }
            int code = int.Parse( ((String)source.Content).Replace("Поломка ", ""));
            source.Content = desc[code].ToString();
            source.IsHitTestVisible = false;
            MainWindow window = new MainWindow(this,desc, code, answers[code]);
            window.Show();
            //Close();
        }
    }
}
