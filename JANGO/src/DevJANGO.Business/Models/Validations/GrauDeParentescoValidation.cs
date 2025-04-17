using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class GrauDeParentescoValidation : AbstractValidator<GrauDeParentesco>
    {
        public GrauDeParentescoValidation()
        {
            RuleFor(c => c.NomeGrauParentesco)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}