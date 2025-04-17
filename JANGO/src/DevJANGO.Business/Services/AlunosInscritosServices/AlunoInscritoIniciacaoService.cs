using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services.AlunosInscritosServices
{

    public class AlunoInscritoIniciacaoService : BaseService, IAlunoInscritoIniciacaoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AlunoInscritoIniciacaoService(IAlunoInscritoRepository alunoInscritoRepository,
                                INotificador notificador) : base(notificador)
        {
            _alunoInscritoRepository = alunoInscritoRepository;
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

            if (_alunoInscritoRepository.Buscar(a=> a.NumDocumento == alunoInscrito.NumDocumento && a.Id != alunoInscrito.Id).Result.Any())
            {
                Notificar("Já existe um alunoInscrito com este documento infomado.");
                return;
            }

            await _alunoInscritoRepository.Atualizar(alunoInscrito);
        }

        public async Task Remover(Guid id)
        {
            await _alunoInscritoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alunoInscritoRepository.Dispose();
        }
    }

}