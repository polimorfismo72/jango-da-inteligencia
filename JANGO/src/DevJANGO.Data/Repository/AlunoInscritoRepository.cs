using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class AlunoInscritoRepository : Repository<AlunoInscrito>, IAlunoInscritoRepository
    {
        private readonly JangoDbContext _context;
        public AlunoInscritoRepository(JangoDbContext context) : base(context) { _context = context; }
        public async Task<AlunoInscrito> ObterAlunoInscritoEncarregado(Guid id){
            return await Db.AlunoInscritos.AsNoTracking().Include(e => e.Encarregado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAluno()
        {
            return await Db.AlunoInscritos.AsNoTracking()
            .Include(f => f.NiveisDeEnsino)
            //.Include(c => c.Classe)
            .ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscrito()
        {
            return await Db.AlunoInscritos.AsNoTracking()
            .Include(f => f.NiveisDeEnsino)
            .Include(c => c.Classe)
            .ToListAsync();
        }
        public int VoltarNumeroDeVagasNaTurma(string nn)
        {
            var contador =   _context.AlunoInscritos.Where(c => c.NiveisDeEnsino.NomeNiveisDeEnsino == nn)
                .Select(c => c.Id).Count();
            return contador;
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrau()
        {
            return await Db.AlunoInscritos.AsNoTracking()
            .Include(f => f.Encarregado)
            .Include(f => f.NiveisDeEnsino)
            .Include(a => a.AreaDeConhecimento)
            .Include(a => a.GrauDeParentesco)
            .Include(c => c.Classe)
            .OrderBy(a => a.Nome).ToListAsync();

        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIniciacao()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaUm()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaDois()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaTres()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEnsinoPrimeiro()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                  .Include(f => f.Encarregado)
                  .Include(f => f.NiveisDeEnsino)
                  .Include(a => a.AreaDeConhecimento)
                  .Include(a => a.GrauDeParentesco)
                  .Include(c => c.Classe)
                 .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauICiclo()
        {
            return await Db.AlunoInscritos.Where(a => a.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloFb()
        {
            return await Db.AlunoInscritos.Where(a => a.AreaDeConhecimento.Nome == "IICiclo FB").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloEj()
        {
            return await Db.AlunoInscritos.Where(a => a.AreaDeConhecimento.Nome == "IICiclo EJ").AsNoTracking()
                 .Include(f => f.Encarregado)
                 .Include(f => f.NiveisDeEnsino)
                 .Include(a => a.AreaDeConhecimento)
                 .Include(a => a.GrauDeParentesco)
                 .Include(c => c.Classe)
                .OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosEncarregados() {
            return await Db.AlunoInscritos.AsNoTracking().Include(e => e.Encarregado)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosPorEncarregado(Guid encarregadoId){ 
            return await Buscar(a => a.EncarregadoId == encarregadoId); 
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIniciacao()
        {
            return await Db.AlunoInscritos.Where(n => n.AreaDeConhecimento.Nome == "Iniciação").AsNoTracking()
           .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosEnsinoPrimario()
        {
            return await Db.AlunoInscritos.Where(n => n.AreaDeConhecimento.Nome == "Primário").AsNoTracking()
           .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosICiclo()
        {
            return await Db.AlunoInscritos.Where(n => n.AreaDeConhecimento.Nome == "I Ciclo").AsNoTracking()
           .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIICicloFb()
        {
            return await Db.AlunoInscritos.Where(n => n.AreaDeConhecimento.Nome == "IICiclo FB").AsNoTracking()
           .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIICicloEj()
        {
            return await Db.AlunoInscritos.Where(n => n.AreaDeConhecimento.Nome == "IICiclo EJ").AsNoTracking()
           .OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<AlunoInscrito> ObterEncarregadoPeloAluno(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(a => a.Encarregado)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoEncarregadoGrauDeParentesco(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(c => c.AlunoMatriculados)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoInscrito> ObterAlunoInscrito(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()

                .Include(n => n.NiveisDeEnsino)
                .Include(c => c.Classe)
                .Include(e => e.Encarregado)
                .Include(g => g.FuncionarioCaixa)
                .Include(g => g.GrauDeParentesco)
                .Include(a => a.AreaDeConhecimento)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<AlunoInscrito> ObterAlunoInscritoPorDocumento(string documento)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(c => c.Classe)
                .Include(e => e.Encarregado)
                .Include(g => g.GrauDeParentesco)
                .Include(a => a.AreaDeConhecimento)
           .FirstOrDefaultAsync(a => a.NumDocumento == documento);
            //.FirstOrDefaultAsync(a => a.NumDocumento.Contains(documento) == a.NumDocumento.Contains(documento));
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoPorDocumentoIniciacao(string documento)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(c => c.Classe)
                .Include(e => e.Encarregado)
                .Include(g => g.GrauDeParentesco)
                .Include(a => a.AreaDeConhecimento)
           .FirstOrDefaultAsync(a => a.NumDocumento == documento && a.Classe.Nome == "Pré");
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoAlunoMatriculados(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(c => c.AlunoMatriculados)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseAlunoMatriculados(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(c => c.AlunoMatriculados)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseEncarregadoAlunoMatriculados(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()
                .Include(c => c.AlunoMatriculados)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseEncarregadoGrauDeParentescoAreaDeConhecimentoAlunoMatriculados(Guid id)
        {
            return await Db.AlunoInscritos.AsNoTracking()

                .Include(n => n.NiveisDeEnsino)
                .Include(c => c.Classe)
                .Include(e => e.Encarregado)
                .Include(g => g.GrauDeParentesco)
                .Include(a => a.AreaDeConhecimento)
            
           .FirstOrDefaultAsync(a => a.Id == id);
        }
     
    }
}
