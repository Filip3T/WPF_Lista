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
    
    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
        }

        static int SumaKontrolna(string pesel)
        {
            int kontrolna = 0;
            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            for (int i = 0; i < 10; i++)
            {
                kontrolna += int.Parse(pesel[i].ToString()) * wagi[i];
            }
            kontrolna %= 10;
            kontrolna = 10 - kontrolna;
            kontrolna %= 10;
            return kontrolna;
        }
        static bool CzyPoprawnyPesel(string pesel, ref int rok, ref int miesiac, ref int dzien)
        {
            bool correct = true;

            if (pesel.Length != 11)
            {
                correct = false;
            }
            else
            {
                char[] pesel_rok_ch = [pesel[0], pesel[1]];
                int pesel_rok_int = int.Parse(new string(pesel_rok_ch.ToArray()));
                if (rok % 100 != pesel_rok_int)
                {
                    correct = false;
                }
                else
                {
                    int miesiac_tmp = miesiac;
                    int stulecie = ((rok % 1000) - (rok % 100)) / 100;
                    switch (stulecie)
                    {
                        case 8:
                            miesiac_tmp += 80;
                            break;
                        case 0:
                            miesiac_tmp += 20;
                            break;
                        case 1:
                            miesiac_tmp += 40;
                            break;
                        case 2:
                            miesiac_tmp += 60;
                            break;
                    }
                    char[] pesel_miesiac_ch = [pesel[2], pesel[3]];
                    int pesel_miesiac_int = int.Parse(new string(pesel_miesiac_ch.ToArray()));
                    if (miesiac_tmp != pesel_miesiac_int)
                    {
                        correct = false;
                    }
                    else
                    {
                        char[] pesel_dzien_ch = [pesel[4], pesel[5]];
                        int pesel_dzien_int = int.Parse(new string(pesel_dzien_ch.ToArray()));
                        if (dzien != pesel_dzien_int)
                        {
                            correct = false;
                        }
                        else
                        {
                            if (int.Parse(pesel[10].ToString()) != SumaKontrolna(pesel))
                            {
                                correct = false;
                            }
                        }
                    }
                }
            }
            return correct;
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
                string data = Date.SelectedDate.ToString().Split()[0];
                string Srok, Smiesiac, Sdzien;
                Srok = data.Split(".")[2];
                Smiesiac = data.Split(".")[1];
                Sdzien = data.Split(".")[0];
                int rok, miesiac, dzien;
                int.TryParse(Srok, out rok);
                int.TryParse(Smiesiac, out miesiac);
                int.TryParse(Sdzien, out dzien);


                if (CzyPoprawnyPesel(Pesel.Text, ref rok, ref miesiac, ref dzien))
                {
                    NewItem item = new NewItem(Pesel.Text, Name.Text, SecName.Text, LastName.Text, data, Phone.Text, Adres.Text, Local.Text, Post.Text);
                    MainWindow mainWindow = Owner as MainWindow;

                    mainWindow.Lista.Items.Add(new
                    {
                        m_strName = item.NI_Name,
                        m_strLastName = item.NI_LastName,
                        m_strPesel = item.NI_Pesel,
                        m_strSecName = item.NI_SecName,
                        m_strDate = item.NI_Date,
                        m_strPhone = item.NI_Phone,
                        m_strAdres = item.NI_Adres,
                        m_strLocal = item.NI_Local,
                        m_strPost = item.NI_Post
                    });
                    this.Close();
                } else
                {
                    Pesel.Background = Brushes.Red;
                }
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