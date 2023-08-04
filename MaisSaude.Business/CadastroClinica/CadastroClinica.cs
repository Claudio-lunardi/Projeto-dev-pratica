using MaisSaude.Common.Connections;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroClinica
{
    public class CadastroClinica : ICadastroClinica
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroClinica(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }

    }
}
