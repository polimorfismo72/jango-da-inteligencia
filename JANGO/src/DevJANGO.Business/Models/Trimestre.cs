namespace DevJANGO.Business.Models
{
    public class Trimestre : Entity
    {
        public string Nome { get; set; }

        /* EF Relations, Lado UM na Entidade Trimestre */
        public IEnumerable<Avaliacao> Avaliacaos { get; set; }
    }

}