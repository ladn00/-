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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        IEnumerable<Product> currentList = MainWindow.db.Product.ToList();
        private int _currentPage = 1;
        private int _count = 20;
        private int _maxPages;
        public ProductsPage()
        {
            InitializeComponent();
            
            var prodTypes = new List<string>();
            prodTypes.Add("Все типы");
            foreach(var el in MainWindow.db.ProductType.ToList())
            {
                prodTypes.Add(el.Title);
            }
            cbProdType.ItemsSource = prodTypes;
            cbProdType.SelectedIndex = 0;
            currentList = MainWindow.db.Product.ToList();
            Refresh();
        }

        private void Refresh()
        {
            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var newList = currentList.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "of " + _maxPages;
            LblInfo.Content = $"Всего {currentList.Count()} записи, по {_count} записей на одной странице";

            lw1.ItemsSource = newList;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button;
            var selected = editButton.DataContext as Product;
            windows.EditProductWin tourEdit = new windows.EditProductWin(selected);
            tourEdit.ShowDialog();
            currentList = MainWindow.db.Product.ToList();
            cbTypes_Changed(sender, null);
            cbSort_Changed(sender, null);
            Refresh();
            tbChanged(sender, null);
        }

        private void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            Refresh();
        }

        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 1) _currentPage = 1;
            else
                _currentPage--;
            Refresh();
        }

        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _maxPages) _currentPage = _maxPages;
            else
                _currentPage++;
            Refresh();
        }

        private void GoToLastPage(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPages;
            Refresh();
        }

        private void tbChanged(object sender, TextChangedEventArgs e)
        {
            var x = currentList;
            string seachText = tbSearch.Text;
            if (!string.IsNullOrEmpty(seachText))
            {
                x = currentList.Where(p => p.Title.ToLower().Contains(seachText.ToLower()) || p.Description.ToLower().Contains(seachText.ToLower())).ToList();
            }
            _maxPages = (int)Math.Ceiling(currentList.Count() * 1.0 / _count);

            var newList = x.Skip((_currentPage - 1) * _count).Take(_count).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "of " + _maxPages;
            LblInfo.Content = $"Всего {x.Count()} записи, по {_count} записей на одной странице";

            lw1.ItemsSource = newList;
        }

        private void cbTypes_Changed(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = cbProdType.SelectedItem as string;
            if (selectedType != "Все типы")
            {
                currentList = MainWindow.db.Product.Where(n => n.ProductType.Title == selectedType);
            }
            else
            {
                currentList = MainWindow.db.Product.ToList();
            }
            cbSort_Changed(sender, null);
            Refresh();
            tbChanged(sender, null);
        }

        private void cbSort_Changed(object sender, SelectionChangedEventArgs e)
        {
            int selected = cbSort.SelectedIndex;
            switch (selected)
            {
                case 0:
                    currentList = currentList.OrderBy(p => p.Title);
                    break;
                case 1:
                    currentList = currentList.OrderByDescending(p => p.Title);
                    break;
                case 2:
                    currentList = currentList.OrderByDescending(p => p.ProductionWorkshopNumber);
                    break;
                case 3:
                    currentList = currentList.OrderBy(p => p.ProductionWorkshopNumber);
                    break;
                case 4:
                    currentList = currentList.OrderByDescending(p => p.MinCostForAgent);
                    break;
                case 5:
                    currentList = currentList.OrderBy(p => p.MinCostForAgent);
                    break;
            }
            Refresh();
        }

        public static decimal newCost = 0;
        private void btChangeOn_Click(object sender, RoutedEventArgs e)
        {
            List<Product> selected = new List<Product>();
            foreach(Product el in lw1.SelectedItems)
            {
                selected.Add(el);
            }

            windows.ChangeOnWin win = new windows.ChangeOnWin();
            win.ShowDialog();
            foreach (var el in selected)
                el.MinCostForAgent += newCost;
            Refresh();
            MainWindow.db.SaveChanges();
            tbChanged(sender, null);
            btChangeOn.Visibility = Visibility.Hidden;
        }

        private void lw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lw1.SelectedItems.Count < 1)
                btChangeOn.Visibility = Visibility.Hidden;
            else
                btChangeOn.Visibility = Visibility.Visible;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Product prod = new Product();
            prod.ID = 0;
            windows.EditProductWin win = new windows.EditProductWin(prod);
            win.ShowDialog();
            cbTypes_Changed(sender, null);
            cbSort_Changed(sender, null);
            Refresh();
            tbChanged(sender, null);

        }
    }
}
