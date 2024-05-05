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

        private void MeasureVoltageButton_Click(object sender, RoutedEventArgs e)
        {
            double voltage = MeasureVoltage();
            MessageBox.Show($"Измеренное напряжение: {voltage} В");
        }

        private double MeasureVoltage()
        {
            //код для измерения напряжения

            // Возврат примерного значения напряжения (здесь это случайное число)
            Random random = new Random();
            return random.NextDouble() * 5; // Генерируем случайное напряжение от 0 до 5 В
        }
    }
}
