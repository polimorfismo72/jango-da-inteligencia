using System;
using System.Linq;
using System.Threading.Tasks;
 
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services.AlunosInscritosServices
{

    public class AlunoInscritoService : BaseService, IAlunoInscritoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
      
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AlunoInscritoService(IAlunoInscritoRepository alunoInscritoRepository,
            IAlunoMatriculadoRepository alunoMatriculadoRepository,
        INotificador notificador) : base(notificador)
        {
            _alunoInscritoRepository = alunoInscritoRepository;
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
        }
        #endregion

        public async Task Adicionar(AlunoInscrito alunoInscrito)
        {
            if (!ExecutarValidacao(new AlunoInscritoValidation(), alunoInscrito)) return;

            if (_alunoInscritoRepository.Buscar(a => a.NumDocumento == alunoInscrito.NumDocumento).Result.Any())
            {
                Notificar("Já existe um aluno Inscrito com este documento infomado.");
                return;
            }
            await _alunoInscritoRepository.Adicionar(alunoInscrito);
        }

        public async Task Atualizar(AlunoInscrito alunoInscrito)
        {
            if (!ExecutarValidacao(new AlunoInscritoValidation(), alunoInscrito)) return;

            if (_alunoInscritoRepository.Buscar(a => a.NumDocumento == alunoInscrito.NumDocumento && a.Id != alunoInscrito.Id).Result.Any())
            {
                Notificar("Já existe um alunoInscrito com este documento infomado.");
                return;
            }
          
            await _alunoInscritoRepository.Atualizar(alunoInscrito);
        }

        public async Task Remover(Guid id)
        {

            if (_alunoMatriculadoRepository.Buscar(a => a.AlunoInscritoId == id).Result.Any())
            {
                Notificar("Este aluno já está matriculado.");
                return;
            }
            await _alunoInscritoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alunoInscritoRepository?.Dispose();
        }
       

    }

}