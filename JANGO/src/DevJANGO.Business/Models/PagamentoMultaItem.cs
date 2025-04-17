

namespace DevJANGO.Business.Models
{
    public class PagamentoMultaItem : Entity
    {
      public Guid PagamentoMultaId { get; set; }
      public Guid MultaId { get; set; }
      public Guid FuncionarioCaixaId { get;  set; }
      public string NomeMulta { get; set; }
      public int Quantidade { get; set; }
      public decimal PrecoMulta { get; set; }

       /* EF Relations, Lado MUITO na Entidade PagamentoMultaItem */
      public Multa Multa { get; set; }
      public PagamentoMulta PagamentoMulta { get; set; }
      public FuncionarioCaixa FuncionarioCaixa { get; set; }
        
    }
}
