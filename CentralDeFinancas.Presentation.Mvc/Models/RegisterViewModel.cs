using System.ComponentModel.DataAnnotations;

namespace CentralDeFinancas.Presentation.Mvc.Models
{
    public class RegisterViewModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
        [Required(ErrorMessage = "Email é obrigatório.")]
        public string? Email { get; set; }

        [SenhaValidation(ErrorMessage = "Informe uma senha com letra maiúscula, letra minúscula, número e caractere especial, de 8 a 20 caracteres.")]
        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Confirmação de senha é obrigatório.")]
        public string? SenhaConfirmacao { get; set; }
    }

    public class SenhaValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value != null)
            {
                var senha = value.ToString();

                return senha.Length >= 8
                    && senha.Length <= 20
                    && senha.Any(char.IsUpper)
                    && senha.Any(char.IsLower)
                    && senha.Any(char.IsDigit)
                    && (
                           senha.Contains("!")
                        || senha.Contains("@")
                        || senha.Contains("#")
                        || senha.Contains("$")
                        || senha.Contains("%")
                        || senha.Contains("&")
                    );
            }

            return false;
        }
    }
}
