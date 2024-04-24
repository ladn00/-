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

namespace Восьмерка.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        Frame frame1;
        public Menu(Frame frame1)
        {
            InitializeComponent();
            this.frame1 = frame1;
        }

        private void products_CLick(object sender, RoutedEventArgs e)
        {
            frame1.NavigationService.Navigate(new ProductsPage());
        }
    }
}
