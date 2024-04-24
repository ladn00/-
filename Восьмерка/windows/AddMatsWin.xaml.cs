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
    /// Логика взаимодействия для AddMatsWin.xaml
    /// </summary>
    public partial class AddMatsWin : Window
    {
        Material mat;
        public AddMatsWin(Material mat)
        {
            InitializeComponent();
            this.mat = mat;
            DataContext = mat;
            cb_Types.ItemsSource = MainWindow.db.MaterialType.ToList();
            cb_Images.ItemsSource = MainWindow.db.Material.ToList();
        }

        private void bt_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleted = mat;

                if (mat.ID != 0)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить запись?",
                        "Внимание!",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.db.Material.Remove(deleted);
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
                if (mat.ID == 0)
                {
                    MainWindow.db.Material.Add(mat);
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

                    mat.Image = "/products/" + filename;
                    MainWindow.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
