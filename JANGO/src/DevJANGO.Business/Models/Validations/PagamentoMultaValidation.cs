using DevJANGO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class PagamentoMultaValidation : AbstractValidator<PagamentoMulta>
    {
        public PagamentoMultaValidation() 
        {
            //RuleFor(a=> a.NumeroDeTransacaoDePagamento)
            //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            //    .Length(5, 150)
            //    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
         
        }
    }
}