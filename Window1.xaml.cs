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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    class NewItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }

        public NewItem(string n_Name, string n_LastName, string n_Pesel)
        {
            var random = new Random();
            ID = random.Next();
            Name = n_Name;
            LastName = n_LastName;
            Pesel = n_Pesel;
        }
    }
    public partial class Window1 : Window
    {
        NewItem item = new NewItem("", "", "");
        public Window1()
        {
            InitializeComponent();
            ID.Text = $"{item.ID}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            item.Name = Name.Text;
            item.LastName = LastName.Text;
            item.Pesel = Pesel.Text;
            MainWindow mainWindow = Owner as MainWindow;

            mainWindow.Lista.Items.Add(new { m_nID = item.ID, m_strName = item.Name, m_strLastName = item.LastName, m_strPesel = item.Pesel });

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Jesteś pweiwn?", "Anuluj" , MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
