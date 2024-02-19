using System.Media;


namespace Wisielec
{
    /// <summary>
    /// Typ wyliczeniowy zawierający możliwe kategorie gry.
    /// </summary>
    public enum EnumKategoria { Zwierzęta, Państwa, Rośliny, Zawody, Sporty }
    
    /// <summary>
    /// Typ wyliczeniowy zawierający możliwe poziomy gry.
    /// </summary>
    public enum EnumPoziom { Łatwy, Średni, Trudny }


    /// <summary>
    /// Klasa abstrakcyjna zawierająca podstawowe pola, właściwości i metody potrzebne w klasie Gra.
    /// </summary>
    /// <remarks>
    /// Zawiera podstawowe ustawienia do gry.
    /// </remarks>
    public abstract class PodstawyGry
    {
        private string sekretneSlowo;
        private int liczbaProb;
        private int czyDzwiek;
        private static SoundPlayer musicPlayer = new SoundPlayer();
        private Dictionary<EnumKategoria, List<string>> slowaKategorii = new Dictionary<EnumKategoria, List<string>>();
        private EnumKategoria wybranaKategoria;
        private EnumPoziom wybranyPoziom;

        
        /// <summary>
        /// Słowo, które będzie zgadywane przez gracza.
        /// </summary>
        public string SekretneSlowo { get => sekretneSlowo; set => sekretneSlowo = value; }
        
        /// <summary>
        /// Liczba prób, którą ma gracz na zgadnięcie słowa. Ustawiona na podstawie wybvranego poziomu.
        /// </summary>
        public int LiczbaProb { get => liczbaProb; set => liczbaProb = value; }

        /// <summary>
        /// Przechowuje informacje, czy gracz wybrał tryb cichy czy z dźwiękiem.
        /// </summary>
        public int CzyDzwiek { get => czyDzwiek; set => czyDzwiek = value; }

        /// <summary>
        /// Pełni rolę odtwarzacza muzyki.
        /// </summary>
        public static SoundPlayer MusicPlayer { get => musicPlayer; set => musicPlayer = value; }

        /// <summary>
        /// Słownik, w którym przechowywane są słowa w zależności od kategorii.
        /// </summary>
        /// <remarks>
        /// Kategoria słowa jest kluczem, a lista słów odpowiadającą mu wartością.
        /// </remarks>
        public Dictionary<EnumKategoria, List<string>> SlowaKategorii { get => slowaKategorii; set => slowaKategorii = value; }
        
        /// <summary>
        /// Kategoria wybrana przez gracza.
        /// </summary>
        public EnumKategoria WybranaKategoria { get => wybranaKategoria; set => wybranaKategoria = value; }
        
        /// <summary>
        /// Poziom wybrany przez gracza
        /// </summary>
        public EnumPoziom WybranyPoziom { get => wybranyPoziom; set => wybranyPoziom = value; }


        /// <summary>
        /// Metoda wczytująca słowa do gry z pliku.
        /// </summary>
        /// <param name="nazwaPliku">Nazwa pliku, z którego mają być wczytane słowa.</param>
        /// <returns>Lista słów odczytanych z podanego pliku.</returns>
        public List<string> WczytajSlowaZPliku(string nazwaPliku)
        {
            List<string> slowa = new List<string>();

            if (File.Exists(nazwaPliku))
            {
                string[] lines = File.ReadAllLines(nazwaPliku);
                slowa.AddRange(lines);
            }

            return slowa;
        }


        /// <summary>
        /// Wczytuje wszystkie słowa o odpowiedniej kategorii z wielu plików.
        /// </summary>
        /// <remarks>
        /// Wykorzystuje metodę WczytajSlowaZPliku()
        /// </remarks>
        public void WczytajSlowaZPlikow()
        {
            string katalogAplikacji = AppContext.BaseDirectory;
            SlowaKategorii[EnumKategoria.Zwierzęta] = WczytajSlowaZPliku(katalogAplikacji + "\\wisielecSlowa\\slowaZwierzeta.txt");
            SlowaKategorii[EnumKategoria.Rośliny] = WczytajSlowaZPliku(katalogAplikacji + "\\wisielecSlowa\\slowaRosliny.txt");
            SlowaKategorii[EnumKategoria.Zawody] = WczytajSlowaZPliku(katalogAplikacji + "\\wisielecSlowa\\slowaZawody.txt");
            SlowaKategorii[EnumKategoria.Państwa] = WczytajSlowaZPliku(katalogAplikacji + "\\wisielecSlowa\\slowaPanstwa.txt");
            SlowaKategorii[EnumKategoria.Sporty] = WczytajSlowaZPliku(katalogAplikacji + "\\wisielecSlowa\\slowaSporty.txt");
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
        /// Metoda abstrakcyjna do przesłonięcia w klasie Gra.
        /// </summary>
        /// <param name="poziom">Poziom wybrany przez gracza</param>
        public abstract void UstawLiczbeProbNaPodstawiePoziomu(EnumPoziom poziom);

    }
}
