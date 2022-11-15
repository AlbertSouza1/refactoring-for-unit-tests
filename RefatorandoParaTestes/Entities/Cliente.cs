namespace RefatorandoParaTestes.Entities
{
    public class Cliente : Entidade
    {
        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}
