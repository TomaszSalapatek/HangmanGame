using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;
using System.Media;


namespace Wisielec
{
    /// <summary>
    /// Klasa odpowiedzialna za główne okno gry.
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool czyDzwiek = true;
        private static SoundPlayer musicPlayer = new SoundPlayer();

        /// <summary>
        /// Przechowuje informacje, czy gracz wybrał tryb cichy czy z dźwiękiem.
        /// </summary>
        public bool CzyDzwiek { get => czyDzwiek; set => czyDzwiek = value; }

        /// <summary>
        /// Pełni rolę odtwarzacza muzyki.
        /// </summary>
        public static SoundPlayer MusicPlayer { get => musicPlayer; set => musicPlayer = value; }


        /// <summary>
        /// Konstruktor nieparametryczny głównego okna gry.
        /// </summary>
        /// /// <remarks>
        /// Otwiera główne okno, ustawia odpowiednie ścieżki do obrzaów i gifów.
        /// </remarks>
        public MainWindow()
        {
            InitializeComponent();
            
            string katalogAplikacji = AppContext.BaseDirectory;
            string sciezkaFolder = $"\\Assets\\gif4.gif";
            string sciezkaObrazu = katalogAplikacji + sciezkaFolder;

            var imageUri = new Uri(sciezkaObrazu, UriKind.RelativeOrAbsolute);
            var bitmap = new BitmapImage(imageUri);

            ImageBehavior.SetAnimatedSource(GifImage, bitmap);

            string sciezkaDzwiek1 = katalogAplikacji + $"\\Obrazy\\Dzwiek1.png";
            string sciezkaDzwiek2 = katalogAplikacji + $"\\Obrazy\\Dzwiek2.png";

            // Ustawienie dynamicznych ścieżek do obrazków
            ImageDzwiek1.Source = new BitmapImage(new Uri(sciezkaDzwiek1, UriKind.RelativeOrAbsolute));
            ImageDzwiek2.Source = new BitmapImage(new Uri(sciezkaDzwiek2, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Służy do odtworzenia dźwięku z odpowiedniego pliku.
        /// </summary>
        /// <param name="sciezka">Ścieżka do pliku z dźwiękiem</param>
        public static void OdtworzDzwiek(string sciezka)
        {
            MusicPlayer.SoundLocation = sciezka;
            MusicPlayer.Play();
        }


        /// <summary>
        /// Kliknięcie przycisku Nowa Gra skutkuje rozpoczęciem nowej gry.
        /// </summary>
        /// <remarks>
        /// Jeśli użytkownik podał nazwę - tworzony jest nowy gracz na jej podstawie oraz otwierane okno WisielecOkno.
        /// Jeśli użytkownik nie podał nazwy - wyświetla się MessageBox z informacją o braku podanej nazwy.
        /// </remarks>
        public void BNowaGra_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNazwaGraczaMain.Text))
            {
                string nazwaGracza = TxtNazwaGraczaMain.Text;
                Gracz gracz = new Gracz(nazwaGracza);         // tworzymy gracza już tutaj i przekazujemy go do WisielecOkno

                WisielecOkno wisielecOkno = new WisielecOkno(gracz, CzyDzwiek);
                wisielecOkno.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Proszę podać nazwę gracza.", "Brak nazwy gracza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Kliknięcie przycisku Statystyki Graczy skutkuje pojawieniem się nowego okna.
        /// </summary>
        /// <remarks>
        /// Wykonuje się zapytanie do bazy danych o wszystkich zapisanych w niej graczy.
        /// Jeśli w bazie nie ma żadnych graczy, wyświetlany jest MessageBox z odpowiednim komunikatem.
        /// Jeśli są gracze - otwiera się nowe okno z ich statystykami.
        /// </remarks>
        public void BStatystykiGraczy_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new HistoriaGraczyDbContext())
            {
                // Zapytanie do bazy danych
                var query = from gracz in db.Gracze
                            select gracz;

                // Sprawdzenie, czy w bazie jest jakiś gracz
                if (query.Any())
                {
                    List<Gracz> gracze = query.ToList();

                    // Otworzenie okna i przekazanie danych
                    HistoriaGraczyOkno historiaGraczyOkno = new HistoriaGraczyOkno(gracze);
                    historiaGraczyOkno.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Brak danych o graczach.", "Historia Graczy", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        /// <summary>
        /// Metoda odpowiedzialna za wyciszenie dźwięku w grze i wyświetlenie znaku wyciszenia.
        /// </summary>
        public void Dzwiek1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ImageDzwiek1.Visibility = Visibility.Hidden;
            ImageDzwiek2.Visibility = Visibility.Visible;
            CzyDzwiek = false;
        }

        /// <summary>
        /// Metoda odpowiedzialna za ponowne włączenie dźwięku w grze i wyświetlenie znaku dźwięku.
        /// </summary>
        public void Dzwiek2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ImageDzwiek1.Visibility = Visibility.Visible;
            ImageDzwiek2.Visibility = Visibility.Hidden;
            OdtworzDzwiek("Dzwieki\\beep.wav");
            CzyDzwiek = true;
        }

        /// <summary>
        /// Zamknięcie okna głównego gry.
        /// </summary>
        public void BZakoncz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}






