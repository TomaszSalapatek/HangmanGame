

namespace Wisielec
{
    /// <summary>
    /// Klasa dziedzicząca po klasie Exception, stworzona do wyrzucenia błędu nazwy pliku.
    /// </summary>
    public class BladNazwyPlikuException : Exception
    {
        /// <summary>
        /// Konstruktor nieparametryczny nie przyjmujący żadnych patametrów
        /// </summary>
        public BladNazwyPlikuException()
        {
                
        }
        
        // <summary>
        /// Konstruktor parametryczny do przekazywania odpowiedniej wiadomości
        /// </summary>
        /// <param name="message">Wiadomość na temat błędu</param>
        public BladNazwyPlikuException(string message) : base(message)
        {
                
        }
    }
}
