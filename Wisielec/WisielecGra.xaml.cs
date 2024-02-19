using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Wisielec
{

    public partial class WisielecGra : Window
    {
        private Gracz graczOkno;
        private Gra graOkno;
        private bool czyDzwiek;
        private int numerObrazu = 1;

        /// <summary>
        /// Gracz, który aktualnie gra.
        /// </summary>
        public Gracz GraczOkno { get => graczOkno; set => graczOkno = value; }

        /// <summary>
        /// Aktualna gra.
        /// </summary>
        public Gra GraOkno { get => graOkno; set => graOkno = value; }


        /// <summary>
        /// Zmienna, która mówi, czy dźwięk jest włączony.
        /// </summary>
        public bool CzyDzwiek { get => czyDzwiek; set => czyDzwiek = value; }


        /// <summary>
        /// Konstruktor, który otwiera nowe okno WisielecGra i ustawia gracza, grę i dźwięk w grze.
        /// </summary>
        /// <param name="gracz">Aktualnie grający gracz.</param>
        /// <param name="gra">Aktualna gra.</param>
        /// <param name="czyDzwiek">Zmienna, która mówi, czy dźwięk jest włączony.</param>
        /// <remarks>
        /// Dodatkowo ustawia opisy z nazwą gracza, wybraną kategorią i poziomem trudności oraz rejstruje obsługę zdarzeń
        /// i wyświetla odpowiedni obraz szubienicy.
        /// </remarks>
        public WisielecGra(Gracz gracz, Gra gra, bool czyDzwiek)
        {
            InitializeComponent();

            GraczOkno = gracz;
            GraOkno = gra;
            CzyDzwiek = czyDzwiek;

            WyswietlObrazSzubienicy();
            ZarejestrujObslugeZdarzen();
            
            LabelNazwaGracza.Content = "Gracz: " + GraczOkno.Nazwa;
            LabelKategoria.Content = "Kategoria: " + GraOkno.WybranaKategoria.ToString();
            LabelPoziom.Content = "Poziom: " + GraOkno.WybranyPoziom.ToString();
        }

        /// <summary>
        /// Przycisk Menu pozwala powrót do okna głównego i jednocześnie zapisuje aktualnego gracza do bazy danych.
        /// </summary>
        public void BSettings_Click(object sender, RoutedEventArgs e)
        {
            GraczOkno.ZapiszDoBazy();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Metoda, która służy wyświetleniu odpowiedniego obrazu szubienicy na początku gry.
        /// </summary>
        /// <remarks>
        /// Obraz szubienicy jest pobierany z odpowiedniego pliku w zależności od wybranego poziomu gry.
        /// </remarks>
        public void WyswietlObrazSzubienicy()
        {
            string katalogAplikacji = AppContext.BaseDirectory;

            if (GraOkno.WybranyPoziom == EnumPoziom.Łatwy)
            {
                string sciezkaFolder_latwy = $"\\Obrazy/p{numerObrazu}.png";
                string sciezkaObrazu = katalogAplikacji + sciezkaFolder_latwy;
                obrazWisielca.Source = new BitmapImage(new Uri(sciezkaObrazu, UriKind.RelativeOrAbsolute));
            }
            else if (GraOkno.WybranyPoziom == EnumPoziom.Średni)
            {
                string sciezkaFolder_sredni = $"\\Obrazy/s{numerObrazu}.png";
                string sciezkaObrazu = katalogAplikacji + sciezkaFolder_sredni;
                obrazWisielca.Source = new BitmapImage(new Uri(sciezkaObrazu, UriKind.RelativeOrAbsolute));
            }
            else if (GraOkno.WybranyPoziom == EnumPoziom.Trudny)
            {
                string sciezkaFolder_trudny = $"\\Obrazy/t{numerObrazu}.png";
                string sciezkaObrazu = katalogAplikacji + sciezkaFolder_trudny;
                obrazWisielca.Source = new BitmapImage(new Uri(sciezkaObrazu, UriKind.RelativeOrAbsolute));
            }
        }

        /// <summary>
        /// Metoda służy do wyświetlania kolejnych obrazów szubienicy w miarę postępu gry.
        /// </summary>
        public void NastepnyObrazSzubienicy()
        {
            if (numerObrazu <= 12)
            {
                numerObrazu++;
                WyswietlObrazSzubienicy();
            }
        }

        /// <summary>
        /// Przycisk Wylosuj słowo pozwala graczowi wylosować słowo, które będzie zgadywał.
        /// </summary>
        /// <remarks>
        /// Po wciśnięciu następuje inicjalizacja gry, ustawiany jest odpowiedni opis z liczbą prób, a także
        /// pojawia się ukryte sekretne słowo. Wyświetlony jest obraz szubienicy nr 1 oraz zmienione na widoczne stają się:
        /// napis Podaj literę, pole tekstowe do wpisania litery oraz przycisk Sprawdź.
        /// </remarks>
        public void BtnWylosujSlowo_Click(object sender, RoutedEventArgs e)
        {
            GraOkno.InicjalizujGre();

            LabelLiczbaProb.Content = "Liczba prób: " + GraOkno.LiczbaProb;
            LabelUkryteSlowo.Content = GraOkno.UkryjSekretneSlowo();

            numerObrazu = 1;
            WyswietlObrazSzubienicy();

            LabelPodajLitere.Visibility = Visibility.Visible;
            TxtPodawanaLitera.Visibility = Visibility.Visible;
            ButtonSprawdz.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Przycisk Sprawdź pozwalający sprawdzić graczowi, czy podana przez niego litera zawiera się w sekretnym słowie.
        /// </summary>
        /// <remarks>
        /// Metoda zabezpiecza wszystkie możliwe przypadki, gdy gracz nie wpisze żadnego znaku lub więcej niż jeden znak,
        /// lub podany przez niego znak nie będzie literą. Wyświetla się wtedy MessageBox z odpowiednim komunikatem.
        /// Sprawdzone jest też, czy litera była już wcześniej podawana, jeśli tak nic się nie dzieje, ale gra odpowiedni dźwięk.
        /// Jeśli znak jest poprawny, sprawdzane jest, czy zawiera się on w sekretnym słowie. Jeśli tak, odsłaniany jest postęp
        /// odgadywanych liter w słowie i gra odpowiedni dźwięk. Jeśli nie, gracz traci jedną szansę i wyświetla się kolejny
        /// obraz na szubienicy. Sprawdzone jest także, czy gra została już zakończona i jeśli tak, wywoływana jest metoda odpowiedzialna
        /// za działąnia po zakończeniu gry.
        /// </remarks>
        public void ButtonSprawdz_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie, czy TextBox nie jest pusty
            if (!string.IsNullOrEmpty(TxtPodawanaLitera.Text))
            {
                // Sprawdzenie, czy TextBox zawiera tylko jedną literę
                if (TxtPodawanaLitera.Text.Length == 1)
                {
                    // Pobranie pierwszej litery
                    char wprowadzonaLitera = TxtPodawanaLitera.Text[0];

                    // Sprawdzenie, czy wprowadzony znak jest literą
                    if (char.IsLetter(wprowadzonaLitera))
                    {
                        TxtPodawanaLitera.Clear();

                        if (!GraOkno.SprawdzCzyLiteraPodana(wprowadzonaLitera))
                        {
                            if (GraOkno.SprawdzCzyLiteraWystepujeWSlowie(wprowadzonaLitera))
                            {
                                if(CzyDzwiek)
                                {
                                    Gra.OdtworzDzwiek("Dzwieki\\zgadniete.wav");
                                }
                                
                            }
                            else
                            {
                                NastepnyObrazSzubienicy();
                                LabelLiczbaProb.Content = "Liczba prób: " + GraOkno.LiczbaProb;
                                if (CzyDzwiek)
                                {
                                    Gra.OdtworzDzwiek("Dzwieki\\niezgadniete.wav");
                                }
                                
                            }
                        }
                        else // Jeśli litera została już wcześniej podana
                        {
                            if(CzyDzwiek)
                            {
                                Gra.OdtworzDzwiek("Dzwieki\\powtorzenie.wav");
                            }
                            
                        }

                        // Odsłonięcie postępu odgadniętych liter w sekretnym słowie
                        LabelUkryteSlowo.Content = GraOkno.UkryjSekretneSlowo();

                        // Sprawdzenie, czy gra została już zakończona
                        if (GraOkno.LiczbaProb == 0 || GraOkno.CzyOdgadniete())
                        {
                            ZakonczGre();
                        }
                    }
                    else
                    {
                        // Błąd - wprowadzony znak nie jest literą
                        MessageBox.Show("Wprowadzony znak nie jest literą", "Błędny znak", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Błąd - gracz podał więcej niż jeden znak
                    MessageBox.Show("Wprowadź tylko jedną literę.", "Zbyt dużo liter", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Błąd - gracz nie podał litery
                MessageBox.Show("Wprowadź literę przed sprawdzeniem.", "Brak litery", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Metoda, która odpowiada za działania po zakończeniu gry w zależności od tego, czy gracz wygrał lub przegrał.
        /// </summary>
        /// <remarks>
        /// Jeśli gracz wygrał, zwiększają się jego wygrane. W obu przypadkach odtwarzany jest odpowiedni dźwięk
        /// (jeśli gracz wybrał tryb gry z dźwiękiem) oraz ustawiony odpowiedni komunikat. Otwiera się nowe okno ZakonczenieGryOkno
        /// z komunikatem. Gdy następuje powrót z okna zakończenia gry do okna gry, przywracane są ustawienia początkowe.
        /// </remarks>
        public void ZakonczGre()
        {
            string message = string.Empty;

            // Sprawdzenie, czy wszystkie litery zostały odgadnięte (gracz wygrał)
            if (GraOkno.CzyOdgadniete())
            {
                GraczOkno.GryWygrane++;
                message = $"Gratulacje! Odgadłeś sekretne słowo!";
                if(CzyDzwiek == true)
                {
                    Gra.OdtworzDzwiek("Dzwieki\\winningSound.wav");
                }
                
            }
            else // W przeciwnym wypadku gracz przegrał
            {
                message = $"Niestety, nie udało się. Prawidłowe słowo to: {GraOkno.SekretneSlowo}";
                if(CzyDzwiek == true)
                {
                    Gra.OdtworzDzwiek("Dzwieki\\losingSound.wav");
                }
            }

            ZakonczenieGryOkno zakonczenieGryOkno = new ZakonczenieGryOkno(message, GraczOkno, GraOkno, CzyDzwiek);
            bool? result = zakonczenieGryOkno.ShowDialog();

            if (result == true)
            {
                UstawieniePoczatkoweOknaGra();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Metoda przywraca ustawienia początkowe w oknie gry.
        /// </summary>
        /// <remarks>
        /// Przywracane ustawienia: ukryte słowo jest puste, liczba prób wynosi 0,
        /// niewidoczne stają się: napis Podaj literę, pole tekstowe do wpisywania litery i przycisk Sprawdź. Wyświetlany jest obraz szubienicy
        /// nr 1 oraz czyszczone są wszystkie litery ze zbioru zgadniętych liter.
        /// </remarks>
        public void UstawieniePoczatkoweOknaGra()
        {
            LabelUkryteSlowo.Content = string.Empty;
            LabelLiczbaProb.Content = "Liczba prób: 0";

            LabelPodajLitere.Visibility = Visibility.Hidden;
            TxtPodawanaLitera.Visibility = Visibility.Hidden;
            ButtonSprawdz.Visibility = Visibility.Hidden;

            numerObrazu = 1;
            WyswietlObrazSzubienicy();

            GraOkno.ZgadnieteLitery.Clear();
            LabelZgadnieteLitery.Content = string.Empty;
        }

        /// <summary>
        /// Metoda wywołuje zdarzenie po aktualizacji liter.
        /// </summary>
        public void ZarejestrujObslugeZdarzen()
        {
            GraOkno.ZgadnieteLiteryUpdated += AktualizujEtykieteZgadnietychLiter;
        }

        /// <summary>
        /// Metoda pozwala aktualizować etykietę ze wszystkimi zgadniętymi  przez gracza literami w trakcie rozgrywki.
        /// </summary>
        /// <param name="zgadnieteLitery">Zbiór wszystkich liter, króre odgadł gracz.</param>
        private void AktualizujEtykieteZgadnietychLiter(HashSet<char> zgadnieteLitery)
        {
            Dispatcher.Invoke(() =>
            {
                var wszystkieLitery = GraOkno.PobierzWszystkiePodaneLitery();
                LabelZgadnieteLitery.Content = $"Wszystkie podane litery: {string.Join(", ", wszystkieLitery)}";
            });
        }
    }
}
