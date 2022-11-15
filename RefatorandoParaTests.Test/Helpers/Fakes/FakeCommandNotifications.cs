using Flunt.Notifications;
using Flunt.Validations;

namespace RefatorandoParaTests.Test.Helpers.Fakes
{
    internal class FakeCommandNotifications : Notifiable<Notification>
    {
        public FakeCommandNotifications()
        {
            AddNotifications(new Contract<Notification>()
             .Requires()
             .IsTrue(false, "", "Mensagem de validação 1")
             .IsTrue(false, "", "Mensagem de validação 2"));
        }
    }
}
