using System;
using System.Windows;
using System.Windows.Controls;

namespace MotherboardTester
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MultimeterButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно мультиметра
            MultimeterWindow multimeterWindow = new MultimeterWindow();
            multimeterWindow.Owner = this;
            multimeterWindow.ShowDialog();
        }

        private void OscilloscopeButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно осциллографа
            OscilloscopeWindow oscilloscopeWindow = new OscilloscopeWindow();
            oscilloscopeWindow.Owner = this;
            oscilloscopeWindow.ShowDialog();
        }
    }

}
