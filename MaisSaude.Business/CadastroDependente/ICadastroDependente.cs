using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tDapendente;
using MaisSaude.Models.tUser.tMedico;
using MaisSaude.Models.tUser.tTitular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroDependente
{
    public interface ICadastroDependente
    {
        List<tUser> GetDependentes();
        List<SelectClinica> GetListTitular();

        void PostDependente(tDependenteRetorno tDependenteRetorno);

        tDependenteRetorno GetDependente(int ID);
        void UpdateDependente(tDependenteRetorno tDependenteRetorno);
    }
}
