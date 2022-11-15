using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Commands;
using RefatorandoParaTestes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefatorandoParaTests.Test.Helpers
{
    [TestClass]
    public class ExtrairGuidsTest
    {
        [TestMethod]
        public void Dado_uma_lista_de_itens_deve_retornar_os_guids()
        {
            var guidProduto1 = Guid.NewGuid();
            var guidProduto2 = Guid.NewGuid();

            var itens = new List<CriarPedidoItemCommand>()
            {
                new CriarPedidoItemCommand(guidProduto1.ToString(), 1),
                new CriarPedidoItemCommand(guidProduto2.ToString(), 2)
            };

            var result = ExtrairGuids.Extrair(itens).ToList();

            Assert.AreEqual(guidProduto1, result[0]);
            Assert.AreEqual(guidProduto2, result[1]);
        }

        [TestMethod]
        public void Dado_uma_lista_vazia_de_itens_deve_retornar_uma_lista_de_guid_vazia()
        {
            var result = ExtrairGuids.Extrair(new List<CriarPedidoItemCommand>()).ToList();

            Assert.IsTrue(result.Count == 0);
        }
    }
}
