using HW_WPF_Image_from_Pixabay.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HW_WPF_Image_from_Pixabay
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainVM mainVM = new MainVM();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow() { DataContext = mainVM }.Show();
        }
    }
}
