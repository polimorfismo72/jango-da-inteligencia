using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class AplicaMultaService : BaseService, IAplicaMultaService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAplicaMultaRepository _aplicaMultaRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AplicaMultaService(IAplicaMultaRepository aplicaMultaRepository,
                                INotificador notificador) : base(notificador)
        {
            _aplicaMultaRepository = aplicaMultaRepository;
        }
        #endregion

        public async Task Adicionar(AplicaMulta aplicaMulta)
        {
            //if (!ExecutarValidacao(new  ProfessorValidation(),  professor)) return;
            if (_aplicaMultaRepository.Buscar(e => e.Nome == aplicaMulta.Nome).Result.Any())
            {
                Notificar("Já foi aplicado a multa para este mês!");
                return;
            }
        
          

            await _aplicaMultaRepository.Adicionar(aplicaMulta);
        }

        //public async Task Atualizar( Professor professor)
        //{
        //    if (!ExecutarValidacao(new  ProfessorValidation(),  professor)) return;

        //    if (_professorRepository.Buscar(e => e.BI == professor.BI).Result.Any())
        //    {
        //        Notificar("Já existe um  professor com este BI infomado.");
        //        return;
        //    }
        //    if (_professorRepository.Buscar(e => e.Telefone == professor.Telefone).Result.Any())
        //    {
        //        Notificar("Já existe um  professor com este telefone infomado.");
        //        return;
        //    }
        //    if (_professorRepository.Buscar(e => e.Email == professor.Email).Result.Any())
        //    {
        //        Notificar("Já existe um  professor com este email infomado.");
        //        return;
        //    }

        //    await _professorRepository.Atualizar( professor);
        //}

        //public async Task Remover(Guid id)
        //{
        //    await _professorRepository.Remover(id);
        //}

        public void Dispose()
        {
            _aplicaMultaRepository?.Dispose();
        }

    }

}