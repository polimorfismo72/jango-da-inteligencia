using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(c => c.AlunoMatriculadoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Aluno não reconhecido"); 

            RuleFor(c => c.ValorTotal)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
