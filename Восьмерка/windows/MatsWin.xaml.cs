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
    /// Логика взаимодействия для MatsWin.xaml
    /// </summary>
    public partial class MatsWin : Window
    {
        Product prod;
        public MatsWin(Product prod)
        {
            InitializeComponent();
            this.prod = prod;
            lw1.ItemsSource = MainWindow.db.ProductMaterial.Where(p => p.ProductID == prod.ID).ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as ProductMaterial;
            var sel = MainWindow.db.Material.Where(p => p.ID == selected.MaterialID).ToList()[0];
            windows.AddMatsWin tourEdit = new windows.AddMatsWin(sel);
            tourEdit.ShowDialog();
            lw1.ItemsSource = MainWindow.db.ProductMaterial.Where(p => p.ProductID == prod.ID).ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ProductMaterial prodM = new ProductMaterial();
            AddMatsFromList win = new AddMatsFromList(prod, prodM);
            win.ShowDialog();
            lw1.ItemsSource = MainWindow.db.ProductMaterial.Where(p => p.ProductID == prod.ID).ToList();
        }
    }
}
