using RefatorandoParaTestes.Entities;
using System;
using System.Collections.Generic;

namespace RefatorandoParaTestes.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> Obter(IEnumerable<Guid> codigos);
    }
}
