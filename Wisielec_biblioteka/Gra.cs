using System.Text;

namespace Wisielec
{
    /// <summary>
    /// Główna klasa odpowiedzialna za rozgrywkę.
    /// </summary>
    ///<remarks>
    ///Dziedziczy po interfejsie IFunkcjeGry
    /// </remarks>
    public class Gra : PodstawyGry, IFunkcjeGry
    {

        private Gracz gracz;
        private HashSet<char> zgadnieteLitery = new HashSet<char>();
        private HashSet<char> wszystkiePodaneLitery = new HashSet<char>();


        /// <summary>
        /// Gracz, który aktualnie gra.
        /// </summary>
        public Gracz Gracz { get => gracz; set => gracz = value; }

        /// <summary>
        /// Litery, które już zostały zgadnięte.
        /// </summary>
        public HashSet<char> ZgadnieteLitery { get => zgadnieteLitery; set => zgadnieteLitery = value; }

        /// <summary>
        /// Wszystkie litery, które zostały podane w grze.
        /// </summary>
        public HashSet<char> WszystkiePodaneLitery { get => wszystkiePodaneLitery; set => wszystkiePodaneLitery = value; }
        

        /// <summary>
        /// Konstruktor parametryczny - ustawia gracza, kategorię i poziom,
        /// wczytuje słowa z plików i ustawia liczbę prób na podstawie poziomu.
        /// </summary>
        /// <param name="kategoria">Kategoria wybrana przez gracza.</param>
        /// <param name="poziom">Poziom wybrany przez gracza.</param>
        /// <param name="gracz">Gracz grający w grę.</param>
        public Gra(EnumKategoria kategoria, EnumPoziom poziom, Gracz gracz)
        {
            WybranaKategoria = kategoria;
            WybranyPoziom = poziom;
            Gracz = gracz;
            WczytajSlowaZPlikow();
            UstawLiczbeProbNaPodstawiePoziomu(poziom);
        }

        /// <summary>
        /// Delegat reprezentujący metodę obsługującą zdarzenie aktualizacji zgadniętych liter.
        /// </summary>
        /// <param name="zgadnieteLitery">Zgadnięte już litery.</param>
        public delegate void ZgadnieteLiteryUpdatedEventHandler(HashSet<char> zgadnieteLitery);

        /// <summary>
        /// Zdarzenie wywoływane po aktualizacji zgadniętych liter.
        /// </summary>
        public event ZgadnieteLiteryUpdatedEventHandler ZgadnieteLiteryUpdated;
        
        /// <summary>
        /// Metoda ustawiająca liczbę prób w grze na podstawie poziomu wybieranego przez gracza.
        /// </summary>
        /// <param name="poziom"></param>
        /// <exception cref="BlednyPoziomException"></exception>
        public override void UstawLiczbeProbNaPodstawiePoziomu(EnumPoziom poziom)
        {
            switch (poziom)
            {
                case EnumPoziom.Łatwy:
                    WybranyPoziom = EnumPoziom.Łatwy;
                    LiczbaProb = 12;
                    break;
                case EnumPoziom.Średni:
                    WybranyPoziom = EnumPoziom.Średni;
                    LiczbaProb = 8;
                    break;
                case EnumPoziom.Trudny:
                    WybranyPoziom = EnumPoziom.Trudny;
                    LiczbaProb = 5;
                    break;
                default:
                    throw new BlednyPoziomException("Nieprawidłowy poziom trudności.");

            }
        }

        /// <summary>
        /// Metoda, która ukrywa zgadywane słowo i zastępuje je odpowiednimi znakami.
        /// </summary>
        /// <returns>Ukryte słowo w postaci □. Znak spacji zastępuje pustym znakiem.</returns>
        public string UkryjSekretneSlowo()
        {
            StringBuilder ukryteSlowo = new StringBuilder();

            for (int i = 0; i < SekretneSlowo.Length; i++)
            {
                char litera = SekretneSlowo[i];

                if (litera == ' ')
                {
                    ukryteSlowo.Append(" ");
                }
                else
                {
                    bool czyZgadnieta = ZgadnieteLitery.Contains(litera);
                    ukryteSlowo.Append(czyZgadnieta ? litera.ToString() : "□");
                }
            }

            return ukryteSlowo.ToString().ToUpper().Trim();
        }


        /// <summary>
        /// Sprawdza, czy sekretne słowo zawiera podaną przez gracza literę.
        /// </summary>
        /// <param name="litera">Litera podawana przez gracza.</param>
        /// <returns>Prawda jeśli słowo zawiera podanę literę lub fałsz w przeciwnym wypadku.</returns>
        /// <remarks>
        /// Jeśli litera występuje w słowie - dodaje ją do zbioru zgadniętych liter i do zbioru wszystkich podanych liter.
        /// W przeciwnym razie zmniejsza liczbę prób o jedną i również dodaje literę do zbioru wszystkich podanych liter.
        /// W obu przypadkach wywołuje zdarzenie ZgadnieteLiteryUpdated.
        /// </remarks>
        public bool SprawdzCzyLiteraWystepujeWSlowie(char litera)
        {
            if (SekretneSlowo.Contains(litera)) 
            {
                ZgadnieteLitery.Add(litera);
                WszystkiePodaneLitery.Add(litera); 
                ZgadnieteLiteryUpdated?.Invoke(ZgadnieteLitery);
                return true;
            }
            else 
            {
                LiczbaProb--;
                WszystkiePodaneLitery.Add(litera); 
                ZgadnieteLiteryUpdated?.Invoke(ZgadnieteLitery);
                return false;
            }
        }

        
        /// <summary>
        /// Sprawdza, czy litera została już wcześniej podana.
        /// </summary>
        /// <param name="litera">Litera podawana przez gracza</param>
        /// <returns>Prawda, jeżeli zbiór WszystkiePodaneLitery zawiera podawaną przez gracza literę.
        /// Fałsz w przeciwnym wypadku</returns>
        public bool SprawdzCzyLiteraPodana(char litera)
        {
            return WszystkiePodaneLitery.Contains(litera);
        }

        /// <summary>
        /// Sprawdza, czy słowo zostało już w całości odgadnięte.
        /// </summary>
        /// <returns>Prawda, jeżeli słowo zostało w całości odgadnięte. Fałsz, jeżeli jeszcze nie.</returns>
        public bool CzyOdgadniete()
        {
            return SekretneSlowo.Where(litera => litera != ' ').All(litera => ZgadnieteLitery.Contains(litera));
        }

        /// <summary>
        /// Metoda inicjalizująca grę.
        /// </summary>
        /// <remarks>
        /// Zwiększa liczbę gier zagranych przez gracza, losuje sekretne słowo,
        /// czyści zbiory zawiuerające zgadnięte i podawane litery oraz ustawia ponownie liczbę prób.
        /// </remarks>
        public void InicjalizujGre()
        {
            Gracz.GryZagrane++;

            var dostepneSlowa = SlowaKategorii[WybranaKategoria];
            var random = new Random();
            SekretneSlowo = dostepneSlowa[random.Next(dostepneSlowa.Count)].ToLower();
            ZgadnieteLitery.Clear();
            WszystkiePodaneLitery.Clear();
            UstawLiczbeProbNaPodstawiePoziomu(WybranyPoziom);
        }
        

        /// <returns>
        /// Zbiór ze wzystkimi podanymi już przez gracza literami.
        /// </returns>
        public HashSet<char> PobierzWszystkiePodaneLitery()
        {
            return WszystkiePodaneLitery;
        }

    }

}
