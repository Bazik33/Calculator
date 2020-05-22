using System;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    public partial class MainWindow : Window
    {

        double lastValue;
        string operation = "";
        bool CEClicked = false;

        public MainWindow() {
            InitializeComponent();
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            CEClicked = true; 
        }

        private void Equal(object sender, RoutedEventArgs e)
        {

            double currentValue;
            double equationValue = 0;
            string printValue;

            if (CEClicked){
                Double.TryParse(textBox.Text, out currentValue);

                switch (operation)
                {
                    case "+":
                        equationValue = lastValue + currentValue;
                        break;

                    case "-":
                        equationValue = lastValue - currentValue;
                        break;

                    case "*":
                        equationValue = lastValue * currentValue;
                        break;

                    case "/":
                        equationValue = lastValue / currentValue;
                        break;

                    case "^":
                        equationValue = 1; 
                        for (int a = 0; a < currentValue; a++) equationValue = equationValue * lastValue; 
                        break;

                    case "2":
                        equationValue = Math.Sqrt(lastValue);
                        break;

                    case "x":
                        equationValue = 1 / lastValue;
                    break;

                    case "%":
                        equationValue = lastValue / 100; 
                        break;

                    default:
                        equationValue = currentValue;
                        break;
                }

                lastValue = 0;
                currentValue = 0;
                operation = "";

                printValue = equationValue.ToString();
                equationValue = 0;

                textBox.Clear();
                textBox.AppendText(printValue);

                printValue = "";
            } else MessageBox.Show("Start the Application using CE button!");

        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (CEClicked)
            {
                string theObject;
                theObject = sender.ToString();
                theObject = theObject.Substring(theObject.Length - 1);

                if (operation == "")
                {
                    Double.TryParse(textBox.Text, out lastValue);
                    textBox.Clear();
                    operation = theObject; 
                }
                else operation = theObject;

                if (operation == "2" || operation == "x" || operation == "%") Equal(null, null);

            } else MessageBox.Show("Start the Application using CE button!");
        } 

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            if (CEClicked)
            {
                string theObject;
                theObject = sender.ToString();
                theObject = theObject.Substring(theObject.Length - 1);

                textBox.AppendText(theObject);
            } else MessageBox.Show("Start the Application using CE button!");
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A simple real-number .NET Framework Calculator. Version 1.2. \n(C) 2019 bazik33@hotmail.com", "About Calculator .NET", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.+*/^%-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Regex regex = new Regex("[^0-9.+*/^%-]+");
            if (regex.IsMatch(e.Key.ToString()))
            {
                string ass = e.Key.ToString();  ass = ass.Substring(ass.Length - 1);
                textBox.AppendText(ass);
            }
        }

    }
}
