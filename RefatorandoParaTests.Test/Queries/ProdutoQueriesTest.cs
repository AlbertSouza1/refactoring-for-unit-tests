using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Entities;
using RefatorandoParaTestes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefatorandoParaTests.Test.Queries
{
    [TestClass]
    public class ProdutoQueriesTest
    {
        private readonly IList<Produto> _produtos;

        public ProdutoQueriesTest()
        {
            _produtos = new List<Produto>()
            {
                new Produto("Sabão", 10, true),
                new Produto("Feijão", 10, true),
                new Produto("Água", 10, true),
                new Produto("Macarrão", 10, false),
                new Produto("Esfirra", 10, false),
            };
        }

        [TestMethod]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
            var result = _produtos.AsQueryable().Where(ProdutoQueries.ObterProdutosAtivos());

            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _produtos.AsQueryable().Where(ProdutoQueries.ObterProdutosInativos());

            Assert.IsTrue(result.Count() == 2);
        }
    }
}
