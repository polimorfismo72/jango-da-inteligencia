using DevJANGO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Context
{
    public class JangoDbContext : DbContext
    {
        public JangoDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        #region ENTIDADES
        public DbSet<AplicaMulta> AplicaMultas { get; set; }
        public DbSet<FuncionarioCaixa> FuncionarioCaixas { get; set; }
        public DbSet<AreaDeConhecimento> AreaDeConhecimentos { get; set; }
        public DbSet<GrauDeParentesco> GrauDeParentescos { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Encarregado> Encarregados { get; set; }
        public DbSet<NiveisDeEnsino> NiveisDeEnsinos { get; set; }
        public DbSet<ProfessorDisciplinaClasse> ProfessorDisciplinaClasses { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<TipoAvaliacao> TipoAvaliacaos { get; set; }
        public DbSet<Trimestre> Trimestres { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<AlunoInscrito> AlunoInscritos { get; set; }
        public DbSet<AlunoMatriculado> AlunoMatriculados { get; set; }
        public DbSet<Avaliacao> Avaliacaos { get; set; }
        public DbSet<Propina> Propinas { get; set; }
        public DbSet<Multa> Multas { get; set; }
        public DbSet<PagamentoPropina> PagamentoPropinas { get; set; }
        public DbSet<PagamentoMulta> PagamentoMultas { get; set; }
        public DbSet<PagamentoMultaItem> PagamentoMultaItems { get; set; }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Confiração padráo para varchar(100) das entidades cujo campo é string */
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            base.OnModelCreating(modelBuilder);

            /* ApplyConfigurationsFromAssembly Configura de uma só vez o mapeamento das entidades bo Mappings */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JangoDbContext).Assembly);


            /* Desabilitar o Cascade Delete */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            
            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MinhaSequenciaCodigoAluno").StartsAt(1).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
