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

        [TestMethod()]
        public void CreateTest()
        {
            Materiel materielTest = new Materiel(0, 5, "Test1", "Test2", "Test3");

            materielTest.Create();

            bool materielTrouve = false;

            foreach (Materiel unMateriel in materielTest.FindAll())
            {
                if (unMateriel.IDCategorieMateriel == 5 && unMateriel.CodeBarre == "Test1" && unMateriel.Nom == "Test2" && unMateriel.Reference == "Test3")
                {
                    materielTrouve = true;
                    break;
                }
            }

            Assert.IsTrue(materielTrouve);

            materielTest.Delete();
        }

        [TestMethod()]
        public void UpdateTest()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {

        }
    }
}