using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IAlunoInscritoRepository : IRepository<AlunoInscrito>
    {
        /* AlunoInscrito N  : 1 Encarregado  */
        /* EF Relations, Lado MUITO na Entidade */
        int VoltarNumeroDeVagasNaTurma(string nn);
        Task<AlunoInscrito> ObterAlunoInscritoEncarregado(Guid id);
        Task<AlunoInscrito> ObterEncarregadoPeloAluno(Guid id);
        Task<IEnumerable<AlunoInscrito>> ObterAluno();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscrito();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosEncarregados();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosPorEncarregado(Guid encarregadoId);
        Task<AlunoInscrito> ObterAlunoInscritoEncarregadoGrauDeParentesco(Guid id);
        Task<AlunoInscrito> ObterAlunoInscrito(Guid id);
        Task<AlunoInscrito> ObterAlunoInscritoPorDocumento(string documento);
        Task<AlunoInscrito> ObterAlunoInscritoPorDocumentoIniciacao(string documento);

        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIniciacao();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosEnsinoPrimario();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosICiclo();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIICicloFb();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritosIICicloEj();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrau();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIniciacao();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaUm();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaDois();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaTres();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauEnsinoPrimeiro();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauICiclo();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloFb();
        Task<IEnumerable<AlunoInscrito>> ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloEj();

        /* AlunoInscrito 1 : N AlunoMatriculado */
        /* EF Relations, Lado UM na Entidade */
        Task<AlunoInscrito> ObterAlunoInscritoAlunoMatriculados(Guid id);
        Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseAlunoMatriculados(Guid id);
        Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseEncarregadoAlunoMatriculados(Guid id);
        Task<AlunoInscrito> ObterAlunoInscritoNiveisDeEnsinoClasseEncarregadoGrauDeParentescoAreaDeConhecimentoAlunoMatriculados(Guid id);

    }
}
