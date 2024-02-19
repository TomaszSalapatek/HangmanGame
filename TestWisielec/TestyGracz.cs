
namespace Wisielec.Tests
{
    [TestClass]
    public class GraczTests
    {

        // Testy - obliczanie odsetka skutecznoœci
        [TestMethod]
        public void OdsetekSkutecznosci_GryZagraneNiezerowe_Test()
        {
            // Arrange
            Gracz gracz = new Gracz("TestowyGracz");
            gracz.GryZagrane = 10;
            gracz.GryWygrane = 5;

            // Act
            double odsetek = gracz.ObliczSkutecznosc();

            // Assert
            Assert.AreEqual(50, odsetek);
        }

        [TestMethod]
        public void OdsetekSkutecznosci_GryZagraneZerowe_Test()
        {
            // Arrange
            Gracz gracz = new Gracz("TestowyGracz");
            gracz.GryZagrane = 0;
            gracz.GryWygrane = 0;

            // Act
            double odsetek = gracz.ObliczSkutecznosc();

            // Assert
            Assert.AreEqual(0.0, odsetek);
        }

        [TestMethod]
        public void ToString_Formatowanie_Test()
        {
            // Arrange
            Gracz gracz = new Gracz("TestowyGracz");
            gracz.GryZagrane = 20;
            gracz.GryWygrane = 15;

            // Act
            string result = gracz.ToString();

            // Assert
            string expected = "Gracz: TestowyGracz,\ngry zagrane: 20,  gry wygrane: 15,  odsetek skutecznoœci: 75%";
            Assert.AreEqual(expected, result);
        }

        
    }
}