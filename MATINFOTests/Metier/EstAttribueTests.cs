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