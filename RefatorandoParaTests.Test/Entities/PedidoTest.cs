using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Entities;
using RefatorandoParaTestes.Enums;
using System;
using System.Linq;

namespace RefatorandoParaTests.Test.Entities
{
    [TestClass]
    public class PedidoTest
    {
        private readonly Cliente _cliente = new Cliente("Nome", "email@email.com");
        private readonly Produto _produto = new Produto("Produto 1", 10, true);
        private readonly Desconto _desconto = new Desconto(5, DateTime.Now.AddDays(1));

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_gerar_um_numero_com_8_caracteres()
        {
            //Arrange & Act
            var sut = new Pedido(_cliente, 0, _desconto);

            //Assert
            Assert.AreEqual(8, sut.Numero.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            //Arrange & Act
            var sut = new Pedido(_cliente, 0, _desconto);

            //Assert
            Assert.AreEqual(StatusPedido.AguardandoPagamento, sut.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pagamento_do_pedido_nao_deve_alterar_status_se_total_nao_for_igual_ao_valor_do_pagamento()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);
            sut.AdicionarItem(_produto, 1);

            //Act
            sut.Pagar(0);

            //Assert
            Assert.IsFalse(sut.Status == StatusPedido.AguardandoEntrega);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);
            sut.AdicionarItem(_produto, 1);

            //Act
            sut.Pagar(10);

            //Assert
            Assert.AreEqual(StatusPedido.AguardandoEntrega, sut.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);

            //Act
            sut.Cancelar();

            //Assert
            Assert.AreEqual(StatusPedido.Cancelado, sut.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);

            //Act
            sut.AdicionarItem(null, 1);

            //Assert
            Assert.AreEqual(0, sut.Itens.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_item_com_quantidade_zerada_de_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);

            //Act
            sut.AdicionarItem(_produto, 0);

            //Assert
            Assert.AreEqual(0, sut.Itens.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_calcular_o_total_quando_nao_ha_desconto_nem_taxa()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(20, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_desconsiderar_desconto_quando_desconto_esta_expirado()
        {
            //Arrange
            var descontoExpirado = new Desconto(5, DateTime.Now.AddDays(-1));
            var sut = new Pedido(_cliente, 0, descontoExpirado);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(20, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_desconsiderar_desconto_invalido()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, null);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(20, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_calcular_o_total_com_desconto_valido()
        {
            //Arrange
            var sut = new Pedido(_cliente, 0, _desconto);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(15, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_calcular_o_total_com_taxa_de_entrega()
        {
            //Arrange
            var sut = new Pedido(_cliente, 5, null);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(25, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_deve_calcular_o_total_com_desconto_e_taxa_de_entrega()
        {
            //Arrange
            var sut = new Pedido(_cliente, 5, _desconto);

            //Act
            sut.AdicionarItem(_produto, 2);

            //Assert
            Assert.AreEqual(20, sut.Total);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_cliente_deve_ser_invalidado()
        {
            //Arrange & //Act
            var sut = new Pedido(null, 0, _desconto);

            //Assert
            Assert.IsFalse(sut.IsValid);
            Assert.AreEqual("Cliente inválido.", sut.Notifications.FirstOrDefault().Message);
        }

    }
}
