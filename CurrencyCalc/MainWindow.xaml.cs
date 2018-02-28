using CurrencyCalc.Controllers;
using CurrencyCalc.Models;
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

namespace CurrencyCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitComboBoxes();
        }

        private Calculator Calc = new Calculator();

        #region Handlers
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = (int)OutCurrencyComboBox.SelectedValue;
            int j = (int)InputCurrencyComboBox.SelectedValue;
            if (Double.TryParse(inputCurrencyAmountTextBox.Text, out Double inputCurrencyAmount))
            {
                outCurrencyAmountTextBox.Text = Calc.Excange(i, j, inputCurrencyAmount).ToString();
            }
            else
            {
                inputCurrencyAmountTextBox.Text = "";
                outCurrencyAmountTextBox.Text = "";
            }
        }
        #endregion

        #region Private methods

        private void InitComboBoxes()
        {
            OutCurrencyComboBox.SelectedValuePath = "Key";
            OutCurrencyComboBox.DisplayMemberPath = "Value";
            InputCurrencyComboBox.SelectedValuePath = "Key";
            InputCurrencyComboBox.DisplayMemberPath = "Value";
            foreach (var item in Calc.CurrencyList)
            {
                this.OutCurrencyComboBox.Items.Add(new KeyValuePair<int, string>(item.Id, GetComboBoxItemString(item)));
                this.InputCurrencyComboBox.Items.Add(new KeyValuePair<int, string>(item.Id, GetComboBoxItemString(item)));
            }

        }

        private string GetComboBoxItemString(Currency cur)
        {
            return string.Format("({0}) {1}", cur.ShortName, cur.Name);
        }

        #endregion
    }
}
