using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATINFO.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Metier.Tests
{
    [TestClass()]
    public class MaterielTests
    {
        Materiel materielTest = new Materiel(0, 1, "Test1", "Test2", "Test3");

        [TestMethod()]
        public void CreateTest()
        {
            materielTest.Create();  // L'id généré dans la base est différent de celui dans cet objet

            bool materielTrouve = false;
            int idGenere = 0;

            foreach (Materiel unMateriel in materielTest.FindAll())
            {
                if (unMateriel.IDCategorieMateriel == 1 && unMateriel.CodeBarre == "Test1" && unMateriel.Nom == "Test2" && unMateriel.Reference == "Test3")
                {
                    materielTrouve = true;
                    idGenere = unMateriel.IDMateriel;
                    break;
                }
            }

            Assert.IsTrue(materielTrouve);

            new Materiel(idGenere, 0, "", "", "").Delete();
        }

        [TestMethod()]
        public void UpdateTest()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {
            materielTest.Create();

            int idGenere = 0;

            foreach (Materiel unMateriel in materielTest.FindAll())
            {
                if (unMateriel.IDCategorieMateriel == 1 && unMateriel.CodeBarre == "Test1" && unMateriel.Nom == "Test2" && unMateriel.Reference == "Test3")
                {
                    idGenere = unMateriel.IDMateriel;
                    break;
                }
            }

            new Materiel(idGenere, 1, "", "", "").Delete();

            foreach (Materiel unMateriel in materielTest.FindAll())
            {
                if (unMateriel.IDCategorieMateriel == 1 && unMateriel.CodeBarre == "Test1" && unMateriel.Nom == "Test2" && unMateriel.Reference == "Test3")
                    Assert.Fail();
            }
        }
    }
}