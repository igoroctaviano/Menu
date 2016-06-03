using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Menu.Algoritmos;

namespace Menu.Algoritmos
{
    [TestClass()]
    public class tp_pdTests
    {
        private tp_pd menu;

        public tp_pdTests()
        {
            CriarInstancia();
        }

        [TestMethod()]
        public void CriarInstancia()
        {
            this.menu = new tp_pd(CreateMockStream());
        }

        // Mock.
        private static StreamReader CreateMockStream()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("2 1 5");
            stringBuilder.AppendLine("3 5");
            stringBuilder.AppendLine("3 5 20");
            stringBuilder.AppendLine("2 5");
            stringBuilder.AppendLine("18 6");
            stringBuilder.AppendLine("1 1");
            stringBuilder.AppendLine("3 3");
            stringBuilder.AppendLine("2 3");
            stringBuilder.AppendLine("0 0 0");

            var streamReader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(stringBuilder.ToString())));
            return streamReader;
        }

        /// <summary>
        /// Resultado com todos pratos que Alfred pode cozinhar.
        /// </summary>
        [TestMethod]
        public void PratosCorretosASeremCozinhados()
        {
            this.menu.Resolver();
            Assert.AreEqual(1, this.menu.itens[0]);
            Assert.AreEqual(5, this.menu.itens[1]);
            Assert.AreEqual(1, this.menu.itens[2]);
        }

        /// <summary>
        /// Valor maximo do lucro alcancavel.
        /// </summary>
        [TestMethod]
        public void ValorMaxLucroCorreto()
        {
            this.menu.Resolver();
            Assert.AreEqual(this.menu.melhores.ElementAt(0), 0);
            Assert.AreEqual(this.menu.melhores.ElementAt(1), 13);
        }

        /// <summary>
        /// Número de dias que Alfred quer planejar.
        /// k(1 ≤ k ≤ 21)
        /// </summary>
        [TestMethod]
        public void NumeroDeDiasEntreUmEVinteUm()
        {
            this.menu.Resolver();
            Assert.IsTrue(this.menu.k >= 1 && this.menu.k <= 21);
        }

        /// <summary>
        /// O número de pratos n(1 ≤ n ≤ 50) que ele pode cozinhar.
        /// n(1 ≤ n ≤ 50) 
        /// </summary>
        [TestMethod]
        public void NumeroDePratosEntreUmECinquenta()
        {
            this.menu.Resolver();
            Assert.IsTrue(this.menu.n >= 1 && this.menu.n <= 50);
        }

        /// <summary>
        /// Orçamento.
        /// m(0 ≤ m ≤ 100)
        /// </summary>
        [TestMethod]
        public void OrcamentoEntreZeroECem()
        {
            this.menu.Resolver();
            Assert.IsTrue(this.menu.m >= 0 && this.menu.m <= 100);
        }

        /// <summary>
        /// Custo do i-ésimo prato.
        /// c(1 ≤ c ≤ 50)
        /// </summary>
        [TestMethod]
        public void CustoEntreUmECinquenta()
        {
            this.menu.Resolver();
            for (int i = 0; i < this.menu.custos.Length; i++)
                Assert.IsTrue(this.menu.custos[i] >= 1 && this.menu.custos[i] <= 50);
        }

        /// <summary>
        /// Lucro do i-ésimo prato.
        /// v(1 ≤ v ≤ 10000)
        /// </summary>
        [TestMethod]
        public void LucroEntreUmEDezMil()
        {
            this.menu.Resolver();
            for (int i = 0; i < this.menu.lucros.Length; i++)
                Assert.IsTrue(this.menu.lucros[i] >= 1 && this.menu.lucros[i] <= 10000);
        }

        /// <summary>
        /// Testar final da entrada.
        /// (k = n = m = 0)
        /// </summary>
        [TestMethod]
        public void FinalDaEntradaRetornaTrue()
        {
            this.menu.Resolver();
            Assert.IsTrue(this.menu.numeroDeDias
                + this.menu.numeroDePratos
                + this.menu.orcamento
                == 0);
        }
    }
}