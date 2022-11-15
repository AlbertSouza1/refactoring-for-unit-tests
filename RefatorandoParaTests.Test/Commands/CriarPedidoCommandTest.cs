using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Commands;
using System.Collections.Generic;

namespace RefatorandoParaTests.Test.Commands
{
    [TestClass]
    public class CriarPedidoCommandTest
    {
        [TestMethod]
        [DataRow("", "15710000")]
        [DataRow("14221112312", "")]
        [DataRow("", "")]
        public void Dado_um_command_invalido_o_pedido_nao_deve_ser_gerado(string cliente, string cep)
        {
            var sut = new CriarPedidoCommand(cliente, cep, "123", new List<CriarPedidoItemCommand>());

            sut.Validar();

            Assert.IsFalse(sut.IsValid);
        }

        [TestMethod]       
        public void Dado_um_command_valido_o_pedido_deve_ser_gerado()
        {
            var sut = new CriarPedidoCommand("14221112312", "15710000", "123", new List<CriarPedidoItemCommand>());

            sut.Validar();

            Assert.IsTrue(sut.IsValid);
        }
    }
}
