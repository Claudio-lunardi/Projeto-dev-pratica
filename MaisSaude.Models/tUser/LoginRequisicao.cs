using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models.tUser
{
    public class LoginRequisicao
    {

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
