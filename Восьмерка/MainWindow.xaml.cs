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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Восьмерка
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static УП_3_Курс_2_семестрEntities db = new УП_3_Курс_2_семестрEntities();

        public MainWindow()
        {
            InitializeComponent();
            frame1.NavigationService.Navigate(new Pages.Menu(frame1));
        }

        private void getBack_Click(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.GoBack();
        }
    }
}
