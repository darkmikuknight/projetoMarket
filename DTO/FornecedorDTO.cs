using System;
using System.ComponentModel.DataAnnotations;



namespace projetoMarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id {get; set;}

        [Required(ErrorMessage = "O nome do fornecedor é obrigatório!")]
        [StringLength(100, ErrorMessage = "Nome tem que ser menor que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome muito curto!")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "O e-mail do fornecedor é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email {get; set;}

        [Required(ErrorMessage = "O telefone do fornecedor é obrigatório!")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone {get; set;}
    }
}