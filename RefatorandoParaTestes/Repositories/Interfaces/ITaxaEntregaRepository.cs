using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefatorandoParaTestes.Repositories.Interfaces
{
    public interface ITaxaEntregaRepository
    {
        decimal Obter(string cep);
    }
}
