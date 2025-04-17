using DevJANGO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class AlunoMatriculadoValidation : AbstractValidator<AlunoMatriculado>
    {
        public AlunoMatriculadoValidation() 
        {
            RuleFor(a=> a.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
     
            RuleFor(a => a.AnoLetivo)
     .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
     .Length(2, 9)
     .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}