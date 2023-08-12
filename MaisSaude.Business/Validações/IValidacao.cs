using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Validações
{
    public interface IValidacao
    {
        bool ExisteEmail(string Email);
    }
}
