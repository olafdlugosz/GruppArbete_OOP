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

namespace GruppArbete_OOP
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private List<Item> _orderList = new List<Item>();
        private MainWindow _mainWindow;

        public Cart(MainWindow mainWindow)
        {
            this._mainWindow = mainWindow;
            InitializeComponent();
        }

        public void AddToCart(Item item)
        {
            item.Quantity = 0;

            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i].Identifier == item.Identifier)
                {
                    _orderList[i].ChangeQuantity();
                    CartListBox.Items.Remove(_orderList[i]);
                    CartListBox.Items.Add(_orderList[i]);
                    return;
                }
            }

            item.ChangeQuantity();
            _orderList.Add(item);
            CartListBox.Items.Add(item);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.ShowDialog();
            printDlg.PrintVisual(CartListBox, "Cart Printing.");
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartListBox.SelectedIndex >= 0)
            {
                if (_orderList[CartListBox.SelectedIndex].Quantity > 0)
                {
                    _orderList[CartListBox.SelectedIndex].Quantity--;
                }
                else
                {
                    _orderList.RemoveAt(CartListBox.SelectedIndex);
                    CartListBox.Items.RemoveAt(CartListBox.SelectedIndex);
                }
            }
        }

        private void BackToWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Show();
            this.Hide();
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartListBox.Items.Clear();
            _orderList.Clear();
        }
    }
}
