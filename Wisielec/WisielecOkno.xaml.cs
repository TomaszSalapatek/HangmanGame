using System;
using System.Windows;

namespace Wisielec
{   
    
    /// <summary>
    /// Klasa pomocnicza służąca do ustalenia ustawień przed rozgrywką.
    /// </summary>
    public partial class WisielecOkno : Window
    {
        private Gracz graczOkno;
        private Gra graOkno;
        private bool czyDzwiek;

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
        /// Konstruktor parametryczny - ustawia gracza, dźwięk oraz tło okna,
        /// inicjalizuje dwa cComboBox odpowiedzialne za kategorie oraz poziom trudności.
        /// </summary>
        /// <param name="gracz">AGracz, który aktualnie gra.</param>
        /// <param name="czyDzwiek">Czy dźwięk jest włączony.</param>
        public WisielecOkno(Gracz gracz, bool czyDzwiek)
        {
            GraczOkno = gracz;
            CzyDzwiek = czyDzwiek;

            InitializeComponent();
            InicjalizujCmbWyborKategori();
            InicjalizujCmBWyborTrudnosci();
            
            string katalogAplikacji_1 = AppContext.BaseDirectory;
            string sciezkaFolder = $"\\Assets\\litery.mp4";
            string sciezkaObrazu = katalogAplikacji_1 + sciezkaFolder;
            BackgroundVideo.Source = new Uri(sciezkaObrazu, UriKind.Relative); string katalogAplikacji = AppContext.BaseDirectory;
        }
        
        /// <summary>
        /// Metoda odpowiedzialna za sprecyzowanie ustawień dotyczących video w tle okna.
        /// </summary>
        public void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
            BackgroundVideo.MediaEnded += BackgroundVideo_MediaEnded;

        }

        /// <summary>
        /// Metoda, która dodaje możliwe kategorie do ComboBoxa.
        /// </summary>
        public void InicjalizujCmbWyborKategori()
        {

            CmBWyborKategori.Items.Add(EnumKategoria.Sporty);
            CmBWyborKategori.Items.Add(EnumKategoria.Zwierzęta);
            CmBWyborKategori.Items.Add(EnumKategoria.Zawody);
            CmBWyborKategori.Items.Add(EnumKategoria.Rośliny);
            CmBWyborKategori.Items.Add(EnumKategoria.Państwa);
        }

        /// <summary>
        /// Metoda, która dodaje możliwe poziomy do ComboBoxa.
        /// </summary>
        public void InicjalizujCmBWyborTrudnosci()
        {
            CmBWyborTrudnosci.Items.Add(EnumPoziom.Łatwy);
            CmBWyborTrudnosci.Items.Add(EnumPoziom.Średni);
            CmBWyborTrudnosci.Items.Add(EnumPoziom.Trudny);
        }
        
        /// <summary>
        /// Kliknięcie przycisku Menu skutkuje powrotem do głównego okna gry.
        /// </summary>
        public void BWrocMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        /// <summary>
        /// Metoda obsługująca zdarzenie, gdy klikniemy na przycisk "Start gry". 
        /// </summary>
        /// <remarks>
        /// Tworzy się gra z ustalonymi wcześniej parametrami oraz przechodzimy do nowego okna WisielecGra.
        /// </remarks>
        public void Bstart_Click(object sender, RoutedEventArgs e)
        {

                // Jeśli użytkownik nie wybrał poziomu trudności lub kategorii pojawia się MessageBox z kounikatem.
                if (CmBWyborKategori.SelectedItem == null || CmBWyborTrudnosci.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wprowadzić kategorię i poziom", "Brak kategorii lub poziomu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    EnumKategoria kategoria = (EnumKategoria)CmBWyborKategori.SelectedItem;
                    EnumPoziom poziom = (EnumPoziom)CmBWyborTrudnosci.SelectedItem;


                    Gra nowaGra = new Gra(kategoria, poziom, GraczOkno);  
                    WisielecGra rozpoczynamyGre = new WisielecGra(GraczOkno, nowaGra, CzyDzwiek); 

                    rozpoczynamyGre.Show();
                    this.Close();
                }
        }
    }
}