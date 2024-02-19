using System.Text;
using System.Windows;

namespace Wisielec
{
    /// <summary>
    /// Klasa odpowiedzialna za okno zakończenia gry.
    /// </summary>
    public partial class ZakonczenieGryOkno : Window
    {
        private string message;
        private Gracz graczOkno;
        private Gra graOkno;
        private bool czyDzwiek;

        /// <summary>
        /// Aktualnie grający gracz.
        /// </summary>
        public Gracz GraczOkno { get => graczOkno; set => graczOkno = value; }

        /// <summary>
        /// Aktualna gra.
        /// </summary>
        public Gra GraOkno { get => graOkno; set => graOkno = value; }

        /// <summary>
        /// Zmienna mówiąca o tym, czy w grze ma być dźwięk.
        /// </summary>
        public bool CzyDzwiek { get => czyDzwiek; set => czyDzwiek = value; }

        /// <summary>
        /// Wiadomość do przekazania w oknie.
        /// </summary>
        public string Message { get => message; set => message = value; }

        /// <summary>
        /// Konstruktor tworzy nowe okno, ustawia grę, gracza i dźwięk a także wiadomość.
        /// </summary>
        /// <param name="message">Wiadomość do przekazania po zakończeniu gry.</param>
        /// <param name="gracz">Aktualny gracz.</param>
        /// <param name="gra">Aktualna gra.</param>
        /// <param name="czyDzwiek">Zmienna mówiąca o tym, czy ma być dźwięk.</param>
        public ZakonczenieGryOkno(string message, Gracz gracz, Gra gra, bool czyDzwiek)
        {
            GraczOkno = gracz;
            GraOkno = gra;
            CzyDzwiek = czyDzwiek;
            Message = message;

            InitializeComponent();
            DataContext = this;

        }

        /// <summary>
        /// Przycisk Kontunuuj pozwala kontynować grę na tym zamym poziomie, z tą samą kategorią. Zamyka okno ZakonczenieGryOkno.
        /// </summary>
        private void BtnZakonczenieKontunuuj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Przycisk Ustawienia pozwala wrócić do ustawień gry, aby zmienić poziom i kategorię.
        /// Gracz pozostaje ten sam. Zamykane jest ZakonczenieGryOkno.
        /// </summary>
        private void BtnZakonczenieUstawienia_Click(object sender, RoutedEventArgs e)
        {
            WisielecOkno wisielecOkno = new WisielecOkno(GraczOkno, CzyDzwiek);
            wisielecOkno.Show();
            this.Close();
        }

        /// <summary>
        /// Przycisk Menu pozwala wrócić do menu głównego.  Zapisuje wynik gracza do bazy danych oraz
        /// zamyka ZakonczenieGryOkno i WisielecGraOkno.
        /// </summary>
        private void BtnZakonczenieMenu_Click(object sender, RoutedEventArgs e)
        {
            GraczOkno.ZapiszDoBazy();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Przycisk Statystyki pozwala zobaczyć statystyki gracza w obecnej rozgrywce. Po naciśnięciu wyświetla się
        /// MessageBox z odpowiednim komunikatem, który przedstawia nazwę gracza, ilość gier zagranych i wygranych oraz
        /// jego odsetek skuteczności.
        /// </summary>
        private void BtnZakonczenieStatystyki_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Gracz: {GraczOkno.Nazwa}");
            sb.AppendLine($"Gry zagrane: {GraczOkno.GryZagrane}");
            sb.AppendLine($"Gry wygrane: {GraczOkno.GryWygrane}");
            sb.AppendLine($"Odsetek skuteczności: {GraczOkno.ObliczSkutecznosc()}%");

            this.Hide();
            MessageBox.Show(sb.ToString(), "Statystyki", MessageBoxButton.OK);
            this.ShowDialog();
        }
    }
}

