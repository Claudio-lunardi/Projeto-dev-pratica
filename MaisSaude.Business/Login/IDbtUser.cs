using MaisSaude.Models.tUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Login
{
    public interface IDbtUser
    {

        int Insertuser(tUser tUser);
        int InsertUserData(tUserData tUserData);
    }
}
