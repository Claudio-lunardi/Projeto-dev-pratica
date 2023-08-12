using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models.tUser.tMedico
{
    public class tMedicoRetorno
    {
        public tUser tUser { get; set; }
        public tUserData tUserData { get; set; }

        public tMedico tMedico { get; set; }
    }
}
