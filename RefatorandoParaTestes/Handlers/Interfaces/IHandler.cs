using RefatorandoParaTestes.Commands.Interfaces;

namespace RefatorandoParaTestes.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
