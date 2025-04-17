namespace DevJANGO.Business.Models
{
    public class TipoAvaliacao : Entity
    {
        public string Nome { get; set; }

        /* EF Relations, Lado UM na Entidade */
        public IEnumerable<Avaliacao> Avaliacaos { get; set; }
    }
}