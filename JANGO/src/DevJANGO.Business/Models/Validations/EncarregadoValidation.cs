using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class EncarregadoValidation : AbstractValidator<Encarregado>
    {
        public EncarregadoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Telefone)
                  .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                  //.Length(2, 16).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
                  .Length(9).WithMessage("O campo {PropertyName} deve ter exatamente 9 números");

            RuleFor(c => c.Proficao)
                 .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                 .Length(2, 25).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}