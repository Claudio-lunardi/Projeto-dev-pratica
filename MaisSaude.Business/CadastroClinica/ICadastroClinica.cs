using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tClinica;
using MaisSaude.Models.tUser.tTitular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroClinica
{
    public interface ICadastroClinica
    {

        List<tUser> GetClinicas();

        void CreateClinica(tClinicaRetorno tClinicaRetorno);

        void UpdateClinica(tClinicaRetorno tClinicaRetorno);

        tClinicaRetorno GetClinica(int ID);
    }
}
