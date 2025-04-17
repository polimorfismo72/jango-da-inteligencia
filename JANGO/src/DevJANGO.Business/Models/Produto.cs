namespace DevJANGO.Business.Models
{
    public class Produto : Entity
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeEstoque { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations  Lado UM */
        public IEnumerable<PedidoItem> PedidoItems { get; set; }

    }
}