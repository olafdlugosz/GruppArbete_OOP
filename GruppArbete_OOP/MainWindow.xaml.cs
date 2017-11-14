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

namespace GruppArbete_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Warehouse warehouse = new Warehouse();
        
        public MainWindow()
        {
            InitializeComponent();
            ComboBox.Items.Add("Book");
            ComboBox.Items.Add("Film");           
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
        private string Type => ComboBox.SelectedItem.ToString();
        private void AddNewItemButton_Click(object sender, RoutedEventArgs e) {
            if (Type == "Book") {
                Book book = new Book(Title, Price, Quantity, Type);
                warehouse.WarehouseStorage.Add(book.Identifier, book);
                warehouse.BookList.Add(book);
                ItemListBox.Items.Add(book.Title);
            }
            if(Type == "Film") {
                Film film = new Film(Title, Price, Quantity, Type);
                warehouse.WarehouseStorage.Add(film.Identifier, film);
                warehouse.FilmList.Add(film);
                ItemListBox.Items.Add(film.Title);
            }
            //Printling Function
            foreach (var item in warehouse.WarehouseStorage) {
                Console.WriteLine(item.Value.ToString());
            }
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            warehouse.SaveData(warehouse.WarehouseStorage);
            if(warehouse.BookList.Count != 0) { warehouse.SaveData(warehouse.BookList); };
            if (warehouse.FilmList.Count != 0) { warehouse.SaveData(warehouse.FilmList); };

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            warehouse.LoadData();
        }
    }
}
