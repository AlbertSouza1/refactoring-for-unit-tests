using RefatorandoParaTestes.Commands.Interfaces;

namespace RefatorandoParaTestes.Commands.Results
{
    public class SucessoResult : ICommandResult
    {
        public SucessoResult(string mensagem = "", object dados = null)
        {
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}
