using System.Data.Entity;

namespace Wisielec
{
    /// <summary>
    /// Klasa dziedzicząca po DbContext do stworzenia bazy danych ze statystykami graczy.
    /// </summary>
    public class HistoriaGraczyDbContext : DbContext
    {
        /// <summary>
        /// Tabela zawierająca informacje o statystykach graczy.
        /// </summary>
        public DbSet<Gracz> Gracze { get; set; }
    }
}
