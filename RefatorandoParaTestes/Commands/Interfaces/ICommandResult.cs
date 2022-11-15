namespace RefatorandoParaTestes.Commands.Interfaces
{
    public abstract class ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
    }
}
