﻿using System;
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

namespace GruppArbete_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ComboBox.Da = Enum.GetValues(typeof(TYPE))
        }
        private string Title => NameTextBox.Text;
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
        private TYPE Type => Co
        private void AddNewItemButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
