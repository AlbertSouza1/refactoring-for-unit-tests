using RefatorandoParaTestes.Entities;

namespace RefatorandoParaTestes.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Cliente Obter(string documento);
    }
}
