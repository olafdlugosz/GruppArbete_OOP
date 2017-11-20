using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            //TODO You can use LINQ here too. - DONE!
            var itemToAdd = _orderList.FirstOrDefault(i => i.Identifier == item.Identifier);

            if (itemToAdd == null)
            {
                item.Quantity = 1;
                _orderList.Add(item);
                CartListBox.Items.Add(item);
            }
            else
            {
                itemToAdd.Quantity++;
                CartListBox.Items.Remove(itemToAdd);
                CartListBox.Items.Add(itemToAdd);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDlg = new PrintDialog();
                bool? print = printDlg.ShowDialog();
                if (print == true)
                {
                    printDlg.PrintVisual(CartListBox, "Printing Order");
                    CartListBox.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("There was a problem printing your orderlist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_orderList.Count == 0)
                    return;
                else
                {
                    SaveFileDialog saveList = new SaveFileDialog
                    {
                        Filter = " TXT-fil| *.txt",
                        Title = "Spara Plocklistan",
                        FileName = $"Plocklista - {DateTime.Now.ToString("yyyy.MM.dd kl. HH.mm")}"
                    };
                    if (saveList.ShowDialog() == true)
                    {
                        StreamWriter writer = new StreamWriter(saveList.OpenFile());
                        writer.WriteLine($"Plocklistan skapad den {DateTime.Now.ToString("yyyy.MM.dd kl. HH:mm")}", false);
                        writer.WriteLine("\n\n");
                        writer.WriteLine($"Varor att Plocka:");

                        for (int i = 0; i < _orderList.Count; i++)
                        {
                            writer.WriteLine(_orderList[i].ToString());
                        }

                        writer.Dispose();
                        writer.Close();
                        Environment.Exit(0);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There was a problem saving your orderlist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {

            if (CartListBox.SelectedIndex >= 0)
            {
                if (_orderList[CartListBox.SelectedIndex].Quantity > 1)
                {
                    AdjustItemsQuantity(_orderList[CartListBox.SelectedIndex]);
                    _orderList[CartListBox.SelectedIndex].Quantity--;
                    CartListBox.Items.Clear();

                    foreach (var item in _orderList)
                    {
                        CartListBox.Items.Add(item);
                    }
                }
                else
                {
                    AdjustItemsQuantity(_orderList[CartListBox.SelectedIndex]);
                    _orderList.RemoveAt(CartListBox.SelectedIndex);
                    CartListBox.Items.RemoveAt(CartListBox.SelectedIndex);
                }
            }
        }

        private void AdjustItemsQuantity(Item item)
        {
            _mainWindow.warehouse.WarehouseStorage[item.Identifier].Quantity++;
        }

        private void BackToWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Show();
            this.Hide();
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            CartListBox.Items.Clear();

            foreach (var item in _orderList)
            {
                while (item.Quantity > 0)
                {
                    AdjustItemsQuantity(item);
                    item.Quantity--;
                }
            }
            _orderList.Clear();
        }
    }
}
