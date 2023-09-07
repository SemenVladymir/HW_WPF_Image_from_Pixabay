using System;
using System.Windows;

namespace HW_WPF_Image_from_Pixabay
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application.Current.Resources = Application.LoadComponent(new Uri("View\\DayStyles.xaml", UriKind.Relative)) as ResourceDictionary;
            InitializeComponent();
        }

        private void LightSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (LightSwitch.IsChecked == false)
            {
                ResourceDictionary res = Application.LoadComponent(new Uri("View\\DayStyles.xaml", UriKind.Relative)) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(res);
            }
            else
            {
                ResourceDictionary res = Application.LoadComponent(new Uri("View\\NightStyles.xaml", UriKind.Relative)) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(res);
            }
        }
    }
}
