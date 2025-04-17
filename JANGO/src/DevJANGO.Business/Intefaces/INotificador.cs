using System.Collections.Generic;
using DevJANGO.Business.Notificacoes;

namespace DevJANGO.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}