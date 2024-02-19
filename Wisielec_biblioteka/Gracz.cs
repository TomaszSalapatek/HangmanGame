using System.ComponentModel.DataAnnotations;

namespace Wisielec
{
    /// <summary>
    /// Klasa reprezentująca gracza.
    /// </summary>
    /// <remarks>
    /// Dziedziczy po interfejsach: IEquatable<Gracz> i IComparable<Gracz>
    /// </remarks>
    public class Gracz : IEquatable<Gracz>, IComparable<Gracz>
    {

        private string nazwa;
        private int gryZagrane;
        private int gryWygrane;
        private double odsetekSkutecznosci;

        /// <summary>
        /// Konstruktor nieparametryczny gracza.
        /// </summary>
        public Gracz()
        {
            
        }

        /// <summary>
        /// Konstruktor gracza. Ustawia: Nazwę, GryZagrane, GryWygrane oraz OdsetekSkuteczności.
        /// </summary>
        /// <param name="nazwa">Nazwa, którą będzie miał gracz.</param>
        public Gracz(string nazwa)
        {
            Nazwa = nazwa;
            GryZagrane = 0;
            GryWygrane = 0;
            OdsetekSkutecznosci = 0;
        }

        /// <summary>
        /// Nazwa, którą będzie miał gracz. Jednocześnie klucz unikalny w bazie danych.
        /// </summary>
        [Key]
        public string Nazwa { get => nazwa; set => nazwa = value; }
        
        /// <summary>
        /// Liczba gier, które rozegrał gracz.
        /// </summary>
        public int GryZagrane { get => gryZagrane; set => gryZagrane = value; }
        
        /// <summary>
        /// Liczba gier, które wygrał gracz.
        /// </summary>
        public int GryWygrane { get => gryWygrane; set => gryWygrane = value; }
        
        /// <summary>
        /// Stosunek liczby wygranych gier do liczby wszystkich rozegranych gier.
        /// </summary>
        public double OdsetekSkutecznosci { get => odsetekSkutecznosci; set => odsetekSkutecznosci = value; }

        
        #region DataBase

        /// <summary>
        /// Metoda, która zapisuje do bazy danych wynik gracza. Jeśli gracz jeszcze nie istnieje w bazie, dodaje go.
        /// </summary>
        public void ZapiszDoBazy()
        {
            
            using (var db = new HistoriaGraczyDbContext())
            {
                Gracz istniejacyGracz = db.Gracze.FirstOrDefault(g => g.Nazwa == Nazwa);


                if (istniejacyGracz != null)
                {
                    // Gracz już istnieje - aktualizuje jego statystyki
                    istniejacyGracz.GryZagrane += GryZagrane;
                    istniejacyGracz.GryWygrane += GryWygrane;
                    istniejacyGracz.ObliczSkutecznosc();
                }
                else
                {
                    // Gracz jeszcze nie istnieje w bazie - dodaje go
                    db.Gracze.Add(this);
                }

                db.SaveChanges();
            }
        }

        #endregion Database

        
        /// <summary>
        /// Metoda oblicza stosunek gier wygranych do wszystkich gier rozegranych i mnoży go razy 100, aby otrzymać wynik procentowy.
        /// </summary>
        /// <returns>Procentową skuteczność gracza lub jeśli gracz rozegrał 0 gier, zwraca 0.</returns>
        public double ObliczSkutecznosc()
        {
            if(GryZagrane != 0)
            {
                OdsetekSkutecznosci = (double) GryWygrane / GryZagrane;
                return 100 * Math.Round(OdsetekSkutecznosci, 4);
            }
            else
            {
                return 0;
            }
            
        }

        public override string ToString()
        {
            return $"Gracz: {Nazwa},\ngry zagrane: {GryZagrane},  gry wygrane: {gryWygrane}," +
                $"  odsetek skuteczności: {ObliczSkutecznosc()}%";
        }

        
        /// <summary>
        /// Porównuje, czy gracze są identyczni na podstawie ich nazwy.
        /// </summary>
        /// <param name="other">Inny gracz do porównania.</param>
        /// <returns></returns>
        public bool Equals(Gracz? other)
        {
            return Nazwa.Equals(other.Nazwa);
        }

        
        /// <summary>
        /// Porównuje ze sobą graczy na podstawie ich nazwy.
        /// </summary>
        /// <param name="other">Inny gracz do porównania.</param>
        /// <returns></returns>
        public int CompareTo(Gracz? other)
        {
            return Nazwa.CompareTo(other.Nazwa);
        }
    }
}
