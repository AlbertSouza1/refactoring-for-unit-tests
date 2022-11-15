using RefatorandoParaTestes.Commands.Interfaces;

namespace RefatorandoParaTestes.Commands.Results
{
    public class FalhaResult : ICommandResult
    {
        public FalhaResult(string mensagem = "")
        {
            Sucesso = false;
            Mensagem = mensagem;
        }
    }
}
