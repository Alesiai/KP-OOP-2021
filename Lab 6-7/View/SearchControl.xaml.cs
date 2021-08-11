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

namespace Lab_6_7.View
{
    /// <summary>
    /// Логика взаимодействия для SearcControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(SearchControl));



           public string SearchText
        {
            get => GetValue(SearchTextProperty) as string;
            set => SetValue(SearchTextProperty, value);
        }

        public event Action<string> SearchTextChanged;
        public SearchControl()
        {
            InitializeComponent();
        }

        private void SearchParam_TextChanged(object sender, TextChangedEventArgs e) => SearchTextChanged?.Invoke(searchTB.Text);
       
    }
}
