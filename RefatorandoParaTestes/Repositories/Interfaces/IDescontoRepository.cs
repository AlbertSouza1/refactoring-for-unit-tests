using RefatorandoParaTestes.Entities;

namespace RefatorandoParaTestes.Repositories.Interfaces
{
    public interface IDescontoRepository
    {
        Desconto Obter(string codigo);
    }
}
