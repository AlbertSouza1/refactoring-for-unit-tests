using Flunt.Notifications;
using Flunt.Validations;
using RefatorandoParaTestes.Commands.Interfaces;

namespace RefatorandoParaTestes.Commands
{
    public class CriarPedidoItemCommand : Notifiable<Notification>, ICommand
    {
        public CriarPedidoItemCommand(string produtoGuid, int quantidade) 
        {
            ProdutoGuid = produtoGuid;
            Quantidade = quantidade;
        }

        public string ProdutoGuid { get; set; }
        public int Quantidade { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsTrue(ProdutoGuid.Length == 32, "Produto")
                .IsGreaterThan(Quantidade, 0, "Quantidade"));
        }
    }
}
