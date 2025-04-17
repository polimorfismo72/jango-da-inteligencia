using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class ProfessorValidation : AbstractValidator<Professor>
    {
        public ProfessorValidation()
        {
            RuleFor(c => c.BI)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 16).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Telefone)
                  .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                   .Length(9).WithMessage("O campo {PropertyName} deve ter exatamente 9 números");

            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                 .Length(2, 254).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}