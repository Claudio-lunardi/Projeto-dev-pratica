using MaisSaude.Models.tUser;

namespace MaisSaude.Business.Users
{
    public interface IUsers
    {

        Task<List<tUser>> GetUsers();
        Task<List<tUser>> GetUserClinica();
    }
}
