using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Wisielec
{
    /// <summary>
    /// Klasa odpowiedzialna za okno wyświetlające statystyki graczy.
    /// </summary>
    public partial class HistoriaGraczyOkno : Window
    {

        private bool czyDzwiek;

        /// <summary>
        /// Przechowuje informacje, czy gracz wybrał tryb cichy czy z dźwiękiem.
        /// </summary>
        public bool CzyDzwiek { get => czyDzwiek; set => czyDzwiek = value; }


        /// <summary>
        /// Konstruktor, który otwiera nowe okno i wyświetla w ListBox statystyki graczy.
        /// </summary>
        /// <param name="gracze">Lista graczy zapisanych w bazie danych.</param>
        public HistoriaGraczyOkno(List<Gracz> gracze)
        {
            InitializeComponent();
            ListboxHistoriaGraczy.ItemsSource = gracze;
        }

        /// <summary>
        /// Przycisk pozwalający powrócić do menu głównego.
        /// </summary>
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        /// <summary>
        /// Przycisk sorujący graczy w ListBox na podstawie liczby wygranych malejąco.
        /// </summary>
        public void ButtonSortujWygrane_Click(object sender, RoutedEventArgs e)
        {
            // Czyści obecne ustawienia sortowania
            this.ListboxHistoriaGraczy.Items.SortDescriptions.Clear();

            // Dodaje nowe ustawienia sortowania
            this.ListboxHistoriaGraczy.Items.SortDescriptions.Add(
                new SortDescription(nameof(Gracz.GryWygrane), ListSortDirection.Descending));
        }


        /// <summary>
        /// Przycisk sorujący graczy w ListBox na podstawie odsetka wygranych malejąco.
        /// <summary>
        public void SortujOdsetek_Click(object sender, RoutedEventArgs e)
        {
            this.ListboxHistoriaGraczy.Items.SortDescriptions.Clear();

            this.ListboxHistoriaGraczy.Items.SortDescriptions.Add(
                new SortDescription(nameof(Gracz.OdsetekSkutecznosci), ListSortDirection.Descending));
        }


        /// <summary>
        /// Przycisk sorujący graczy w ListBox według kolejności alfabetycznej.
        /// </summary>
        public void SortujAlfabetycznie_Click(object sender, RoutedEventArgs e)
        {
            this.ListboxHistoriaGraczy.Items.SortDescriptions.Clear();

            // Dodaj nowe ustawienia sortowania dla ilości wygranych gier
            this.ListboxHistoriaGraczy.Items.SortDescriptions.Add(
                new SortDescription(nameof(Gracz.Nazwa), ListSortDirection.Ascending));
        }

        /// <summary>
        /// Przycisk, który pozwala wyszukać gracza w ListBox, na podstawie wpisanej przez użytkownika nazwy.
        /// </summary>
        public void BSzukaj_Click(object sender, RoutedEventArgs e)
        {
            this.ListboxHistoriaGraczy.SelectedItems.Clear();
            if (TxtSzukaj.Text == null || TxtSzukaj.Text == string.Empty)
                return;

            foreach (var item in this.ListboxHistoriaGraczy.ItemsSource)
            {
                if (((Gracz)item).Nazwa.ToString() == TxtSzukaj.Text)
                {
                    this.ListboxHistoriaGraczy.SelectedItems.Add(item);
                }
            }
        }

    }
}
