using DevJANGO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class PagamentoMultaItemValidation : AbstractValidator<PagamentoMultaItem>
    {
        public PagamentoMultaItemValidation() 
        {
            RuleFor(a=> a.NomeMulta)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(5, 29)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
         
        }
    }
}