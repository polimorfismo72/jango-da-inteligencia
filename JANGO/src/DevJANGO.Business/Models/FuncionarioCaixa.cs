

namespace DevJANGO.Business.Models
{
  public class FuncionarioCaixa : Entity
  {
   //public string Nome { get; set; }
   public string Email { get; set; }  
   public string Nome { get; set; }
   public bool Ativo { get; set; }

   /* EF Relations side 1*/
   public IEnumerable<PagamentoPropina> PagamentoPropinas { get; set; }
   public IEnumerable<PagamentoMultaItem> PagamentoMultaItems { get; set; }
   public IEnumerable<AlunoInscrito> AlunoInscritos { get; set; }
   public IEnumerable<AlunoMatriculado> AlunoMatriculados { get; set; }
   public IEnumerable<PedidoItem> PedidoItems { get; set; }

   }
}
