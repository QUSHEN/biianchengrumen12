using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using MahApps.Metro.Controls;
namespace Calculator2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string Number1="", Number2="";
        List<string> expressions = new List<string>();
        Operator flag = Operator.none;
        enum Operator { add, sub, mul, div, none};
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_number_Click(object sender, RoutedEventArgs e)
        {
            Numberinput(Convert.ToString((sender as Button).Content));
        }
        private void Button_flag_Click(object sender, RoutedEventArgs e)
        {
            Flaginput(Convert.ToString((sender as Button).Content));
        }
        private void Button_DEL_Click(object sender, RoutedEventArgs e)
        {
            if(flag == Operator.none)
            {
                if(Number1.Length >0)
                {
                    Number1 = Number1.Remove(Number1.Length - 1);
                    label1.Content = Number1;
                }
            }
            else
            {
                if (Number2.Length > 0)
                {
                    Number2 = Number2.Remove(Number2.Length - 1);
                    label1.Content = Number2;
                }
            }

        }
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Number1 = "";
            Number2 = "";
            flag = Operator.none;
            label1.Content = "0";
        }
        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            FileStream resultfile = new FileStream("result.txt", FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach(string a in expressions)
            {
                streamWriter.WriteLine(a);
            }
            streamWriter.Close();
        }
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {
            if (Number1 == "" || Number2 == "")
            {
                Number1 = "";
                Number2 = "";
                flag = Operator.none;
                return;
            }

            switch(flag)
            {
                case Operator.add:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) + Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "+" + Number2 + "=" + label1.Content);
                    break;
                case Operator.sub:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) - Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "-" + Number2 + "=" + label1.Content);
                    break;
                case Operator.mul:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "*" + Number2 + "=" + label1.Content);
                    break;
                case Operator.div:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) / Convert.ToDouble(Number2));
                    expressions.Add(Number1 + "/" + Number2 + "=" + label1.Content);
                    break;
            }
            Number1 = "";
            Number2 = "";
            flag = Operator.none;
        }
        private void Numberinput(string content)
        {
            if (flag == Operator.none)
            {
                if(Number1.Contains(".") && content == ".")
                {
                    return;
                }
                Number1 = Number1 + content;
                label1.Content = Number1;
            }
            else
            {
                if (Number2.Contains(".") && content == ".")
                {
                    return;
                }
                Number2 = Number2 + content;
                label1.Content = Number2;
            }
        }
        private void Flaginput(string content)
        {
            switch(content)
            {
                case "+":
                    flag = Operator.add;
                    break;
                case "-":
                    flag = Operator.sub;
                    break;
                case "*":
                    flag = Operator.mul;
                    break;
                case "/":
                    flag = Operator.div;
                    break;
            }
        }
    }
}
