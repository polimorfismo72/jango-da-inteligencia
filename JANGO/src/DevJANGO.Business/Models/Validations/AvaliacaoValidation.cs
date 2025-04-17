using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations.Documentos;
using DevJANGO.Business.Services;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class AvaliacaoValidation : AbstractValidator<Avaliacao>
    {
        public AvaliacaoValidation()
        {
            RuleFor(c => c.Nota)
             .GreaterThanOrEqualTo(0)
             .WithMessage(item => $"A nota deve maior ou igual a 0");

            RuleFor(c => c.AnoLetivo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(4, 9).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}