using System;

namespace RefatorandoParaTestes.Entities
{
    public class Desconto : Entidade
    {
         decimal _valor;

        public Desconto(decimal valor, DateTime dataExpiracao)
        {
            _valor = valor;
            DataExpiracao = dataExpiracao;
        }

        public decimal Valor => EhValido ? _valor : 0;
        public DateTime DataExpiracao { get; private set; }
        public bool EhValido => DateTime.Compare(DateTime.Now, DataExpiracao) < 0;
    }
}
