using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATINFO.Metier;

namespace MATINFO.Metier.Tests
{
    [TestClass()]
    public class EstAttribueTests
    {
        EstAttribue uneAttribution = new EstAttribue(1, 1, "14/06/2024", "Exemple c0mmentaire -95412");
        
        [TestMethod()]
        public void CreateTest()
        {
            uneAttribution.Create();

            bool materielTrouve = false;

            foreach (EstAttribue unAttribut in uneAttribution.FindAll())
            {
                if (unAttribut.IDMateriel == 1 && unAttribut.IDPersonnel == 1 && unAttribut.DateAttribution == "14/06/2024" && unAttribut.Commentaire == "Exemple c0mmentaire -95412")
                {
                    materielTrouve = true;
                    break;
                }
            }

            Assert.IsTrue(materielTrouve);

            uneAttribution.Delete();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            uneAttribution.Create();

            EstAttribue uneAutreAttribution = uneAttribution;

            uneAutreAttribution.DateAttribution = "12/06/2022";

            uneAutreAttribution.Commentaire = "Un autre c0mmentaire 62347";

            uneAutreAttribution.Update();

            bool materielTrouve = false;

            foreach (EstAttribue unAttribut in uneAutreAttribution.FindAll())
            {
                if (unAttribut.IDMateriel == 1 && unAttribut.IDPersonnel == 1 && unAttribut.DateAttribution == "12/06/2022" && unAttribut.Commentaire == "Un autre c0mmentaire 62347")
                {
                    materielTrouve = true;
                    break;
                }
            }

            Assert.IsTrue(materielTrouve);

            uneAutreAttribution.Delete();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            uneAttribution.Create();

            uneAttribution.Delete();

            foreach (EstAttribue unAttribut in uneAttribution.FindAll())
            {
                if (unAttribut.IDMateriel == 1 && unAttribut.IDPersonnel == 1 && unAttribut.DateAttribution == "14/06/2024" && unAttribut.Commentaire == "Exemple c0mmentaire -95412")
                    Assert.Fail();
            }
        }
    }
}