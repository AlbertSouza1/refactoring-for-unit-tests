using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefatorandoParaTestes.Commands;
using RefatorandoParaTestes.Entities;
using RefatorandoParaTestes.Handlers;
using RefatorandoParaTestes.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace RefatorandoParaTests.Test.Handlers
{
    [TestClass]
    public class PedidoHandlerTest
    {
        private readonly List<Produto> _produtosValidos;
        private readonly Mock<IClienteRepository> _clienteRepository;
        private readonly Mock<ITaxaEntregaRepository> _taxaEntregaRepository;
        private readonly Mock<IDescontoRepository> _descontoRepository;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly Mock<IPedidoRepository> _pedidoRepository;

        public PedidoHandlerTest()
        {
            _produtosValidos = new List<Produto>()
            {
                new Produto("Produto 1", 10, true),
                new Produto("Produto 2", 10, true),
                new Produto("Produto 3", 10, true),
            };

            _clienteRepository = new Mock<IClienteRepository>();
            _clienteRepository.Setup(x => x.Obter(It.IsAny<string>())).Returns(new Cliente("José", "email@email.com"));

            _taxaEntregaRepository = new Mock<ITaxaEntregaRepository>();

            _descontoRepository = new Mock<IDescontoRepository>();
            _descontoRepository.Setup(x => x.Obter(It.IsAny<string>())).Returns(new Desconto(1m, DateTime.Today.AddDays(1)));

            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoRepository.Setup(x => x.Obter(It.IsAny<IEnumerable<Guid>>())).Returns(_produtosValidos);

            _pedidoRepository = new Mock<IPedidoRepository>();
        }

        [TestMethod]
        public void Dado_um_command_com_cliente_invalido_deve_retornar_falha()
        {
            var command = new CriarPedidoCommand("", "12345678", "", null);

            var sut = new PedidoHandler(null, null, null, null, null);

            var result = sut.Handle(command);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual("Cliente inválido", result.Mensagem);
        }

        [TestMethod]
        public void Dado_um_command_com_CEP_invalido_deve_retornar_falha()
        {
            var command = new CriarPedidoCommand("12345678910", "", "", null);

            var sut = new PedidoHandler(null, null, null, null, null);

            var result = sut.Handle(command);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual("CEP inválido", result.Mensagem);
        }

        [TestMethod]
        public void Deve_retornar_falha_caso_pedido_seja_invalido()
        {
            var itens = new List<CriarPedidoItemCommand>()
            {
                new CriarPedidoItemCommand(_produtosValidos[0].Id.ToString(), 1),
                new CriarPedidoItemCommand(_produtosValidos[1].Id.ToString(), 2),
                new CriarPedidoItemCommand(_produtosValidos[2].Id.ToString(), 3),
            };

            var command = new CriarPedidoCommand("12345678910", "15710000", "1", itens);

            _taxaEntregaRepository.Setup(x => x.Obter(It.IsAny<string>())).Returns(-5m);

            var sut = new PedidoHandler(_clienteRepository.Object,
                                        _taxaEntregaRepository.Object,
                                        _descontoRepository.Object,
                                        _produtoRepository.Object,
                                        _pedidoRepository.Object);

            var result = sut.Handle(command);

            Assert.IsFalse(result.Sucesso);
            Assert.AreEqual("Não foi possível gerar o pedido.\r\nA taxa de entrega não pode ser menor que zero.", result.Mensagem);
        }

        [TestMethod]
        public void Deve_gravar_o_pedido_se_dados_vierem_corretos()
        {
            var itens = new List<CriarPedidoItemCommand>()
            {
                new CriarPedidoItemCommand(_produtosValidos[0].Id.ToString(), 1),
                new CriarPedidoItemCommand(_produtosValidos[1].Id.ToString(), 2),
                new CriarPedidoItemCommand(_produtosValidos[2].Id.ToString(), 3),
            };

            var command = new CriarPedidoCommand("12345678910", "15710000", "1", itens);

            _taxaEntregaRepository.Setup(x => x.Obter(It.IsAny<string>())).Returns(10m);

            var sut = new PedidoHandler(_clienteRepository.Object,
                                        _taxaEntregaRepository.Object,
                                        _descontoRepository.Object,
                                        _produtoRepository.Object,
                                        _pedidoRepository.Object);

            var result = sut.Handle(command);

            Assert.IsTrue(result.Sucesso);
            Assert.AreEqual("Pedido gerado com sucesso!", result.Mensagem);
        }
    }
}
