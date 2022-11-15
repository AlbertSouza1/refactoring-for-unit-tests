using Flunt.Notifications;
using RefatorandoParaTestes.Commands;
using RefatorandoParaTestes.Commands.Interfaces;
using RefatorandoParaTestes.Commands.Results;
using RefatorandoParaTestes.Entities;
using RefatorandoParaTestes.Handlers.Interfaces;
using RefatorandoParaTestes.Helpers;
using RefatorandoParaTestes.Repositories.Interfaces;
using System;
using System.Linq;

namespace RefatorandoParaTestes.Handlers
{
    public class PedidoHandler : Notifiable<Notification>, IHandler<CriarPedidoCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ITaxaEntregaRepository _taxaEntregaRepository;
        private readonly IDescontoRepository _descontoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoHandler(IClienteRepository clienteRepository,
                             ITaxaEntregaRepository taxaEntregaRepository,
                             IDescontoRepository descontoRepository,
                             IProdutoRepository produtoRepository,
                             IPedidoRepository pedidoRepository)
        {
            _clienteRepository = clienteRepository;
            _taxaEntregaRepository = taxaEntregaRepository;
            _descontoRepository = descontoRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public ICommandResult Handle(CriarPedidoCommand command)
        {
            command.Validar();

            if (!command.IsValid)
                return new FalhaResult(NotificationsHelper.ObterMensagens(command.Notifications));

            var pedido = ObterPedido(command);

            AddNotifications(pedido.Notifications);

            if (!IsValid)
                return new FalhaResult($"Não foi possível gerar o pedido.{Environment.NewLine}{Notifications.FirstOrDefault().Message}");

            _pedidoRepository.Salvar(pedido);

            return new SucessoResult("Pedido gerado com sucesso!");
        }

        private Pedido ObterPedido(CriarPedidoCommand command)
        {
            var cliente = _clienteRepository.Obter(command.CpfCliente);
            var taxaEntrega = _taxaEntregaRepository.Obter(command.CEP);
            var desconto = _descontoRepository.Obter(command.CodigoPromocional);
            var produtos = _produtoRepository.Obter(ExtrairGuids.Extrair(command.Itens)).ToList();

            var pedido = new Pedido(cliente, taxaEntrega, desconto);

            foreach (var item in command.Itens)
            {
                var produto = produtos.Where(x => x.Id.ToString() == item.ProdutoGuid).FirstOrDefault();
                pedido.AdicionarItem(produto, item.Quantidade);
            }

            return pedido;
        }
    }
}
