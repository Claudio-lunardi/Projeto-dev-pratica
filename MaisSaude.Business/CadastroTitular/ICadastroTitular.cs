using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tTitular;

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
