using DevJANGO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevJANGO.Business.Models.Validations
{
    public class AlunoInscritoValidation : AbstractValidator<AlunoInscrito>
    {
        public AlunoInscritoValidation() 
        {
            RuleFor(a=> a.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 60)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
         
            RuleFor(a=> a.NomeDoPai)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(2, 60)
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            
            RuleFor(a => a.NomeDaMae)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .Length(2, 60)
           .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.NumDocumento)
          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
          .Length(2, 15)
          .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.EscolaDeOrgigem)
         .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
         .Length(2, 60)
         .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.AnoLetivo)
        .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
        .Length(2, 9)
        .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
           
            RuleFor(a => a.Endereco)
       .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
       .Length(2, 250)
       .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}