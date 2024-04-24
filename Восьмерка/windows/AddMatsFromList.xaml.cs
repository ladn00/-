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
    /// Логика взаимодействия для AddMatsFromList.xaml
    /// </summary>
    public partial class AddMatsFromList : Window
    {
        Product prod;
        ProductMaterial prodM;
        public AddMatsFromList(Product prod, ProductMaterial prodM)
        {
            InitializeComponent();
            this.prod = prod;
            this.prodM = prodM;
            cb_Prods.ItemsSource = mats;

        }

        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                prodM.ProductID = prod.ID;
                prodM.MaterialID = (cb_Prods.SelectedItem as Material).ID;
                prodM.Count = Convert.ToDouble(tb_Count.Text);
                MainWindow.db.ProductMaterial.Add(prodM);
                MainWindow.db.SaveChanges();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        IEnumerable<Material> mats = MainWindow.db.Material.ToList();
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var x = mats;
            string seachText = tbSearch.Text;
            if (!string.IsNullOrEmpty(seachText))
            {
                x = mats.Where(p => p.Title.ToLower().Contains(seachText.ToLower()) || p.Description.ToLower().Contains(seachText.ToLower())).ToList();
            }

            cb_Prods.ItemsSource = x;
        }
    }
}
