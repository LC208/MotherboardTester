using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MotherboardTester
{
    public partial class OscilloscopeWindow : Window
    {
        public OscilloscopeWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartSampling();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopSampling();
        }

        private void StartSampling()
        {
            // код для запуска сбора данных с осциллографа
            // Здесь можно использовать таймер или цикл для симуляции данных

            // Пример симуляции данных (здесь просто рисуем случайные точки)
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                double voltage = random.NextDouble() * 5; // Генерируем случайное напряжение от 0 до 5 В
                DrawPoint(i, voltage);
            }
        }

        private void StopSampling()
        {
            // код для остановки сбора данных с осциллографа
            // Здесь можно остановить таймер или цикл
        }

        private void DrawPoint(int index, double voltage)
        {
            // Создаем эллипс для отображения точки на осциллографе
            Ellipse point = new Ellipse();
            point.Width = 3;
            point.Height = 3;
            point.Fill = Brushes.Blue; // Цвет точки

            // Располагаем точку на холсте осциллографа
            Canvas.SetLeft(point, index);
            Canvas.SetBottom(point, voltage * 50); // Масштабируем напряжение для отображения на холсте

            // Добавляем точку на холст
            OscilloscopeCanvas.Children.Add(point);
        }
    }
}
