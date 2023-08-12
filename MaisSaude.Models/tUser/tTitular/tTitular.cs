using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models.tUser.tTitular
{
    public class tTitular
    {
        [Display(Name = "Titular")]
        [Required(ErrorMessage = "Titular é obrigatório!")]
        public int ID { get; set; }

    }
}
