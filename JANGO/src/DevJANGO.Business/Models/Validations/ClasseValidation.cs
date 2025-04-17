using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations.Documentos;
using DevJANGO.Business.Services;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class ClasseValidation : AbstractValidator<Classe>
    {
        public ClasseValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}