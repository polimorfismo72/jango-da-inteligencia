using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DevJANGO.Business.Intefaces
{
    public interface IAlunoMatriculadoRepository : IRepository<AlunoMatriculado>
    {
        /* EF Relations, Lado MUITO na Entidade */
        Task<AlunoMatriculado> ObterTurmaDoAluno(Guid alunoId, string anoLetivo);
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculados();
        Task<IEnumerable<AlunoMatriculado>> ObterTurmaComAlunos(string turma = null);
        Task<AlunoMatriculado> ObterAlunoMatriculado(Guid id);
        Task<AlunoMatriculado> ObterAlunoInscritoJaMatriculado(Guid alunoInscritoId);
        Task<AlunoMatriculado> ObterDocumentoDoAluno(string documento);
        Task<AlunoMatriculado> ObterDocumentoDoAlunoMatriculado(string documento);
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaIniciacao();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaUm();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaDois();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNaEtapaTres();

        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoEnsinoPrimario();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoICiclo();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoIICicloFb();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunosMatriculadosNoIICicloEj();
        Task<IEnumerable<AlunoMatriculado>> ObterAlunoMatriculadosPorEncarregado(Guid encarregadoId);
        Task<AlunoMatriculado> ObterAlunoMatriculadoEncarregadoGrauDeParentesco(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadoAlunoInscritoClasseTurmaNiveisDeEnsinoCursoEncarregadoGrauDeParentesco(Guid id);

        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado UM na Entidade */
        Task<AlunoMatriculado> ObterAlunoMatriculadoAvaliacaos(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadoClasseTurmaCursoAvaliacaos(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseAlunoMatriculados(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseEncarregadoAlunoMatriculados(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadoNiveisDeEnsinoClasseEncarregadoGrauDeParentescoAreaDeConhecimentoAlunoMatriculados(Guid id);
        Task<AlunoMatriculado> ObterAlunoMatriculadosAvaliacaosPagamentoPropinasPropinasMultas(Guid id);
        Task<AlunoMatriculado> ObterPropinasAlunoMatriculado(Guid id);
        Task<Propina> ObterPropinasPorPagamentoPropinaId(Guid id);
        Task<PagamentoPropina> ObterPropinasPorPagamentoPropina(Guid id);

        Task<AlunoMatriculado> ObterAlunoMatriculadoPedidos(Guid id);
        /* Visualizar um determinado Cliente com os seus referidos
          Pedidos, Endereco e Contato  */
        //Task<AlunoMatriculado> ObterAlunoMatriculadoPedidos(Guid id);

    }
}
