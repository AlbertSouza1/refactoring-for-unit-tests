using Flunt.Notifications;
using Flunt.Validations;

namespace RefatorandoParaTestes.Entities
{
    public class PedidoItem : Entidade
    {
        public PedidoItem(Produto produto, int quantidade)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(produto, "Produto", "Produto inválido.")
                .IsGreaterThan(quantidade, 0, "Quantidade", "A quantidade do item deve ser maior que zero."));

            Produto = produto;
            Preco = Produto is null ? 0 : Produto.Preco;
            Quantidade = quantidade;
        }

        public Produto Produto { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Total => Preco * Quantidade;
    }
}
