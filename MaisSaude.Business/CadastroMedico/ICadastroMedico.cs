using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tMedico;
using MaisSaude.Models.tUser.tTitular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroMedico
{
    public interface ICadastroMedico
    {

        List<tUser> GetMedicos();

        List<SelectClinica> GetClinicas();

        void CreateMedico(tMedicoRetorno tMedicoRetorno);
    }
}
