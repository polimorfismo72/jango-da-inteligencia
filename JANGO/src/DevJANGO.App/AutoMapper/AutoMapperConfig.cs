using AutoMapper;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Models;

namespace DevJANGO.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AplicaMulta, AplicaMultaViewModel>().ReverseMap();
            CreateMap<AreaDeConhecimento, AreaDeConhecimentoViewModel>().ReverseMap();
            CreateMap<GrauDeParentesco, GrauDeParentescoViewModel>().ReverseMap();
            CreateMap<Encarregado, EncarregadoViewModel>().ReverseMap();
            CreateMap<Mes, MesViewModel>().ReverseMap();
            CreateMap<NiveisDeEnsino, NiveisDeEnsinoViewModel>().ReverseMap();
            CreateMap<FuncionarioCaixa, FuncionarioCaixaViewModel>().ReverseMap();
            CreateMap<Disciplina, DisciplinaViewModel>().ReverseMap();
            CreateMap<TipoAvaliacao, TipoAvaliacaoViewModel>().ReverseMap();
            CreateMap<Trimestre, TrimestreViewModel>().ReverseMap();
            CreateMap<Curso, CursoViewModel>().ReverseMap();
            CreateMap<Classe, ClasseViewModel>().ReverseMap();
            CreateMap<Turma, TurmaViewModel>().ReverseMap();
            CreateMap<Professor, ProfessorViewModel>().ReverseMap();
            CreateMap<ProfessorDisciplinaClasse, ProfessorDisciplinaClasseViewModel> ().ReverseMap();
            CreateMap<AlunoInscrito, AlunoInscritoViewModel>().ReverseMap();
            CreateMap<AlunoMatriculado, AlunoMatriculadoViewModel>().ReverseMap();
            CreateMap<Avaliacao, AvaliacaoViewModel>().ReverseMap();
            CreateMap<Propina, PropinaViewModel>().ReverseMap();
            CreateMap<Multa, MultaViewModel>().ReverseMap();
            CreateMap<PagamentoPropina, PagamentoPropinaViewModel>().ReverseMap();
            CreateMap<PagamentoMulta, PagamentoMultaViewModel>().ReverseMap();
            CreateMap<PagamentoMultaItem, PagamentoMultaItemViewModel>().ReverseMap();
            CreateMap<PedidoItem, PedidoItemViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        }
    }
}