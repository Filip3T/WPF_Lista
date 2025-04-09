using System;
using System.Collections;
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
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string SecName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public string Phone { get; set; }
        public string Adres { get; set; }
        public string Local { get; set; }
        public string Post { get; set; }

        public NewItem(string n_Pesel, string n_Name, string n_SecName, string n_LastName, string n_Date, string n_Phone, string n_Adres,  string n_Local, string n_Post)
        {
            Pesel = n_Pesel;
            Name = n_Name;
            SecName = n_SecName;
            LastName = n_LastName;
            Date = n_Date;
            Phone = n_Phone;
            Adres = n_Adres;
            Local = n_Local;
            Post = n_Post;
        }
    }
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
        }

        public bool empty_check()
        {
            bool empty = false;
            if (Pesel.Text == "") { empty = true; Pesel.Background = Brushes.Red; }
            if (Name.Text == "") { empty = true; Name.Background = Brushes.Red; }
            if (LastName.Text == "") { empty = true; LastName.Background = Brushes.Red; }
            if (Date.Text == "") { empty = true; Date.Background = Brushes.Red; }
            if (Adres.Text == "") { empty = true; Adres.Background = Brushes.Red; }
            if (Local.Text == "") { empty = true; Local.Background = Brushes.Red; }
            if (Post.Text == "") { empty = true; Post.Background = Brushes.Red; }

            return empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool empty = empty_check();

            if (!empty)
            {
                NewItem item = new NewItem(Pesel.Text, Name.Text, SecName.Text, LastName.Text, Date.Text, Phone.Text, Adres.Text, Local.Text, Post.Text);
                MainWindow mainWindow = Owner as MainWindow;

                mainWindow.Lista.Items.Add(new {m_strName = item.Name, m_strLastName = item.LastName, m_strPesel = item.Pesel, m_strSecName = item.SecName,
                m_strDate = item.Date, m_strPhone = item.Phone, m_strAdres = item.Adres, m_strLocal = item.Local, m_strPost = item.Post});
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool empty = empty_check();
            if(!empty)
            {
                var Result = MessageBox.Show("Jesteś pweiwn?", "Anuluj", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
        }
    }
}
