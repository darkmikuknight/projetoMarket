

using System.ComponentModel.DataAnnotations;

namespace projetoMarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
        [StringLength(100, ErrorMessage = "Nome tem que ser menor que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome muito curto!")]
        public string Nome {get; set;}
    }
}