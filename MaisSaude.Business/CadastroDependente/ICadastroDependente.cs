using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tDapendente;
using MaisSaude.Models.tUser.tMedico;

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
