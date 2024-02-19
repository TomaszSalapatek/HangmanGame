

namespace Wisielec
{
    /// <summary>
    /// Klasa dziedzicząca po klasie Exception, stworzona do wyrzucenia błędu poziomu.
    /// </summary>
    public class BlednyPoziomException : Exception
    {
        /// <summary>
        /// Konstruktor nieparametryczny nie przyjmujący żadnych patametrów
        /// </summary>
        public BlednyPoziomException()
        {
            
        }

        /// <summary>
        /// Konstruktor parametryczny do przekazywania odpowiedniej wiadomości
        /// </summary>
        /// <param name="message">Wiadomość na temat błędu</param>
        public BlednyPoziomException(string message) : base(message)
        {
            
        }
    }
}
