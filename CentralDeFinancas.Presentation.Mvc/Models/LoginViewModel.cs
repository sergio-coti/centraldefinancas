using System.ComponentModel.DataAnnotations;

namespace CentralDeFinancas.Presentation.Mvc.Models
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
        [Required(ErrorMessage = "Email é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string? Senha { get; set; }
    }
}
