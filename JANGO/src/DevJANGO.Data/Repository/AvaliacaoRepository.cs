using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(JangoDbContext context) : base(context){ }
        public async Task<Avaliacao> ObterAvaliacaoAlunoMatriculado(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(a => a.AlunoMatriculado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosAlunoMatriculado()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(a => a.AlunoMatriculado)
                .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorAlunoMatriculado(Guid alunoMatriculadoId)
        { return await Buscar(a => a.AlunoMatriculadoId == alunoMatriculadoId); }

        public async Task<Avaliacao> ObterAvaliacaoTipoAvaliacao(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(t => t.TipoAvaliacao)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosTipoAvaliacao()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(t => t.TipoAvaliacao)
                 .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTipoAvaliacao(Guid tipoAvaliacaoId)
        { return await Buscar(t => t.TipoAvaliacaoId == tipoAvaliacaoId); }

        public async Task<Avaliacao> ObterAvaliacaoTrimestre(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(f => f.Trimestre)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosTrimestre()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(t => t.Trimestre)
              .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTrimestre(Guid trimestreId)
        { return await Buscar(t => t.TrimestreId == trimestreId); }

        public async Task<Avaliacao> ObterAvaliacaoClasse(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(f => f.Classe)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosClasse()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(c => c.Classe)
                 .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorClasse(Guid classeId)
        { return await Buscar(c => c.ClasseId == classeId); }

        public async Task<Avaliacao> ObterAvaliacaoTurma(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(t => t.Turma)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosTurma()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(t => t.Turma)
                 .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTurma(Guid turmaId)
        { return await Buscar(t => t.TurmaId == turmaId); }

        public async Task<Avaliacao> ObterAvaliacaoDisciplina(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(f => f.Disciplina)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosDisciplina()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(d => d.Disciplina)
                 .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorDisciplina(Guid disciplinaId)
        { return await Buscar(d => d.DisciplinaId == disciplinaId); }

        public async Task<Avaliacao> ObterAvaliacaoProfessor(Guid id)
        {
            return await Db.Avaliacaos.AsNoTracking().Include(p => p.Professor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosProfessor()
        {
            return await Db.Avaliacaos.AsNoTracking().Include(p => p.Professor)
               .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorProfessor(Guid professorId)
        { return await Buscar(p => p.ProfessorId == professorId); }

    }
}
