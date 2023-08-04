using MaisSaude.Common.Connections;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroMedico
{
    public class CadastroMedico : ICadastroMedico
    {
        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroMedico(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }


    }
}
