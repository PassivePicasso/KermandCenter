using System;
using System.Windows;

namespace KermandCenter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class KermandCenterView : Window
    {
        public KermandCenterView()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dc = DataContext as IDisposable;
            dc?.Dispose();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void Normalize(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Normal)
                WindowState = WindowState.Normal;
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }


    }
}
