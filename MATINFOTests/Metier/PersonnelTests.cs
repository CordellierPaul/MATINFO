using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATINFO.Metier;

namespace MATINFO.Metier.Tests
{
    [TestClass()]
    public class PersonnelTests
    {
        Personnel personnelTest = new Personnel(0, "TestNom54312", "TestPrenom54312", "TestEmail54312");

        [TestMethod()]
        public void CreateTest()
        {
            personnelTest.Create();     // L'id généré dans la base est différent de celui dans cet objet

            bool materielTrouve = false;
            int idGenere = 0;

            foreach (Personnel unPersonnel in personnelTest.FindAll())
            {
                if (unPersonnel.Nom == "TestNom54312" && unPersonnel.Prenom == "TestPrenom54312" && unPersonnel.Email == "TestEmail54312")
                {
                    materielTrouve = true;
                    idGenere = unPersonnel.IDPersonnel;
                    break;
                }
            }

            Assert.IsTrue(materielTrouve);

            new Personnel(idGenere, "", "", "").Delete();
        }

        [TestMethod()]
        public void UpdateTest()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {
            personnelTest.Create();

            int idGenere = 0;

            foreach (Personnel unPersonnel in personnelTest.FindAll())
            {
                if (unPersonnel.Nom == "TestNom54312" && unPersonnel.Prenom == "TestPrenom54312" && unPersonnel.Email == "TestEmail54312")
                {
                    idGenere = unPersonnel.IDPersonnel;
                    break;
                }
            }

            new Personnel(idGenere, "", "", "").Delete();

            foreach (Personnel unPersonnel in personnelTest.FindAll())
            {
                if (unPersonnel.Nom == "TestNom54312" && unPersonnel.Prenom == "TestPrenom54312" && unPersonnel.Email == "TestEmail54312")
                    Assert.Fail();
            }
        }
    }
}