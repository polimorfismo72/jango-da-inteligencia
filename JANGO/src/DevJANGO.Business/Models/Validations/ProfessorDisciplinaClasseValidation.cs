using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations.Documentos;
using DevJANGO.Business.Services;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class ProfessorDisciplinaClasseValidation : AbstractValidator<ProfessorDisciplinaClasse>
    {
        public ProfessorDisciplinaClasseValidation()
        {
            RuleFor(c => c.AnoLetivo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2,9).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}