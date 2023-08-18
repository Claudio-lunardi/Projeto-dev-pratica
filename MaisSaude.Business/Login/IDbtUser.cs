using MaisSaude.Models.tUser;

namespace MaisSaude.Business.Login
{
    public interface IDbtUser
    {

        int Insertuser(tUser tUser);
        int InsertUserData(tUserData tUserData);
    }
}
