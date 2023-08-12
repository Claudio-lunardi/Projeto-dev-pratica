using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models.tUser
{
    public class tUserData
    {
        [Key]
        public int ID { get; set; }

        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório.")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O número de telefone não é válido.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O celular é obrigatório.")]
        [Phone(ErrorMessage = "O número de celular não é válido.")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, ErrorMessage = "O CEP deve ter 9 caracteres.")]

        public string CEP { get; set; } = string.Empty;

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O logradouro não pode ter mais de 100 caracteres.")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro não pode ter mais de 100 caracteres.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade não pode ter mais de 100 caracteres.")]
        public string Cidade { get; set; } = string.Empty;

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(10, ErrorMessage = "O número não pode ter mais de 10 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        public string? Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string UF { get; set; } = string.Empty;

        public DateTime? DateUpdate { get; set; }

    }
}
