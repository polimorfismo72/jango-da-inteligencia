using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class PagamentoPropinaValidation : AbstractValidator<PagamentoPropina>
    {
        public PagamentoPropinaValidation()
        {
            //RuleFor(c => c.NumeroDeTransacaoDePagamento)
            //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            //    .Length(2, 29).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Descricao)
                  .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                  .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
           
        }
    }
}