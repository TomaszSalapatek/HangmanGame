using Wisielec;

namespace TestWisielec
{
    [TestClass]
    public class PodstawyGryTests
    {

        // Testy - wczytywanie słów z pliku
        [TestMethod]
        public void WczytajSlowaZPliku_PoprawnyPlik_Test()
        {
            // Arrange
            Gra graTestowa = new Gra(EnumKategoria.Sporty, EnumPoziom.Trudny, new Gracz("TestowyGracz"));
            string nazwaPliku = "testowyPlik.txt";
            string zawartoscPliku = "Kot\nPies\nKoń";

            File.WriteAllText(nazwaPliku, zawartoscPliku);

            // Act
            List<string> slowa = graTestowa.WczytajSlowaZPliku(nazwaPliku);

            // Assert
            CollectionAssert.AreEqual(new List<string> { "Kot", "Pies", "Koń" }, slowa);

            File.Delete(nazwaPliku);
        }


        [TestMethod]
        public void WczytajSlowaZPliku_PlikNieIstnieje_Test()
        {
            // Arrange
            Gra graTestowa = new Gra(EnumKategoria.Zwierzęta, EnumPoziom.Łatwy, new Gracz("GraczTestowy"));
            string nieistniejacyPlik = "nieistniejacyPlik.txt";

            // Act
            List<string> slowa = graTestowa.WczytajSlowaZPliku(nieistniejacyPlik);

            // Assert
            CollectionAssert.AreEqual(new List<string>(), slowa);
        }

    }
}
