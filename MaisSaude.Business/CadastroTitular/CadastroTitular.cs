using MaisSaude.Common.Connections;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroTitular
{
    public class CadastroTitular : ICadastroTitular
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroTitular(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }


    }
}
