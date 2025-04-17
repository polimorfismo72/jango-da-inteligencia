using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class AlunoMatriculadoService : BaseService, IAlunoMatriculadoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AlunoMatriculadoService(IAlunoMatriculadoRepository alunoMatriculadoRepository,
                                INotificador notificador) : base(notificador)
        {
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
        }
        #endregion

        public async Task Adicionar(AlunoMatriculado alunoMatriculado)
        {
            if (!ExecutarValidacao(new AlunoMatriculadoValidation(), alunoMatriculado)) return;

            if (_alunoMatriculadoRepository.Buscar(a => a.NumDocumento == alunoMatriculado.NumDocumento && a.AnoLetivo == alunoMatriculado.AnoLetivo).Result.Any())
            {
                Notificar($"O aluno que pretende matricular com o documento '{alunoMatriculado.NumDocumento}', já existe neste letivo {alunoMatriculado.AnoLetivo}.");
                return;
            }
            
            await _alunoMatriculadoRepository.Adicionar(alunoMatriculado);
        }

        public async Task Atualizar(AlunoMatriculado alunoMatriculado)
        {
            if (!ExecutarValidacao(new AlunoMatriculadoValidation(), alunoMatriculado)) return;

            if (_alunoMatriculadoRepository.Buscar(a=> a.NumDocumento == alunoMatriculado.NumDocumento && a.Id != alunoMatriculado.Id).Result.Any())
            {
                Notificar("Já existe um aluno matriculado com este documento infomado.");
                return;
            }

            await _alunoMatriculadoRepository.Atualizar(alunoMatriculado);
        }

        public async Task Remover(Guid id)
        {
            await _alunoMatriculadoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alunoMatriculadoRepository?.Dispose();
        }
    }

}