using MaisSaude.Common.Connections;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroDependente
{



    public class CadastroDependente : ICadastroDependente
    {

        private readonly IOptions<ConnectionDataBase> _DefaultConnection;

        public CadastroDependente(IOptions<ConnectionDataBase> connectionDataBase)
        {
            _DefaultConnection = connectionDataBase;
        }

    }
}
