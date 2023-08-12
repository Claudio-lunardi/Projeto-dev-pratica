using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tTitular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroTitular
{
    public interface ICadastroTitular
    {

        List<tUser> GetTitulares();
        void CreateTitular(tTitularRetorno tTitularRetorno);
        tTitularRetorno GetTitular(int ID);

        void UpdateTitular(tTitularRetorno tTitularRetorno);


    }
}
