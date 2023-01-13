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
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;

namespace katalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Ksiazka> ListaKsiazek = null;

        private void Wiazanie()
        {

            ListaKsiazek = new ObservableCollection<Ksiazka>();
            ListaKsiazek.Add(new Ksiazka(12441, "Harry Potter", "Maksiu", "Thriller"));
            ListaKsiazek.Add(new Ksiazka(28478, "Kubuś Puchatek", "Krzyś", "Przygodowy"));
            ListaKsiazek.Add(new Ksiazka(93719, "Zemsta", "Mickiewicz", "Dramatyczny"));
            ListaKsiazek.Add(new Ksiazka(48102, "Czerwone Krzesło", "Ktostamktostam", "Fantastyczny"));
            ListaKsiazek.Add(new Ksiazka(58910, "Dziennik Cwaniaka", "Jeff Kinney", "Komedia"));
            lstksiazki.ItemsSource = ListaKsiazek;

            //filtrowanie
            CollectionView widok = (CollectionView)CollectionViewSource.GetDefaultView(lstksiazki.ItemsSource);
            widok.Filter = FiltrUżytkownika;
        }
        private bool FiltrUżytkownika(object item)
        {
            if (String.IsNullOrEmpty(txtfilter.Text))
                return true;
            else
                switch (Filter.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""))
                {
                    case "Autor":
                        return ((item as Ksiazka)).Autor.IndexOf(txtfilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Tytuł":
                        return ((item as Ksiazka)).Tytul.IndexOf(txtfilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                    case "Gatunek":
                        return ((item as Ksiazka)).Gatunek.IndexOf(txtfilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return true;
                }

        }

        public MainWindow()
        {
            InitializeComponent();
            Wiazanie();
        }
        private void txtfilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstksiazki.ItemsSource).Refresh();
        }

        private void lstksiazki_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ksiazka KsiazkaZListy = lstksiazki.SelectedItem as Ksiazka;

            MessageBoxResult odpowiedz = MessageBox.Show("Czy usunac produkt: " + KsiazkaZListy.Tytul.ToString() + " " + KsiazkaZListy.numer_seryjny.ToString() + "?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (odpowiedz == MessageBoxResult.Yes) ListaKsiazek.Remove(KsiazkaZListy);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
      

            
            ListaKsiazek.Add(new Ksiazka(int.Parse(isbn.Text), tytul.Text.ToString(), autor.Text.ToString(), gatunek.Text.ToString()));

        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            if (save.ShowDialog() == DialogResult)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));

                write.Write(ListaKsiazek);
                write.Dispose();
            }
        }
    }
}
