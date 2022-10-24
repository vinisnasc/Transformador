using Transformador.Domain.NotificadorDeErros;

namespace Transformador.Domain.Interfaces.Services
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}