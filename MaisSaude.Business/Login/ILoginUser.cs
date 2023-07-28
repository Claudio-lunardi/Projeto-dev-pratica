using MaisSaude.Models.tUser;

namespace MaisSaude.Business.Login
{
    public interface ILoginUser
    {
        Task<tUser> AuthenticarUsuario(LoginRequisicao loginRequisicao);
        Task<bool> CreateUser(tUserRetorno tUserRetorno);
        Task<bool> UpdateUser(tUserRetorno tUserRetorno);
        Task<tUserRetorno> GettUser(int UserID);

    }
}
