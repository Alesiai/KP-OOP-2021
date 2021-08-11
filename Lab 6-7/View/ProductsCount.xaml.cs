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
using System.Text.RegularExpressions;

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для ProductsCount.xaml
    /// </summary>
    public partial class ProductsCount : Window
    {
        public ProductsCount()
        {
            InitializeComponent();
        }

        public static int ProdCount;

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CountTextBox.Text, @"[0-9]"))
            {
                MessageBox.Show("Ошибка ввода, вводите пожалуйста цифры");
            }
            else
            {
                ProdCount = Convert.ToInt32(CountTextBox.Text);
                this.DialogResult = true;
            }

        }

        private void CountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (input != "")
            {
                if (!Regex.IsMatch(input, @"[0-9]"))
                {
                    MessageBox.Show("Ошибка ввода, вводите пожалуйста цифры");
                }
            }
        }
    }
}
