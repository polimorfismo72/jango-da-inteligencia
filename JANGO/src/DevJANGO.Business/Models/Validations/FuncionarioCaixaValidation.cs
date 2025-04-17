using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class FuncionarioCaixaValidation : AbstractValidator<FuncionarioCaixa>
    {
        public FuncionarioCaixaValidation()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 254).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}