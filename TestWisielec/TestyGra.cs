
namespace Wisielec
{
    [TestClass]
    public class TestyGra
    {
        // Testy - ustawianie liczby prób

        [TestMethod]
        public void UstawLiczbeProbNaPodstawiePoziomu_Latwy_Test()
        {
            // Arrange
            EnumPoziom poziom = EnumPoziom.Łatwy;
            Gra graTestowa = new Gra(EnumKategoria.Rośliny, poziom, new Gracz("TestowyGracz"));
            int iloscProbTest = 12;

            // Act
            graTestowa.UstawLiczbeProbNaPodstawiePoziomu(graTestowa.WybranyPoziom);

            // Assert
            Assert.AreEqual(iloscProbTest, graTestowa.LiczbaProb);
        }

        [TestMethod]
        public void UstawLiczbeProbNaPodstawiePoziomu_Sredni_Test()
        {
            // Arrange
            EnumPoziom poziom = EnumPoziom.Średni;
            Gra graTestowa = new Gra(EnumKategoria.Rośliny, poziom, new Gracz("TestowyGracz"));
            int iloscProbTest = 8;

            // Act
            graTestowa.UstawLiczbeProbNaPodstawiePoziomu(graTestowa.WybranyPoziom);

            // Assert
            Assert.AreEqual(iloscProbTest, graTestowa.LiczbaProb);
        }

        [TestMethod]
        public void UstawLiczbeProbNaPodstawiePoziomu_Trudny_Test()
        {
            // Arrange
            EnumPoziom poziom = EnumPoziom.Trudny;
            Gra graTestowa = new Gra(EnumKategoria.Rośliny, poziom, new Gracz("TestowyGracz"));
            int iloscProbTest = 5;

            // Act
            graTestowa.UstawLiczbeProbNaPodstawiePoziomu(graTestowa.WybranyPoziom);

            // Assert
            Assert.AreEqual(iloscProbTest, graTestowa.LiczbaProb);
        }


        // Testy - ukrywanie słowa
        [TestMethod]
        public void UkryjSekretneSlowo_BezSpacji_Test()
        {
            // Assert
            Gra graTestowa = new Gra(EnumKategoria.Państwa, EnumPoziom.Średni, new Gracz("TestowyGracz"));
            graTestowa.SekretneSlowo = "Indonezja";
            string zakryte = "□□□□□□□□□";

            // Act, Assert
            Assert.AreEqual(zakryte, graTestowa.UkryjSekretneSlowo());
        }

        [TestMethod]
        public void UkryjSekretneSlowo_Spacja_Test()
        {
            // Assert
            Gra graTestowa = new Gra(EnumKategoria.Zawody, EnumPoziom.Łatwy, new Gracz("TestowyGracz"));
            graTestowa.SekretneSlowo = "skok w dal";
            string zakryte = "□□□□ □ □□□";

            // Act, Assert
            Assert.AreEqual(zakryte, graTestowa.UkryjSekretneSlowo());
        }


        // Testy - czy słowo już odgadnięte
        [TestMethod]
        public void CzyOdgadniete_Odgatniete_Test()
        {
            //  Arrange
            Gra graTestowa = new Gra(EnumKategoria.Zawody, EnumPoziom.Łatwy, new Gracz("TestowyGracz"));
            graTestowa.ZgadnieteLitery = new HashSet<char> { 'k', 'o', 'a', 'l' };
            graTestowa.SekretneSlowo = "koala";

            // Act, Assert
            Assert.IsTrue(graTestowa.CzyOdgadniete());
        }

        public void CzyOdgadniete_NieOdgatniete_Test()
        {
            //  Arrange
            Gra graTestowa = new Gra(EnumKategoria.Zawody, EnumPoziom.Łatwy, new Gracz("TestowyGracz"));
            graTestowa.ZgadnieteLitery = new HashSet<char> { 'k', 'o', 'a', 'l' };
            graTestowa.SekretneSlowo = "panda";

            // Act, Assert
            Assert.IsFalse(graTestowa.CzyOdgadniete());
        }

    }

}
