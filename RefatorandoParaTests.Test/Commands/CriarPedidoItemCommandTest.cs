using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Commands;
using System;

namespace RefatorandoParaTests.Test.Commands
{
    [TestClass]
    public class CriarPedidoItemCommandTest
    {
        [TestMethod]
        public void Dado_um_command_com_produto_invalido_deve_invalidar_o_pedido_item()
        {
            var sut = new CriarPedidoItemCommand(Guid.NewGuid().ToString(), 1);

            sut.Validar();

            Assert.IsFalse(sut.IsValid);
        }

        [TestMethod]
        public void Dado_um_command_com_quantidade_de_produto_invalida_deve_invalidar_o_pedido_item()
        {
            var sut = new CriarPedidoItemCommand(Guid.NewGuid().ToString("N"), 0);

            sut.Validar();

            Assert.IsFalse(sut.IsValid);
        }

        [TestMethod]
        public void Dado_um_command_valido_deve_validar_o_pedido_item()
        {
            var sut = new CriarPedidoItemCommand(Guid.NewGuid().ToString("N"), 4);

            sut.Validar();

            Assert.IsTrue(sut.IsValid);
        }
    }
}
