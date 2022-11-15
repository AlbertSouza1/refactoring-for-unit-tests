using Flunt.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefatorandoParaTestes.Helpers;
using RefatorandoParaTests.Test.Helpers.Fakes;
using System.Collections.Generic;

namespace RefatorandoParaTests.Test.Helpers
{
    [TestClass]
    public class NotificationsHelperTest
    {
        [TestMethod]
        public void Dado_uma_list_de_notificacoes_deve_retornar_as_mensagens_separadas_por_quebra_de_linha()
        {
            var fakeCommand = new FakeCommandNotifications();

            var result = NotificationsHelper.ObterMensagens(fakeCommand.Notifications);

            Assert.AreEqual("Mensagem de validação 1\r\nMensagem de validação 2", result);
        }

        [TestMethod]
        public void Dado_uma_lista_de_notificacoes_vazia_deve_retornar_string_vazia()
        {
            var result = NotificationsHelper.ObterMensagens(new List<Notification>());

            Assert.AreEqual("", result);
        }
    }
}
