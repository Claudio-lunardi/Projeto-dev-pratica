using MaisSaude.Models.tUser;
using MaisSaude.Models.tUser.tTitular;

namespace MaisSaude.Business.CadastroUser
{
    public interface ICadastroUser
    {
        Task<List<tUser>> GetUsers();
        Task<bool> InsertTitular(tTitularRetorno tTitularRetorno);
    }
}
