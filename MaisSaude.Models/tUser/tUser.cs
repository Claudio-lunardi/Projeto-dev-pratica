using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models.tUser
{
    public class tUser 
    {
        #region Tabela
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O usuário não pode ter mais de 100 caracteres.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(12, MinimumLength = 12)]
        public string Senha { get; set; } = string.Empty;

        public int RoleID { get; set; }
       
        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }


        #endregion 



        public string? Role { get; set; }
    }
}
