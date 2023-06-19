using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATINFO.Metier;

namespace MATINFO.Metier.Tests
{
    [TestClass()]
    public class CategorieMaterielTests
    {
        CategorieMateriel categorieTest = new CategorieMateriel(0, "TestNomDeLaCategorie54312");

        [TestMethod()]
        public void CreateTest()
        {
            categorieTest.Create();     // L'id généré dans la base est différent de celui dans cet objet

            bool materielTrouve = false;
            int idGenere = 0;

            foreach (CategorieMateriel uneCategorie in categorieTest.FindAll())
            {
                if (uneCategorie.Nom == "TestNomDeLaCategorie54312")
                {
                    materielTrouve = true;
                    idGenere = uneCategorie.IDCategorieMateriel;
                    break;
                }
            }
                
            Assert.IsTrue(materielTrouve);

            new CategorieMateriel(idGenere, "").Delete();
        }

        [TestMethod()]
        public void UpdateTest()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {
            categorieTest.Create();

            int idGenere = 0;

            foreach (CategorieMateriel uneCategorie in categorieTest.FindAll())
            {
                if (uneCategorie.Nom == "TestNomDeLaCategorie54312")
                {
                    idGenere = uneCategorie.IDCategorieMateriel;
                    break;
                }
            }

            new CategorieMateriel(idGenere, "").Delete();

            foreach (CategorieMateriel uneCategorie in categorieTest.FindAll())
            {
                if (uneCategorie.Nom == "TestNomDeLaCategorie54312")
                    Assert.Fail();
            }
        }
    }
}