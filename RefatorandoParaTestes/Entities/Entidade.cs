using Flunt.Notifications;
using System;

namespace RefatorandoParaTestes.Entities
{
    public class Entidade : Notifiable<Notification>
    {
        public Entidade()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
