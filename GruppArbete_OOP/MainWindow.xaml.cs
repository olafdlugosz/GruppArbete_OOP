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
using System.Diagnostics;
using System.Collections;

namespace GruppArbete_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Warehouse _warehouse = new Warehouse();
        private List<Item> _resultList;
        private Cart _cart;

        public MainWindow()
        {
            InitializeComponent();
            ComboBox.Items.Add("Book");
            ComboBox.Items.Add("Film");
            _cart = new Cart(this);
            _cart.Hide();
        }
        private new string Title => NameTextBox.Text;
        private int Price
        {
            get
            {
                int.TryParse(PriceTextBox.Text, out int year); return year;
            }
        }
        private int Quantity
        {
            get
            {
                int.TryParse(QuantityTextBox.Text, out int year); return year;
            }
        }
        //TODO Handle exceptions of empty boxes.
        private string Type => ComboBox.SelectedItem.ToString(); 
        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (Type == "Book")
            {
                Book book = new Book(Title, Price, Quantity, Type);
                _warehouse.WarehouseStorage.Add(book.Identifier, book);
                _warehouse.BookList.Add(book);
                ItemListBox.Items.Add(book.Title);
            }
            if (Type == "Film")
            {
                Film film = new Film(Title, Price, Quantity, Type);
                _warehouse.WarehouseStorage.Add(film.Identifier, film);
                _warehouse.FilmList.Add(film);
                ItemListBox.Items.Add(film.Title);
            }
            //Printling Function
            foreach (var item in _warehouse.WarehouseStorage)
            {
                Console.WriteLine(item.Value.ToString());
            }

            ClearTextBoxes();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            _warehouse.SaveData(_warehouse.WarehouseStorage);
            if (_warehouse.BookList.Count != 0) { _warehouse.SaveData(_warehouse.BookList); };
            if (_warehouse.FilmList.Count != 0) { _warehouse.SaveData(_warehouse.FilmList); };
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            _warehouse.LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox.SelectedItem == null)
                return;
            else
            {
                ClearListBox();
                _resultList = _warehouse.PerformSearch(_warehouse.CheckObjectType(ComboBox), 
                    ComboBox.SelectedItem, NameTextBox.Text, PriceTextBox.Text, 
                    QuantityTextBox.Text, GuidTextBox.Text);

                foreach (var item in _resultList)
                {
                    ItemListBox.Items.Add(item.Title);
                }
            }
        }


        private void ClearTextBoxes()
        {
            ComboBox.SelectedItem = null;
            NameTextBox.Clear();
            PriceTextBox.Clear();
            QuantityTextBox.Clear();
            GuidTextBox.Clear();
        }

        private void ClearListBox()
        {
            ItemListBox.Items.Clear();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Item itemToRemove = null;

            if (ItemListBox.SelectedIndex >= 0)
            {
                itemToRemove = _resultList[ItemListBox.SelectedIndex];
                _resultList.RemoveAt(ItemListBox.SelectedIndex);
                ItemListBox.Items.RemoveAt(ItemListBox.SelectedIndex);
            }

            _warehouse.RemoveItems(itemToRemove);
        }

        private void ViewCartButton_Click(object sender, RoutedEventArgs e)
        {
            _cart.Show();
            this.Hide();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemListBox.SelectedIndex >= 0)
            {
                Item itemToAddToCart = _resultList[ItemListBox.SelectedIndex];
                _cart.AddToCart(itemToAddToCart);
            }
        }
    }
}
