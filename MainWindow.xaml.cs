using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class NewItem
    {
        public string NI_Pesel { get; set; }
        public string NI_Name { get; set; }
        public string NI_SecName { get; set; }
        public string NI_LastName { get; set; }
        public string NI_Date { get; set; }
        public string NI_Phone { get; set; }
        public string NI_Adres { get; set; }
        public string NI_Local { get; set; }
        public string NI_Post { get; set; }

        public NewItem(string n_Pesel, string n_Name, string? n_SecName, string n_LastName, string n_Date, string? n_Phone, string n_Adres, string n_Local, string n_Post)
        {
            NI_Pesel = n_Pesel;
            string check_name = n_Name.Trim();
            check_name = char.ToUpper(check_name[0]) + check_name.Substring(1).ToLower();
            NI_Name = check_name;
            if (n_SecName != "" && n_SecName != " " && n_SecName != null) {
                string check_SecName = n_SecName.Trim();
                check_SecName = char.ToUpper(check_SecName[0]) + check_SecName.Substring(1).ToLower();
                NI_SecName = check_SecName;
            }
            else NI_SecName = " ";
            string check_LastName = n_LastName.Trim();
            check_LastName = char.ToUpper(check_LastName[0]) + check_LastName.Substring(1).ToLower();
            NI_LastName = check_LastName;
            NI_Date = n_Date;
            if (n_Phone != "" && n_Phone != " " && n_Phone != null) {
                string check_phone;
                check_phone = n_Phone.Replace(" ", "");
                if (!check_phone.StartsWith("+"))
                {
                    check_phone = "+48" + check_phone;
                }

                NI_Phone = check_phone;
            }
            else NI_Phone = "000000000";
            NI_Adres = n_Adres.Trim(); ;
            NI_Local = n_Local;
            NI_Post = n_Post;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Owner = this;

            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            while (Lista.SelectedItems.Count > 0)
            {
                Lista.Items.Remove(Lista.SelectedItems[0]);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            openFileDialog.Title = "Otwórz plik CSV";
            if (openFileDialog.ShowDialog() == true)
            {
                Lista.Items.Clear();
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                string delimiter = ";";
                if (selectedFilterIndex == 1)
                {
                    delimiter = ",";
                }
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    foreach (var line in lines)
                    {
                        string[] columns = line.Split(delimiter);
                        if (columns != null)
                        {
                            Lista.Items.Add(new NewItem(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5], columns[6], columns[7], columns[8]
                            ));
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            saveFileDialog.Title = "Zapisz jako plik CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ";";
                if (saveFileDialog.FilterIndex == 1)
                {
                    delimiter = ",";
                }
                using (var writer = new StreamWriter(filePath))
                {
                    var s = Lista.Items;
                    foreach (var item in s)
                    {
                        var properties = item.GetType().GetProperties();
                        List<string> values = new List<string>();
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(item, null);
                            values.Add(value?.ToString() ?? "");
                        }
                        string line = string.Join(delimiter, values);
                        writer.WriteLine(line);

                    }
                }
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2();
            window.Show();
        }
    }
}