using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tClinica;

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
