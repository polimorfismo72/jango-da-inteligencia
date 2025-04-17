using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data.Common;


namespace DevJANGO.Data.Repository
{
    public class AlunoMatriculadoRepository : Repository<AlunoMatriculado>, IAlunoMatriculadoRepository
    {
        private readonly JangoDbContext _context;
        public AlunoMatriculadoRepository(JangoDbContext context) : base(context) { _context = context; }

        /* EF Relations, Lado MUITO na Entidade */
        //public DbConnection ObterConexao() => _context.Database.GetDbConnection();
        public async Task<AlunoMatriculado> ObterAlunoMatriculado(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()
                    .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                      .Include(f => f.FuncionarioCaixa)

                   .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoInscritoJaMatriculado(Guid alunoInscritoId)
        {
            return await Db.AlunoMatriculados.AsNoTracking()
                  .Include(f => f.AlunoInscrito)
                   .FirstOrDefaultAsync(a => a.AlunoInscritoId == alunoInscritoId);
        }
        public async Task<AlunoMatriculado> ObterTurmaDoAluno(Guid alunoId, string anoLetivo)
        {
            return await Db.AlunoMatriculados.AsNoTracking()
                  .Include(f => f.Turma)
                   .FirstOrDefaultAsync(a => a.Id == alunoId && a.AnoLetivo == anoLetivo);
        }
        public async Task<PagamentoPropina> ObterPropinasPorPagamentoPropina(Guid id)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                      .Include(f => f.AlunoMatriculado).Where(a => a.Id == id)
                   .FirstOrDefaultAsync();
        }
        public async Task<Propina> ObterPropinasPorPagamentoPropinaId(Guid id)
        {
            return await Db.Propinas.AsNoTracking()
                    .Include(f => f.Mes)
                      .Include(f => f.AlunoMatriculado).Where(a => a.Id == id)
                      .Include(f => f.Turma)
                      .Include(f => f.Classe)
                      .Include(f => f.PagamentoPropina)
                   .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculados()
        {
            return await Db.AlunoMatriculados.AsNoTracking()
                      //.Include(f => f.AlunoInscrito)
                      //.Include(f => f.Classe)
                      //.Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      //.Include(f => f.Curso)
                      //.Include(f => f.Encarregado)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaIniciacao()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterTurmaComAlunos(string turma = null)
        {
            return await Db.AlunoMatriculados
         .Include(p => p.AlunoInscrito)
         .Include(p => p.Turma)
         .Include(p => p.Classe)
         .AsNoTracking()
         .Where(a => a.Turma.NomeTurma == turma)
         .OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaUm()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaDois()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaTres()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoEnsinoPrimario()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoICiclo()
        {
            return await Db.AlunoMatriculados.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoIICicloFb()
        {
            return await Db.AlunoMatriculados.Where(a => a.Curso.Nome == "Ciências Fisicas e Biologicas").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoIICicloEj()
        {
            return await Db.AlunoMatriculados.Where(a => a.Curso.Nome == "Ciências Economicas e Jurídicas").AsNoTracking()
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoMatriculado>> ObterAlunoMatriculadosPorEncarregado(Guid encarregadoId)
        {
            return await Buscar(a => a.EncarregadoId == encarregadoId);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoEncarregadoGrauDeParentesco(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoAlunoInscritoClasseTurmaNiveisDeEnsinoCursoEncarregadoGrauDeParentesco(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }

        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado UM na Entidade */
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoAvaliacaos(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoClasseTurmaCursoAvaliacaos(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseAlunoMatriculados(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseEncarregadoAlunoMatriculados(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseEncarregadoGrauDeParentescoAreaDeConhecimentoAlunoMatriculados(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadosAvaliacaosPagamentoPropinasPropinasMultas(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()

                .Include(n => n.Avaliacaos)
                 .Include(e => e.Propinas)
                .Include(c => c.PagamentoPropinas)
                .Include(g => g.Multas)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AlunoMatriculado> ObterPropinasAlunoMatriculado(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()
               .Include(c => c.Propinas)
               .Include(c => c.PagamentoPropinas)
               .Include(g => g.Multas)
                      .Include(f => f.AlunoInscrito)
                      .Include(f => f.Classe)
                      .Include(f => f.Turma)
                      .Include(f => f.NiveisDeEnsino)
                      .Include(f => f.Curso)
                      .Include(f => f.Encarregado)
                      .Include(f => f.GrauDeParentesco)
                      .Include(f => f.FuncionarioCaixa)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoMatriculado> ObterDocumentoDoAluno(string documento)
        {
            return await Db.AlunoMatriculados.AsNoTracking().Include(f => f.Classe)
           .FirstOrDefaultAsync(a => a.NumDocumento == documento);
        }
        public async Task<AlunoMatriculado> ObterDocumentoDoAlunoMatriculado(string documento)
        {
            return await Db.AlunoMatriculados.AsNoTracking().Include(f => f.Classe)
           .FirstOrDefaultAsync(a => a.NumDocumento == documento);
        }
        public async Task<AlunoMatriculado> ObterAlunoMatriculadoPedidos(Guid id)
        {
            return await Db.AlunoMatriculados.AsNoTracking()
              .Include(c => c.Pedidos)
              .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
