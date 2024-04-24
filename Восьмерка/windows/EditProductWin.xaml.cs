using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditProductWin.xaml
    /// </summary>
    public partial class EditProductWin : Window
    {
        Product prod;
        public EditProductWin(Product prod)
        {
            InitializeComponent();
            this.prod = prod;
            DataContext = prod;
            cb_Types.ItemsSource = MainWindow.db.ProductType.ToList();
            cb_Images.ItemsSource = MainWindow.db.Product.ToList();
        }

        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = prod;

                if (prod.ID != 0)
                {
                    if (MainWindow.db.ProductSale.Where(p => p.ProductID == prod.ID).ToList().Count != 0)
                        throw new Exception("Нельзя удалить продукт с продажами агентам");

                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);

                    if (result == MessageBoxResult.Yes)
                    {
                        var list1 = MainWindow.db.ProductMaterial.Where(p => p.ProductID == prod.ID).ToList();
                        var list2 = MainWindow.db.ProductCostHistory.Where(p => p.ProductID == prod.ID).ToList();

                        if (list1.Count != 0)
                        {
                            foreach(var el in list1)
                            {
                                MainWindow.db.ProductMaterial.Remove(el);
                            }
                            MainWindow.db.SaveChanges();
                        }
                        if(list2.Count != 0)
                        {
                            foreach(var el in list2)
                            {
                                MainWindow.db.ProductCostHistory.Remove(el);
                            }
                            MainWindow.db.SaveChanges();
                        }

                        MainWindow.db.Product.Remove(deleted);
                        MainWindow.db.SaveChanges();
                        MessageBox.Show("Запись удалена!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(tbCost.Text) < 0)
                    throw new Exception("Стоимость не должна быть отрицательной");

                if (prod.ID == 0)
                {
                    if (MainWindow.db.Product.Where(p => p.ArticleNumber == prod.ArticleNumber).ToList().Count == 0)
                        throw new Exception("Продукт с таким артикулом уже существует");

                    MainWindow.db.Product.Add(prod);
                }
                MainWindow.db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Files(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "";
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".jpg";
                dialog.Filter = "Images (*.png;*.jpg;*jpeg)|*.png;*.jpg;*jpeg";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    filename = dialog.FileName;
                    string fileTitle = System.IO.Path.GetFileName(filename);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "Pages/products" + filename;
                    if (!File.Exists(path))
                        File.Copy(filename, path, true);

                    prod.Image = "/products/" + filename;
                    MainWindow.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_Mats(object sender, RoutedEventArgs e)
        {
            MatsWin win = new MatsWin(prod);
            win.ShowDialog();
        }
    }
}
