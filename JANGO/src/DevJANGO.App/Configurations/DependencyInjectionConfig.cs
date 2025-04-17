using DevJANGO.App.Extensions;
using DevJANGO.App.Queries;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations.Documentos;
using DevJANGO.Business.Notificacoes;
using DevJANGO.Business.Services;
using DevJANGO.Business.Services.AlunosInscritosServices;
using DevJANGO.Data.Context;
using DevJANGO.Data.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace DevJANGO.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<JangoDbContext>();
            //Repository
            services.AddScoped<IAplicaMultaRepository, AplicaMultaRepository>();
            services.AddScoped<IFuncionarioCaixaRepository, FuncionarioCaixaRepository>();
            services.AddScoped<IAreaDeConhecimentoRepository, AreaDeConhecimentoRepository>();
            services.AddScoped<IGrauDeParentescoRepository, GrauDeParentescoRepository>();
            services.AddScoped<IMesRepository, MesRepository>();
            services.AddScoped<IEncarregadoRepository, EncarregadoRepository>();
            services.AddScoped<INiveisDeEnsinoRepository, NiveisDeEnsinoRepository>();
            services.AddScoped<IProfessorDisciplinaClasseRepository, ProfessorDisciplinaClasseRepository>();
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IClasseRepository, ClasseRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IAlunoInscritoRepository, AlunoInscritoRepository>();
            services.AddScoped<IAlunoMatriculadoRepository, AlunoMatriculadoRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IPropinaRepository, PropinaRepository>();
            services.AddScoped<IMultaRepository, MultaRepository>();
            services.AddScoped<IPagamentoPropinaRepository, PagamentoPropinaRepository>();
            services.AddScoped<IPagamentoMultaRepository, PagamentoMultaRepository>();
            services.AddScoped<IPagamentoMultaItemRepository, PagamentoMultaItemRepository>();
            services.AddScoped<IPedidoItemsRepository, PedidoItemsRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoItemsRepository, PedidoItemsRepository>();

            //Queries
            services.AddScoped<IAlunoInscritoQueries, AlunoInscritoQueries>();
            services.AddScoped<IAlunoMatriculadoQueries, AlunoMatriculadoQueries>();
            services.AddScoped<IProdutoQueries, ProdutoQueries>();
            services.AddScoped<IProfessorDisciplinaClasseQueries, ProfessorDisciplinaClasseQueries>();
            services.AddScoped<IProfessorQueries, ProfessorQueries>();
            services.AddScoped<IPagamentoQueries, PagamentoQueries>();
            services.AddScoped<IPropinaQueries, PropinaQueries>();

            //Validation
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddScoped<INotificador, Notificador>();
           
            //Service
            services.AddScoped<IAplicaMultaService, AplicaMultaService>();
            services.AddScoped<IFuncionarioCaixaService, FuncionarioCaixaService>();
            services.AddScoped<IEncarregadoService, EncarregadoService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IGrauDeParentescoService, GrauDeParentescoService>();
            services.AddScoped<IClasseService, ClasseService>();
            services.AddScoped<IAlunoInscritoIniciacaoService, AlunoInscritoIniciacaoService>();
            services.AddScoped<IAlunoInscritoService, AlunoInscritoService>();
            services.AddScoped<IAlunoMatriculadoService, AlunoMatriculadoService>();
            services.AddScoped<IPagamentoPropinaService, PagamentoPropinaService>();
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();
            services.AddScoped<IPagamentoMultaItemService, PagamentoMultaItemService>();
            services.AddScoped<IPagamentoMultaService, PagamentoMultaService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}