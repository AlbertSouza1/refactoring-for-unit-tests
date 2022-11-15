using RefatorandoParaTestes.Entities;
using System;
using System.Linq.Expressions;

namespace RefatorandoParaTestes.Queries
{
    public static class ProdutoQueries
    {
        public static Expression<Func<Produto, bool>> ObterProdutosAtivos()       
            => x => x.Ativo;

        public static Expression<Func<Produto, bool>> ObterProdutosInativos()
            => x => !x.Ativo;

    }
}
