using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models.tUser.tTitular
{
    public class tTitular
    {
        [Display(Name = "Titular")]
        [Required(ErrorMessage = "Titular é obrigatório!")]
        public int ID { get; set; }

    }
}
