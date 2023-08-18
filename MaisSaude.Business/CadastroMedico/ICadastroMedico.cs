using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tMedico;

namespace MaisSaude.Business.CadastroMedico
{
    public interface ICadastroMedico
    {

        List<tUser> GetMedicos();

        List<SelectClinica> GetClinicas();

        void CreateMedico(tMedicoRetorno tMedicoRetorno);
    }
}
