using System.ComponentModel.DataAnnotations;

namespace projetoMarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage = "O nome do produto é obrigatório!")]
        [StringLength(100, ErrorMessage = "Nome tem que ser menor que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome muito curto!")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "A categoria do produto é obrigatória!")]
        public int CategoriaID {get; set;}

        [Required(ErrorMessage = "O fornecedor do produto é obrigatório!")]
        public int FornecedorID {get; set;}

        [Required(ErrorMessage = "O preço de custo do produto é obrigatório!")]
        public float PrecoDeCusto {get; set;}

        [Required(ErrorMessage = "O preço de venda do produto é obrigatório!")]
        public float PrecoDeVenda {get; set;}

        [Required(ErrorMessage = "A medição do produto é obrigatório!")]
        [Range(0, 2, ErrorMessage = "Medição inválida!")]
        public int Medicao {get; set;}

        //Esses atributos só existem porque a framework razor "não se dá bem" com os campos de número do html//
        [Required(ErrorMessage = "O preço de custo do produto é obrigatório!")]
        public string PrecoDeCustoString {get; set;}

        [Required(ErrorMessage = "O preço de venda do produto é obrigatório!")]
        public string PrecoDeVendaString {get; set;}

    }
}