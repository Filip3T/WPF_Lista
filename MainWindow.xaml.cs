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

        public NewItem(string n_Pesel, string n_Name, string n_SecName, string n_LastName, string n_Date, string n_Phone, string n_Adres, string n_Local, string n_Post)
        {
            NI_Pesel = n_Pesel;
            NI_Name = n_Name;
            NI_SecName = n_SecName;
            NI_LastName = n_LastName;
            NI_Date = n_Date;
            NI_Phone = n_Phone;
            NI_Adres = n_Adres;
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
                            Lista.Items.Add(new
                            {
                                m_strName = columns[1],
                                m_strLastName = columns[3],
                                m_strPesel = columns[0],
                                m_strSecName = columns[2],
                                m_strDate = columns[4],
                                m_strPhone = columns[5],
                                m_strAdres = columns[6],
                                m_strLocal = columns[7],
                                m_strPost = columns[8]
                            });
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
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (NewItem item in Lista.Items)
                    {
                        var row = $"{item.NI_Pesel}{delimiter}{item.NI_Name}" +
                        $"{delimiter}{item.NI_SecName}{delimiter}{item.NI_LastName}" +
                        $"{delimiter}{item.NI_Date}{delimiter}{item.NI_Phone}" +
                        $"{delimiter}{item.NI_Adres}{delimiter}{item.NI_Local}{delimiter}{item.NI_Post}";
                        writer.WriteLine(row);
                    }
                }
            }
        }
    }
}