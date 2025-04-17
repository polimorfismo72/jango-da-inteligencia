using DevJANGO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected ICollection<string> Erros = new List<string>();
        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }
    }
}