namespace DevJANGO.Business.Models
{
    public class Mes : Entity
    {
        public string NomeMes { get; set; }
        public int CodMes { get; set; }
        /* EF Relations, Lado UM na Entidade */
        //public IEnumerable<PagamentoPropina> PagamentoPropinas { get; set; } 
        public IEnumerable<Propina> Propinas { get; set; } 
        public IEnumerable<Multa> Multas { get; set; }


      
    }

}