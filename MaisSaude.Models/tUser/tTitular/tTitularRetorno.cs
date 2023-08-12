using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models.tUser.tTitular
{
    public class tTitularRetorno
    {
        public tUser tUser { get; set; }
        public tUserData tUserData { get; set; } 

        public tTitular? tTitular { get; set; }
    }
}
