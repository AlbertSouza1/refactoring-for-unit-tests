using Flunt.Notifications;
using Flunt.Validations;
using RefatorandoParaTestes.Enums;
using System;
using System.Collections.Generic;

namespace RefatorandoParaTestes.Entities
{
    public class Pedido : Entidade
    {
        public Pedido(Cliente cliente, decimal taxaEntrega, Desconto desconto)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(cliente, "Cliente", "Cliente inválido.")
                .IsGreaterOrEqualsThan(taxaEntrega, 0d, "TaxaEntrega", "A taxa de entrega não pode ser menor que zero."));
            
            Cliente = cliente;
            Data = DateTime.Now;
            Numero = Guid.NewGuid().ToString().Substring(0, 8);
            Itens = new List<PedidoItem>();
            Status = StatusPedido.AguardandoPagamento;
            TaxaEntrega = taxaEntrega;
            Desconto = desconto;

        }

        public Cliente Cliente { get; private set; }
        public DateTime Data { get; private set; }
        public string Numero { get; private set; }
        public IList<PedidoItem> Itens { get; private set; }
        public StatusPedido Status { get; private set; }
        public decimal TaxaEntrega { get; private set; }
        public Desconto Desconto { get; private set; }
        public decimal Total
        {
            get
            {
                decimal total = 0;

                foreach (var item in Itens)
                    total += item.Total;

                total += TaxaEntrega;
                total -= Desconto != null ? Desconto.Valor : 0;

                return total;
            }
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            var item = new PedidoItem(produto, quantidade);

            if(item.IsValid)
                Itens.Add(item);
        }

        public void Pagar(decimal valor)
        {
            if (valor == Total)
                Status = StatusPedido.AguardandoEntrega;
        }

        public void Cancelar() => Status = StatusPedido.Cancelado;
    }
}
