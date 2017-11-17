using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            item.Quantity = 0;

            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i].Identifier == item.Identifier)
                {
                    _orderList[i].Quantity++;
                    CartListBox.Items.Remove(_orderList[i]);
                    CartListBox.Items.Add(_orderList[i]);
                    return;
                }
            }

            item.Quantity++;
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


        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {

            if (CartListBox.SelectedIndex >= 0)
            {
                if (_orderList[CartListBox.SelectedIndex].Quantity > 1)
                {
                    AddItemQuantity(_orderList[CartListBox.SelectedIndex]);
                    _orderList[CartListBox.SelectedIndex].Quantity--;
                    CartListBox.Items.Clear();

                    foreach (var item in _orderList)
                    {
                        CartListBox.Items.Add(item);
                    }
                }
                else
                {
                    AddItemQuantity(_orderList[CartListBox.SelectedIndex]);
                    _orderList.RemoveAt(CartListBox.SelectedIndex);
                    CartListBox.Items.RemoveAt(CartListBox.SelectedIndex);
                }
            }
        }

        private void AddItemQuantity(Item item)
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
                    AddItemQuantity(item);
                    item.Quantity--;
                }
            }
            _orderList.Clear();
        }
    }
}
