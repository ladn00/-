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

namespace Восьмерка.windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeOnWin.xaml
    /// </summary>
    public partial class ChangeOnWin : Window
    {
        public ChangeOnWin()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Pages.ProductsPage.newCost = Convert.ToDecimal(tb1.Text);
            this.Close();
        }
    }
}
