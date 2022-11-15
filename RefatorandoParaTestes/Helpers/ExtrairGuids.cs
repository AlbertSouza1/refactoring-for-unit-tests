using RefatorandoParaTestes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefatorandoParaTestes.Helpers
{
    public static class ExtrairGuids
    {
        public static IEnumerable<Guid> Extrair(IList<CriarPedidoItemCommand> itens)       
            => itens.Select(x => Guid.Parse(x.ProdutoGuid));      
    }
}
