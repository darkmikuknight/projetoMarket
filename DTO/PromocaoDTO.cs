using System.ComponentModel.DataAnnotations;

namespace projetoMarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage = "O nome da promoção é obrigatório!")]
        [StringLength(100, ErrorMessage = "Nome tem que ser menor que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome muito curto!")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "O produto é obrigatório!")]
        public int ProdutoID {get; set;}

        [Required(ErrorMessage = "A porcentagem é obrigatória!")]
        [Range(0, 100, ErrorMessage = "Porcentagem inválida!")]
        public float Porcentagem {get; set;}
    }
}