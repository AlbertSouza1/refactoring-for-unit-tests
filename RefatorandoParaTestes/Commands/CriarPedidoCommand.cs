using Flunt.Notifications;
using Flunt.Validations;
using RefatorandoParaTestes.Commands.Interfaces;
using System.Collections.Generic;

namespace RefatorandoParaTestes.Commands
{
    public class CriarPedidoCommand : Notifiable<Notification>, ICommand
    {
        public CriarPedidoCommand(string cpfCliente, string cep, string codigoPromocional, List<CriarPedidoItemCommand> itens)
        {
            CpfCliente = cpfCliente;
            CEP = cep;
            CodigoPromocional = codigoPromocional;
            Itens = itens;
        }

        public string CpfCliente { get; set; }
        public string CEP { get; set; }
        public string CodigoPromocional { get; set; }
        public List<CriarPedidoItemCommand> Itens { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsTrue(CpfCliente.Length == 11, "Cliente", "Cliente inválido")
               .IsTrue(CEP.Length == 8, "CEP", "CEP inválido"));
        }
    }
}
