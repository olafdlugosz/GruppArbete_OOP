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
        public Warehouse warehouse = new Warehouse();
        public List<Item> resultList = new List<Item>();
        private Cart _cart;

        public MainWindow() {
            InitializeComponent();
            ComboBox.Items.Add("Book");
            ComboBox.Items.Add("Film");
            _cart = new Cart(this);
            _cart.Hide();
        }
        private new string Title => NameTextBox.Text;
        private int Price {
            get {
                int.TryParse(PriceTextBox.Text, out int year); return year;
            }
        }
        private int Quantity {
            get {
                int.TryParse(QuantityTextBox.Text, out int year); return year;
            }
        }
        //TODO Handle exceptions of empty boxes.
        private string Type => ComboBox.SelectedItem.ToString();
        private void AddNewItemButton_Click(object sender, RoutedEventArgs e) {
            try {
                if (Type == "Book") {
                    Book book = new Book(Title, Price, Quantity, Type);
                    warehouse.WarehouseStorage.Add(book.Identifier, book);
                    warehouse.BookList.Add(book);
                    ItemListBox.Items.Add(book.Title);
                    resultList.Add(book as Item);
                }
                else if (Type == "Film") {
                    Film film = new Film(Title, Price, Quantity, Type);
                    warehouse.WarehouseStorage.Add(film.Identifier, film);
                    warehouse.FilmList.Add(film);
                    ItemListBox.Items.Add(film.Title);
                    resultList.Add(film as Item);
                }
            } catch (Exception) {
                MessageBox.Show("You must choose item in combobox!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            //Printling Function
            foreach (var item in warehouse.WarehouseStorage) {
                Console.WriteLine(item.Value.ToString());
            }
            ClearTextBoxes();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            try {
                warehouse.SaveData(warehouse.WarehouseStorage);
                if (warehouse.BookList.Count != 0) { warehouse.SaveData(warehouse.BookList); };
                if (warehouse.FilmList.Count != 0) { warehouse.SaveData(warehouse.FilmList); };
            } catch {
                MessageBox.Show("A problem accured while saving data!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            try {
                warehouse.LoadData();
            } catch {
                MessageBox.Show("A problem accured while loading data!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            if (ComboBox.SelectedItem == null)
                return;
            else {
                ClearListBox();
                resultList = warehouse.PerformSearch(warehouse.CheckObjectType(ComboBox),
                    ComboBox.SelectedItem, NameTextBox.Text, PriceTextBox.Text,
                    QuantityTextBox.Text, GuidTextBox.Text);

                resultList.ForEach(i => ItemListBox.Items.Add(i));
            }
        }
        private void ClearTextBoxes() {
            ComboBox.SelectedItem = null;
            NameTextBox.Clear();
            PriceTextBox.Clear();
            QuantityTextBox.Clear();
            GuidTextBox.Clear();
        }

        private void ClearListBox() {
            ItemListBox.Items.Clear();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {
            Item itemToRemove = null;

            if (ItemListBox.SelectedIndex >= 0) {
                itemToRemove = resultList[ItemListBox.SelectedIndex];
                resultList.RemoveAt(ItemListBox.SelectedIndex);
                ItemListBox.Items.RemoveAt(ItemListBox.SelectedIndex);
            }

            warehouse.RemoveItems(itemToRemove);
        }

        private void ViewCartButton_Click(object sender, RoutedEventArgs e) {
            ClearTextBoxes();
            ClearListBox();
            _cart.Show();
            this.Hide();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e) {
            
            if (ItemListBox.SelectedIndex >= 0 && resultList[ItemListBox.SelectedIndex].Quantity > 0) {
                Item itemToAddToCart = null;
                resultList[ItemListBox.SelectedIndex].Quantity--;

                if (resultList[ItemListBox.SelectedIndex].Type == "Book") {
                    itemToAddToCart = new Book(resultList[ItemListBox.SelectedIndex].Title,
                    resultList[ItemListBox.SelectedIndex].Price,
                    resultList[ItemListBox.SelectedIndex].Quantity,
                    resultList[ItemListBox.SelectedIndex].Type,
                    resultList[ItemListBox.SelectedIndex].Identifier);
                }

                else if (resultList[ItemListBox.SelectedIndex].Type == "Film") {
                    itemToAddToCart = new Film(resultList[ItemListBox.SelectedIndex].Title,
                    resultList[ItemListBox.SelectedIndex].Price,
                    resultList[ItemListBox.SelectedIndex].Quantity,
                    resultList[ItemListBox.SelectedIndex].Type,
                    resultList[ItemListBox.SelectedIndex].Identifier);
                }

                _cart.AddToCart(itemToAddToCart);
                ItemListBox.Items.Clear();

                foreach (var item in resultList) {
                    ItemListBox.Items.Add(item);
                }
            }
            else
                return;
        }
    }
}
