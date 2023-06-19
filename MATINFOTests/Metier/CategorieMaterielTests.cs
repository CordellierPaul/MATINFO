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
    public class CategorieMaterielTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            CategorieMateriel categorieTest = new CategorieMateriel(0, "TestNomDeLaCategorie");

            categorieTest.Create();

            bool materielTrouve = false;

            foreach (CategorieMateriel uneCategorie in categorieTest.FindAll())
            {
                Console.WriteLine(uneCategorie.Nom);

                if (uneCategorie.Nom == "TestNomDeLaCategorie")
                {
                    materielTrouve = true;
                    break;
                }
            }
                
            Assert.IsTrue(materielTrouve);

            categorieTest.Delete();
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