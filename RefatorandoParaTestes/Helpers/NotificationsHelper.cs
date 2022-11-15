using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefatorandoParaTestes.Helpers
{
    public class NotificationsHelper
    {
        public static string ObterMensagens(IReadOnlyCollection<Notification> notifications)
            => string.Join(Environment.NewLine, notifications.Select(x => x.Message));        
    }
}
