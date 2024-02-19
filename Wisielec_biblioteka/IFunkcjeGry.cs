

namespace Wisielec
{
    /// <summary>
    /// Interfejs, który zawiera metody używane w klasie Gra
    /// </summary>
    public interface IFunkcjeGry
    {
        public string UkryjSekretneSlowo();
        public bool SprawdzCzyLiteraWystepujeWSlowie(char litera);
        public bool SprawdzCzyLiteraPodana(char litera);
        public bool CzyOdgadniete();
        public void InicjalizujGre();
        public HashSet<char> PobierzWszystkiePodaneLitery();

    }
}
